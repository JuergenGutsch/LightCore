using PeterBucher.AutoFunc.Mapping;

namespace PeterBucher.AutoFunc.Fluent
{
    /// <summary>
    /// Represents the fluent interface for lifecycles.
    /// </summary>
    public class LifecycleFluent : ILifecycleFluent
    {
        /// <summary>
        /// The current mapping item.
        /// </summary>
        private readonly IMappingItem _mappingItem;

        /// <summary>
        /// Initializes a new instance of <see cref="LifecycleFluent" /> type.
        /// </summary>
        /// <param name="mappingItem">The current mapping item.</param>
        public LifecycleFluent(IMappingItem mappingItem)
        {
            this._mappingItem = mappingItem;
        }

        /// <summary>
        /// Treat the current registration to singleton lifecycle.
        /// </summary>
        public void AsSingleton()
        {
            this._mappingItem.Lifecycle = Lifecycle.Singleton;
        }

        /// <summary>
        /// Treat the current registration to transient lifecycle.
        /// </summary>
        public void AsTransient()
        {
            this._mappingItem.Lifecycle = Lifecycle.Transient;
        }
    }
}