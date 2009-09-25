using System;
using System.Collections.Generic;

using System.Linq;
using System.Reflection;
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

        private object Resolve(Type typeOfContract)
        {
            IMappingItem mappingItem = _mappings.Where(m => m.Key.Equals(typeOfContract)).SingleOrDefault().Value;

            switch (mappingItem.Lifecycle)
            {
                case Lifecycle.Singleton:
                    if (mappingItem.Instance == null)
                    {
                        this.CreateInstanceFromType(mappingItem.ImplementationType);
                    }

                    return mappingItem.Instance;
            }

            return this.CreateInstanceFromType(mappingItem.ImplementationType);
        }

        public TContract Resolve<TContract>()
        {
            return (TContract)this.Resolve(typeof(TContract));
        }

        private object CreateInstanceFromType(Type implementationType)
        {
            ConstructorInfo[] constructors = implementationType.GetConstructors();
            if (constructors.Length == 0 || constructors.Length == 1 && constructors[0].GetParameters() == null)
            {
                return Activator.CreateInstance(implementationType);
            }

            ConstructorInfo constructorWithDependencies = constructors.OrderByDescending(
                delegate(ConstructorInfo c)
                {
                    var parameters = c.GetParameters();
                    return parameters == null || parameters.Count() == 0;
                }).First();

            List<object> parameterResults = new List<object>();

            foreach (var parameter in constructorWithDependencies.GetParameters())
            {
                parameterResults.Add(this.Resolve(parameter.ParameterType));
            }

            return constructorWithDependencies.Invoke(parameterResults.ToArray());
        }
    }
}