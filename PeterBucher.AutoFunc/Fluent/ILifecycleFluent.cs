namespace PeterBucher.AutoFunc.Fluent
{
    public interface ILifecycleFluent
    {
        void AsSingleton();
        void AsTransient();
    }
}