using System;

using PeterBucher.AutoFunc.Fluent;

namespace PeterBucher.AutoFunc.Mapping
{
    /// <summary>
    /// Represents a mapping item.
    /// </summary>
    public class MappingItem : IMappingItem
    {
        /// <summary>
        /// The lifecycle for the current mapping item.
        /// </summary>
        private Lifecycle _lifecycle = Lifecycle.Transient;

        /// <summary>
        /// Creates a new instance of <see cref="MappingItem" />.
        /// </summary>
        /// <param name="imlementationType">The implementation type as <see cref="Type" />.</param>
        public MappingItem(Type imlementationType)
        {
            this.ImplementationType = imlementationType;
        }

        /// <summary>
        /// Creates a new instance of <see cref="MappingItem" />.
        /// </summary>
        /// <param name="imlementationType">The implementation type as <see cref="Type" />.</param>
        /// <param name="lifecycle">The lifecycle.</param>
        public MappingItem(Type imlementationType, Lifecycle lifecycle)
            : this(imlementationType)
        {
            _lifecycle = lifecycle;
        }

        /// <summary>
        /// Gets the implementation type as <see cref="Type" />.
        /// </summary>
        public Type ImplementationType
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the lifecycle of the mapping.
        /// </summary>
        public Lifecycle Lifecycle
        {
            get;
            set;
        }

        /// <summary>
        /// Gets and sets the current instance of this mapping.
        /// </summary>
        public object Instance
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the current fluent interface instance.
        /// </summary>
        public ILifecycleFluent LifecycleFluent
        {
            get
            {
                return new LifecycleFluent(this);
            }
        }
    }
}