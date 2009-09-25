using System;
using System.Collections.Generic;

using System.Linq;
using PeterBucher.AutoFunc.Fluent;
using PeterBucher.AutoFunc.Mapping;

namespace PeterBucher.AutoFunc
{
    public class AutoFuncContainer : IContainer
    {
        private readonly IDictionary<Type, IMappingItem> _mappings;

        public AutoFuncContainer()
        {
            this._mappings = new Dictionary<Type, IMappingItem>();
        }

        public ILifecycleFluent Register<TContract, TImplementation>()
        {
            Type typeOfContract = typeof(TContract);
            MappingItem mappingItem;

            if (!this._mappings.ContainsKey(typeOfContract))
            {
                Type typeofImplementation = typeof(TImplementation);
                mappingItem = new MappingItem(typeofImplementation);

                this._mappings.Add(typeOfContract, mappingItem);

                return mappingItem.LifecycleFluent;
            }

            throw new Exception("mapping for this contract allready registered");
        }

        public TContract Resolve<TContract>()
        {
            Type typeOfContract = typeof(TContract);
            IMappingItem mappingItem = _mappings.Where(m => m.Key.Equals(typeOfContract)).SingleOrDefault().Value;

            switch (mappingItem.Lifecycle)
            {
                case Lifecycle.Singleton:
                    if (mappingItem.Instance == null)
                    {
                        this.CreateInstanceFromType<TContract>(mappingItem.ImplementationType);
                    }

                    return (TContract)mappingItem.Instance;
            }

            return this.CreateInstanceFromType<TContract>(mappingItem.ImplementationType);
        }

        private TContract CreateInstanceFromType<TContract>(Type implementationType)
        {
            return (TContract)Activator.CreateInstance(implementationType);
        }
    }
}