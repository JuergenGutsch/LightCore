using System;
using PeterBucher.AutoFunc.Fluent;

namespace PeterBucher.AutoFunc.Mapping
{
    public class MappingItem : IMappingItem
    {
        private Lifecycle _lifecycle = Lifecycle.Transient;

        public MappingItem(Type imlementationType)
        {
            this.ImplementationType = imlementationType;
        }

        public MappingItem(Type imlementationType, Lifecycle lifecycle)
            : this(imlementationType)
        {
            _lifecycle = lifecycle;
        }

        public Type ImplementationType
        {
            get;
            private set;
        }

        public Lifecycle Lifecycle
        {
            get;
            set;
        }

        public object Instance
        {
            get;
            set;
        }

        public ILifecycleFluent LifecycleFluent
        {
            get
            {
                return new LifecycleFluent(this);
            }
        }
    }
}