namespace PeterBucher.AutoFunc.Integrations.Web
{
    /// <summary>
    /// Represents an accessor for <see cref="IContainer" />.
    /// </summary>
    public interface IContainerAccessor
    {
        /// <summary>
        /// Gets the container.
        /// </summary>
        IContainer Container { get; }
    }
}