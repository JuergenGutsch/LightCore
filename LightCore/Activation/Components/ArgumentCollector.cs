using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LightCore.Activation.Components
{
    /// <summary>
    /// Represents a collector for arguments.
    /// </summary>
    internal class ArgumentCollector
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

            var dependencyParameters = parameters.Where(p => resolutionContext.Registrations.IsRegisteredOrSupportedContract(p.ParameterType));

            Func<object, ParameterInfo, bool> argumentSelector = (argument, parameter) => argument.GetType() == parameter.ParameterType;

            // Priority from heighest: Runtime arguments -> named / anonymous, Arguments -> named / anonymous / depdendency parameters.
            foreach (ParameterInfo parameter in parameters)
            {
                if (resolutionContext.RuntimeArguments.NamedArguments != null && resolutionContext.RuntimeArguments.NamedArguments.ContainsKey(parameter.Name))
                {
                    finalArguments.Add(resolutionContext.RuntimeArguments.NamedArguments[parameter.Name]);
                    continue;
                }

                if (resolutionContext.RuntimeArguments.AnonymousArguments != null && resolutionContext.RuntimeArguments.AnonymousArguments.Any(argument => argumentSelector(argument, parameter)))
                {
                    finalArguments.Add(resolutionContext.RuntimeArguments.AnonymousArguments.FirstOrDefault(argument => argumentSelector(argument, parameter)));
                    continue;
                }

                if (resolutionContext.Arguments.NamedArguments != null && resolutionContext.Arguments.NamedArguments.ContainsKey(parameter.Name))
                {
                    finalArguments.Add(resolutionContext.Arguments.NamedArguments[parameter.Name]);
                    continue;
                }

                if (resolutionContext.Arguments.AnonymousArguments != null && resolutionContext.Arguments.AnonymousArguments.Any(argument => argumentSelector(argument, parameter)))
                {
                    finalArguments.Add(resolutionContext.Arguments.AnonymousArguments.FirstOrDefault(argument => argumentSelector(argument, parameter)));
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