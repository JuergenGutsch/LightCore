using System;

using LightCore.Configuration.Exceptions;
using LightCore.Configuration.Properties;
using LightCore.Fluent;

namespace LightCore.Configuration
{
    ///<summary>
    ///</summary>
    public class  RegistrationLoader
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
            foreach (var registration in configuration.Registrations)
            {
                IFluentRegistration fluentRegistration = containerBuilder.Register(
                    this.LoadType(configuration, registration.ContractType,
                                   configuration.Defaults.DefaultContractNamespace),
                    this.LoadType(configuration, registration.ImplementationType,
                                   configuration.Defaults.DefaultImplementationNamespace));

                if (!String.IsNullOrEmpty(registration.Name))
                {
                    fluentRegistration.WithName(registration.Name);
                }

                if (!String.IsNullOrEmpty(registration.Arguments))
                {
                    var arguments = registration.Arguments.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    fluentRegistration.WithArguments(arguments);
                }
            }
        }

        /// <summary>
        /// Loads type according to the registration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="typeName">The type to register.</param>
        /// <param name="defaultNamespace">The default namespace.</param>
        /// <returns>The type to register.</returns>
        private Type LoadType(LightCoreConfiguration configuration, string typeName, string defaultNamespace)
        {
            string typeIdentifier = typeName;

            // Add default namespace, if needed.
            if (!typeIdentifier.Contains("."))
            {
                typeIdentifier = string.Concat(defaultNamespace, string.Concat(".", typeIdentifier));
            }

            // Add default assembly, if needed.
            if (!typeIdentifier.Contains(","))
            {
                typeIdentifier += string.Concat(", ", configuration.Defaults.DefaultAssembly);
            }

            Type type = Type.GetType(typeIdentifier);

            // Type could not be loaded, throw exception with detailed message.
            if (type == null)
            {
                throw new XamlRegistrationException(
                    Resources.CouldNotLoadTypeFormat.FormatWith(typeName, defaultNamespace, typeIdentifier));
            }

            // Return type to the caller.
            return type;
        }
    }
}