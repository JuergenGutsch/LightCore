using PeterBucher.AutoFunc.Fluent;

namespace PeterBucher.AutoFunc
{
    public interface IContainer
    {
        ILifecycleFluent Register<TContract, TImplementation>();
        TContract Resolve<TContract>();
    }
}