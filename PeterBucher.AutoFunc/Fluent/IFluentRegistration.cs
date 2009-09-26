namespace PeterBucher.AutoFunc.Fluent
{
    /// <summary>
    /// Represents the fluent interface for registration.
    /// </summary>
    public interface IFluentRegistration
    {
        /// <summary>
        /// Treat the current registration to singleton lifecycle.
        /// </summary>
        IFluentRegistration AsSingleton();

        /// <summary>
        /// Treat the current registration to transient lifecycle.
        /// </summary>
        IFluentRegistration AsTransient();


        IFluentRegistration Named(string name);
    }
}