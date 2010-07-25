using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using LightCore.Registration;

namespace LightCore.Activation.Components
{
    /// <summary>
    /// Represents a collector for arguments.
    /// </summary>
    internal class ArgumentCollector : IArgumentCollector
    {
        /// <summary>
        /// Collect the arguments from given parameter types.
        /// </summary>
        /// <param name="dependencyResolver">The depenency resolver.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="resolutionContext">The resolution context.</param>
        /// <returns>The collected arguments.</returns>
        public object[] CollectArguments(Func<Type, object> dependencyResolver, ParameterInfo[] parameters, ResolutionContext resolutionContext)
        {
            var finalArguments = new List<object>();

            var dependencyParameters =
                parameters.Where(
                    p =>
                    resolutionContext.RegistrationContainer.IsRegistered(p.ParameterType) || resolutionContext.RegistrationContainer.IsSupportedByRegistrationSource(p.ParameterType, RegistrationFilter.SkipResolveAnything));

            Func<object, ParameterInfo, bool> argumentSelector = (argument, parameter) => argument.GetType() == parameter.ParameterType;

            var runtimeArguments = resolutionContext.RuntimeArguments;
            var arguments = resolutionContext.Arguments;

            // Priority from heighest: Runtime arguments -> named / anonymous, Arguments -> named / anonymous / depdendency parameters.
            foreach (ParameterInfo parameter in parameters)
            {
                ParameterInfo localParameter = parameter;

                if (runtimeArguments.NamedArguments != null && runtimeArguments.NamedArguments.ContainsKey(parameter.Name))
                {
                    finalArguments.Add(runtimeArguments.NamedArguments[parameter.Name]);
                    continue;
                }

                if (runtimeArguments.AnonymousArguments != null && runtimeArguments.AnonymousArguments.Any(argument => argumentSelector(argument, localParameter)))
                {
                    finalArguments.Add(runtimeArguments.AnonymousArguments.FirstOrDefault(argument => argumentSelector(argument, localParameter)));
                    continue;
                }

                if (arguments.NamedArguments != null && arguments.NamedArguments.ContainsKey(parameter.Name))
                {
                    finalArguments.Add(arguments.NamedArguments[parameter.Name]);
                    continue;
                }

                if (arguments.AnonymousArguments != null && arguments.AnonymousArguments.Any(argument => argumentSelector(argument, localParameter)))
                {
                    finalArguments.Add(arguments.AnonymousArguments.FirstOrDefault(argument => argumentSelector(argument, localParameter)));
                    continue;
                }

                if (dependencyParameters.Contains(parameter))
                {
                    finalArguments.Add(dependencyResolver(parameter.ParameterType));
                    continue;
                }
            }

            return finalArguments.ToArray();
        }
    }
}