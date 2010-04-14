using System;
using System.Linq;

using LightCore.ExtensionMethods.System;
using LightCore.Registration;

namespace LightCore.ExtensionMethods.LightCore.Registration
{
    /// <summary>
    /// Represents extensionmethods for LightCore.Registration namespace.
    /// </summary>
    public static class RegistrationContainerExtensions
    {
        /// <summary>
        /// Checks whether a parameter is registered as anything (default, enumerable, generic.).
        /// </summary>
        /// <param name="source">The source registration container.</param>
        /// <param name="contractType">The contract type.</param>
        /// <returns></returns>
        internal static bool IsRegisteredAsAnything(this RegistrationContainer source, Type contractType)
        {
            return source.IsRegisteredContract(contractType) || source.IsRegisteredOpenGeneric(contractType) ||
                   source.IsRegisteredGenericEnumerable(contractType);
        }

        /// <summary>
        /// Checks whether a contract is type of IEnumerable{T}, where {T} is a registered contract.
        /// </summary>
        /// <param name="source">The source registration container.</param>
        /// <param name="contractType">The contract type.</param>
        /// <returns><true /> if the parameter is a registered type within an generic enumerable instance.</returns>
        internal static bool IsRegisteredGenericEnumerable(this RegistrationContainer source, Type contractType)
        {
            return contractType.IsGenericEnumerable()
                && source.IsRegisteredContract(contractType.GetGenericArguments().FirstOrDefault());
        }

        /// <summary>
        /// Checks whether an open generic type, taken from the closed type (contractType) is registered or not.
        /// (This makes possible to use open generic types and also closed generic types at once.
        /// </summary>
        /// <param name="source">The source registration container.</param>
        /// <param name="contractType">The type of the contract.</param>
        /// <returns><value>true</value> if the open generic type is registered, otherwise <value>false</value>.</returns>
        internal static bool IsRegisteredOpenGeneric(this RegistrationContainer source, Type contractType)
        {
            return contractType.IsGenericTypeDefinition || contractType.IsGenericType
                                                             &&
                                                             source.IsRegisteredContract(contractType.GetGenericTypeDefinition());
        }

        /// <summary>
        /// Determines whether a contracttype is registered or not.
        /// </summary>
        /// <param name="source">The source registration container.</param>
        /// <param name="contractType">The type of contract.</param>
        /// <returns><value>true</value> if an registration with the contracttype found, otherwise <value>false</value>.</returns>
        internal static bool IsRegisteredContract(this RegistrationContainer source, Type contractType)
        {
            if (contractType == null)
            {
                return false;
            }

            return source
                .Registrations
                .Values
                .Concat(source.DuplicateRegistrations)
                .Any(registration => registration.Key.ContractType == contractType);
        }
    }
}