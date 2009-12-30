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
                    if (!registrationGroups.Any(g => g.Name.Trim() == group.Trim()))
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
            string implementationTypeName = this.ResolveAlias(registration.ImplementationType);

            IFluentRegistration fluentRegistration = this._containerBuilder.Register(
                LoadType(contractTypeName),
                LoadType(implementationTypeName));

            if (!String.IsNullOrEmpty(registration.Name))
            {
                fluentRegistration.WithName(registration.Name);
            }

            if (!String.IsNullOrEmpty(registration.Arguments))
            {
                var arguments = registration.Arguments.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                fluentRegistration.WithArguments(arguments);
            }
            else if (registration.Arguments == "")
            {
                fluentRegistration.WithArguments(new[] {""});
            }

            string lifecycleTypeName = this.ResolveAlias(registration.Lifecycle);

            if (String.IsNullOrEmpty(lifecycleTypeName) && !String.IsNullOrEmpty(this._configuration.DefaultLifecycle))
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
            if (rawType != null && !rawType.Contains("."))
            {
                TypeAlias typeAlias = this._configuration.TypeAliases.Find(a => a.Alias == rawType);

                if (typeAlias != null)
                {
                    return typeAlias.Type;
                }
            }

            return rawType;
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