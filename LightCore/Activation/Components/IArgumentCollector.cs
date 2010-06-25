using System;
using System.Reflection;

namespace LightCore.Activation.Components
{
    internal interface IArgumentCollector
    {
        /// <summary>
        /// Collect the arguments from given parameter types.
        /// </summary>
        /// <param name="dependencyResolver">The depenency resolver.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="resolutionContext">The resolution context.</param>
        /// <returns>The collected arguments.</returns>
        object[] CollectArguments(Func<Type, object> dependencyResolver, ParameterInfo[] parameters, ResolutionContext resolutionContext);
    }
}