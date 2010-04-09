using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using LightCore.Registration;

namespace LightCore.Activation.Components
{
    internal class ConstructorSelector
    {
        public ConstructorInfo SelectConstructor(Func<Type, bool> dependencyParameterSelector, IEnumerable<ConstructorInfo> constructors, ArgumentContainer arguments, ArgumentContainer runtimeArguments)
        {
            var constructorsWithParameters = constructors.OrderByDescending(constructor => constructor.GetParameters().Length);

            ConstructorInfo finalConstructor = constructorsWithParameters.Last();

            if (constructorsWithParameters.Count() == 1)
            {
                return finalConstructor;
            }

            // Loop througth all constructors, from the most to the least parameters.
            foreach (ConstructorInfo constructorCandidate in constructorsWithParameters)
            {
                ParameterInfo[] parameters = constructorCandidate.GetParameters();
                var dependencyParameters = parameters.Where(p => dependencyParameterSelector(p.ParameterType));

                if (arguments == null)
                {
                    arguments = new ArgumentContainer();
                }

                if(runtimeArguments == null)
                {
                    runtimeArguments = new ArgumentContainer();
                }

                // Parameters and registered dependencies match.
                if (arguments.CountOfAllArguments + runtimeArguments.CountOfAllArguments == 0 && parameters.Length == dependencyParameters.Count())
                {
                    finalConstructor = constructorCandidate;
                    break;
                }

                if (arguments.CountOfAllArguments > 0 || runtimeArguments.CountOfAllArguments > 0)
                {
                    if (arguments.CountOfAllArguments + runtimeArguments.CountOfAllArguments >= parameters.Count() - dependencyParameters.Count())
                    {
                        bool canSupply = true;

                        foreach (ParameterInfo parameter in parameters)
                        {
                            bool dependenciesCanSupplyValue = dependencyParameters.Contains(parameter);
                            bool argumentsCanSupplyValue = arguments.CanSupplyValue(parameter);
                            bool runtimeArgumentsCanSupplyValue = runtimeArguments.CanSupplyValue(parameter);

                            if (!(dependenciesCanSupplyValue || argumentsCanSupplyValue || runtimeArgumentsCanSupplyValue))
                            //if (!dependenciesCanSupplyValue || (!argumentsCanSupplyValue || !runtimeArgumentsCanSupplyValue))
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