using System;
using System.Collections.Generic;
using System.Linq;

using LightCore.Configuration.Properties;
using LightCore.Fluent;

namespace LightCore.Configuration
{
    ///<summary>
    ///</summary>
    public class RegistrationLoader
    {
        private static readonly RegistrationLoader _instance = new RegistrationLoader();

        ///<summary>
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
        /// <param name="containerBuilder">The controllerbuilder.</param>
        /// <param name="configuration">The configuration</param>
        public void Register(IContainerBuilder containerBuilder, LightCoreConfiguration configuration)
        {
            IEnumerable<Registration> registrationsToRegister = configuration.Registrations;

            if(configuration.ActiveGroupConfigurations != null)
            {
                var activeGroups = configuration.ActiveGroupConfigurations.Split(new[] { ',' },
                                                                                 StringSplitOptions.RemoveEmptyEntries);

                registrationsToRegister = registrationsToRegister.Where(
                    r =>
                    string.IsNullOrEmpty(r.Group) ||
                    (!string.IsNullOrEmpty(r.Group) && activeGroups.Any(g => g.Trim() == r.Group)));
            }

            foreach (Registration registration in registrationsToRegister)
            {
                this.ProcessRegistration(containerBuilder, configuration, registration);
            }
        }

        private void ProcessRegistration(IContainerBuilder containerBuilder, LightCoreConfiguration configuration, Registration registration)
        {
            string contractType = registration.ContractType;
            string implementationType = registration.ImplementationType;

            if (!registration.ContractType.Contains("."))
            {
                TypeAlias typeAlias = configuration.TypeAliases.Find(a => a.Alias == contractType);

                if (typeAlias != null)
                {
                    contractType = typeAlias.Type;
                }
            }

            if (!registration.ImplementationType.Contains("."))
            {
                TypeAlias typeAlias = configuration.TypeAliases.Find(a => a.Alias == implementationType);

                if (typeAlias != null)
                {
                    implementationType = typeAlias.Type;
                }
            }

            IFluentRegistration fluentRegistration = containerBuilder.Register(
                LoadType(configuration, contractType),
                LoadType(configuration, implementationType));

            if (!String.IsNullOrEmpty(registration.Name))
            {
                fluentRegistration.WithName(registration.Name);
            }

            if (!String.IsNullOrEmpty(registration.Arguments))
            {
                var arguments = registration.Arguments.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                fluentRegistration.WithArguments(arguments);
            }

            Type lifecycleType = null;

            if (!String.IsNullOrEmpty(registration.Lifecycle))
            {
                TypeAlias typeAlias = configuration.TypeAliases.Find(a => a.Alias == registration.Lifecycle);

                if (typeAlias != null)
                {
                    lifecycleType = LoadType(configuration, typeAlias.Type);
                }
                else
                {
                    lifecycleType = LoadType(configuration, registration.Lifecycle);
                }
            }
            else
            {
                if (!String.IsNullOrEmpty(configuration.DefaultLifecycle))
                {
                    TypeAlias typeAlias =
                        configuration.TypeAliases.Find(alias => alias.Alias == configuration.DefaultLifecycle);

                    lifecycleType = LoadType(configuration, typeAlias.Type);
                }
            }

            if (lifecycleType != null)
            {
                fluentRegistration.ControlledBy(lifecycleType);
            }
        }

        /// <summary>
        /// Loads type according to the registration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="typeName">The type to register.</param>
        /// <returns>The type to register.</returns>
        private static Type LoadType(LightCoreConfiguration configuration, string typeName)
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