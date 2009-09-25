using System;
using PeterBucher.AutoFunc.Fluent;

namespace PeterBucher.AutoFunc.Mapping
{
    /// <summary>
    /// Represents a mapping item.
    /// </summary>
    public interface IMappingItem
    {
        /// <summary>
        /// Gets the implementation type as <see cref="Type" />.
        /// </summary>
        Type ImplementationType { get; }

        /// <summary>
        /// Gets the lifecycle of the mapping.
        /// </summary>
        Lifecycle Lifecycle { get; set; }

        /// <summary>
        /// Gets the current instance of this mapping.
        /// </summary>
        object Instance { get; }

        /// <summary>
        /// Gets the current fluent interface instance.
        /// </summary>
        ILifecycleFluent LifecycleFluent { get; }
    }
}