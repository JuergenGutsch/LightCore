using System.Collections.Generic;
using System.Reflection;

namespace LightCore.Activation.Components
{
    internal interface IConstructorSelector
    {
        /// <summary>
        /// Selects the right constructor for current context.
        /// </summary>
        /// <param name="constructors">The constructors.</param>
        /// <param name="resolutionContext">The resolution context.</param>
        /// <returns>The selected constructor.</returns>
        ConstructorInfo SelectConstructor(IEnumerable<ConstructorInfo> constructors, ResolutionContext resolutionContext);
    }
}