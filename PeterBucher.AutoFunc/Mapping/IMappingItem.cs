using System;
using PeterBucher.AutoFunc.Fluent;

namespace PeterBucher.AutoFunc.Mapping
{
    public interface IMappingItem
    {
        Type ImplementationType { get; }
        Lifecycle Lifecycle { get; set; }
        object Instance { get; }
        ILifecycleFluent LifecycleFluent { get; }
    }
}