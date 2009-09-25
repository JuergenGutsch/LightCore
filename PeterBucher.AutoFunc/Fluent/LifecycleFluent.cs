using PeterBucher.AutoFunc.Mapping;

namespace PeterBucher.AutoFunc.Fluent
{
    public class LifecycleFluent : ILifecycleFluent
    {
        private readonly IMappingItem _mappingItem;

        public LifecycleFluent(IMappingItem mappingItem)
        {
            this._mappingItem = mappingItem;
        }

        public void AsSingleton()
        {
            this._mappingItem.Lifecycle = Lifecycle.Singleton;
        }

        public void AsTransient()
        {
            this._mappingItem.Lifecycle = Lifecycle.Transient;
        }
    }
}