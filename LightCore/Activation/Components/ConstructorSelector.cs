using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using LightCore.ExtensionMethods.System;
using LightCore.Properties;

namespace LightCore.Activation.Components
{
    /// <summary>
    /// Represents the constructor search stratetgy.
    /// </summary>
    internal class ConstructorSelector : IConstructorSelector
    {
        /// <summary>
        /// Selects the right constructor for current context.
        /// </summary>
        /// <param name="constructors">The constructors.</param>
        /// <param name="resolutionContext">The resolution context.</param>
        /// <returns>The selected constructor.</returns>
        public ConstructorInfo SelectConstructor(IEnumerable<ConstructorInfo> constructors, ResolutionContext resolutionContext)
        {
            var constructorsWithParameters = constructors.OrderByDescending(constructor => constructor.GetParameters().Length);

            ConstructorInfo finalConstructor = constructorsWithParameters.LastOrDefault();

            if (finalConstructor == null)
            {
                throw new ResolutionFailedException(Resources.NoConstructorAvailableForType.FormatWith(resolutionContext.Registration.ImplementationType));
            }

            if (constructorsWithParameters.Count() == 1 && constructorsWithParameters.First().GetParameters().Length == 0)
            {
                return finalConstructor;
            }

            // Loop througth all constructors, from the most to the least parameters.
            foreach (ConstructorInfo constructorCandidate in constructorsWithParameters)
            {
                ParameterInfo[] parameters = constructorCandidate.GetParameters();
                var dependencyParameters = parameters
                    .Where(p => resolutionContext.RegistrationContainer.IsRegistered(p.ParameterType)
                                ||
                                resolutionContext.RegistrationContainer.IsSupportedByRegistrationSource(p.ParameterType));

                // Parameters and registered dependencies match.
                if (resolutionContext.Arguments.CountOfAllArguments + resolutionContext.RuntimeArguments.CountOfAllArguments == 0 && parameters.Length == dependencyParameters.Count())
                {
                    finalConstructor = constructorCandidate;
                    break;
                }

                if (resolutionContext.Arguments.CountOfAllArguments > 0 || resolutionContext.RuntimeArguments.CountOfAllArguments > 0)
                {
                    if (resolutionContext.Arguments.CountOfAllArguments + resolutionContext.RuntimeArguments.CountOfAllArguments >= parameters.Count() - dependencyParameters.Count())
                    {
                        bool canSupply = true;

                        foreach (ParameterInfo parameter in parameters)
                        {
                            bool dependenciesCanSupplyValue = dependencyParameters.Contains(parameter);
                            bool argumentsCanSupplyValue = resolutionContext.Arguments.CanSupplyValue(parameter);
                            bool runtimeArgumentsCanSupplyValue = resolutionContext.RuntimeArguments.CanSupplyValue(parameter);

                            if (!(dependenciesCanSupplyValue || argumentsCanSupplyValue || runtimeArgumentsCanSupplyValue))
                            {
                                canSupply = false;
                            }
                        }

                        if (canSupply)
                        {
                            finalConstructor = constructorCandidate;
                            break;
                        }
                    }
                }
            }

            return finalConstructor;
        }
    }
}