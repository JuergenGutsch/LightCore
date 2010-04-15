using System;
using System.Collections.Generic;
using System.Linq;

using LightCore.Configuration.Properties;
using LightCore.ExtensionMethods.System;
using LightCore.Fluent;

namespace LightCore.Configuration
{
    ///<summary>
    /// Represents the loader for a LightCore registration.
    ///</summary>
    public class RegistrationLoader
    {
        /// <summary>
        /// Contains the configuration.
        /// </summary>
        private LightCoreConfiguration _configuration;

        /// <summary>
        /// Contains the container builder.
        /// </summary>
        private IContainerBuilder _containerBuilder;

        /// <summary>
        /// Contains the singleton instance.
        /// </summary>
        private static readonly RegistrationLoader _instance = new RegistrationLoader();

        ///<summary>
        /// Gets the current instance of <see cref="RegistrationLoader" />.
        ///</summary>
        public static RegistrationLoader Instance
        {
            get
            {
                return _instance;
            }
        }

        /// <summary>
        /// Registers all candidates.
        /// </summary>
        /// <param name="containerBuilder">The containerbuilder.</param>
        /// <param name="configuration">The configuration</param>
        public void Register(IContainerBuilder containerBuilder, LightCoreConfiguration configuration)
        {
            this._configuration = configuration;
            this._containerBuilder = containerBuilder;

            IEnumerable<RegistrationGroup> registrationGroups = configuration.RegistrationGroups;
            IEnumerable<Registration> registrationsToRegister = configuration.Registrations;

            if (configuration.ActiveRegistrationGroups == null)
            {
                registrationsToRegister = registrationsToRegister.Union(registrationGroups.SelectMany(g => g.Registrations));
            }
            else
            {
                var activeGroups = configuration.ActiveRegistrationGroups.Split(new[] { ',' },
                                                                                 StringSplitOptions.RemoveEmptyEntries);

                foreach (string group in activeGroups)
                {
                    if (!registrationGroups.Any(g => g.Name != null && g.Name.Trim() == group.Trim()))
                    {
                        throw new ActiveGroupNotFoundException(
                            Resources.ActiveRegistrationGroupNotFoundFormat.FormatWith(group));
                    }
                }

                Func<RegistrationGroup, bool> groupNameIsEmpty = group => string.IsNullOrEmpty(group.Name);
                Func<RegistrationGroup, bool> groupNameIsNotEmpty = group => !groupNameIsEmpty(group);
                Func<RegistrationGroup, bool> groupIsActive =
                    group => activeGroups.Any(activeGroup => activeGroup.Trim() == group.Name.Trim());

                var validGroupRegistrations = registrationGroups
                    .Where(group => groupNameIsEmpty(group) || groupNameIsNotEmpty(group) && groupIsActive(group))
                    .SelectMany(group => group.Registrations);

                registrationsToRegister = registrationsToRegister.Union(validGroupRegistrations);
            }

            foreach (Registration registration in registrationsToRegister)
            {
                ProcessRegistration(registration);
            }
        }

        /// <summary>
        /// Processes a Registration.
        /// </summary>
        /// <param name="registration">The registration to process.</param>
        private void ProcessRegistration(Registration registration)
        {
            string contractTypeName = this.ResolveAlias(registration.ContractType);

            if (string.IsNullOrEmpty(contractTypeName))
            {
                throw new ArgumentException(string.Format(Resources.ContractTypeCannotBeEmptyFormat, registration));
            }

            string implementationTypeName = this.ResolveAlias(registration.ImplementationType);

            if (string.IsNullOrEmpty(implementationTypeName))
            {
                throw new ArgumentException(string.Format(Resources.ImplementationTypeCannotBeEmptyFormat, registration));
            }

            IFluentRegistration fluentRegistration = this._containerBuilder.Register(
                LoadType(contractTypeName),
                LoadType(implementationTypeName));

            if (registration.Arguments.Count > 0)
            {
                var namedArguments = new Dictionary<string, object>();

                foreach (Argument argument in registration.Arguments)
                {
                    string argumentTypeName = this.ResolveAlias(argument.Type);
                    Type argumentType = typeof(string);

                    if (argumentTypeName != null)
                    {
                        argumentType = this.LoadType(argumentTypeName);
                    }

                    if (!string.IsNullOrEmpty(argument.Name))
                    {
                        namedArguments.Add(argument.Name, argument.Value.ToOrDefault(argumentType));
                    }
                    else
                    {
                        fluentRegistration.WithArguments(argument.Value.ToOrDefault(argumentType));
                    }
                }

                if (namedArguments.Count > 0)
                {
                    fluentRegistration.WithNamedArguments(namedArguments);
                }
            }

            string lifecycleTypeName = this.ResolveAlias(registration.Lifecycle);

            if (string.IsNullOrEmpty(lifecycleTypeName) && !String.IsNullOrEmpty(this._configuration.DefaultLifecycle))
            {
                lifecycleTypeName = this.ResolveAlias(this._configuration.DefaultLifecycle);
            }

            if (lifecycleTypeName != null)
            {
                Type lifecycleType = this.LoadType(lifecycleTypeName);

                if (lifecycleType != null)
                {
                    fluentRegistration.ControlledBy(lifecycleType);
                }
            }
        }

        /// <summary>
        /// Resolves a potential alias to the full qualified type string.
        /// </summary>
        /// <param name="rawType">The alias or full qualified type string.</param>
        /// <returns>The rawType, if no alias found, otherwise the full qualified type string according to the alias data.</returns>
        private string ResolveAlias(string rawType)
        {
            const string leadingBracket = "{";
            const string followingBracket = "}";

            // Replace for alternative generic syntax.
            if (rawType != null && rawType.IndexOf(leadingBracket) > -1)
            {
                rawType = rawType.Replace(leadingBracket, "`");
                rawType = rawType.Replace(followingBracket, "");
            }

            if (rawType != null && !rawType.Contains("."))
            {
                TypeAlias typeAlias = this._configuration.TypeAliases.Find(a => FindAlias(a, rawType));

                if (typeAlias != null)
                {
                    return typeAlias.Type;
                }
            }

            return rawType;
        }

        /// <summary>
        /// Finds an alias. Supports multiple alias pro alias instance.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <param name="rawType">The rawType.</param>
        /// <returns><value>true</value> if the alias was found, otherwise <value>false</value>.</returns>
        private bool FindAlias(TypeAlias alias, string rawType)
        {
            string[] aliase = alias.Alias.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            return aliase.Any(a => a.Trim() == rawType.Trim());
        }

        /// <summary>
        /// Loads type according to the registration.
        /// </summary>
        /// <param name="typeName">The type to register.</param>
        /// <returns>The type to register.</returns>
        private Type LoadType(string typeName)
        {
            Type type = Type.GetType(typeName);

            // Type could not be loaded, throw exception with detailed message.
            if (type == null)
            {
                throw new XamlRegistrationException(
                    Resources.CouldNotLoadTypeFormat.FormatWith(typeName));
            }

            // Return type to the caller.
            return type;
        }
    }
}