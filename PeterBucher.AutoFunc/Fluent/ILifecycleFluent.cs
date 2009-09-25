namespace PeterBucher.AutoFunc.Fluent
{
    /// <summary>
    /// Represents the fluent interface for lifecycles.
    /// </summary>
    public interface ILifecycleFluent
    {
        /// <summary>
        /// Treat the current registration to singleton lifecycle.
        /// </summary>
        void AsSingleton();

        /// <summary>
        /// Treat the current registration to transient lifecycle.
        /// </summary>
        void AsTransient();
    }
}