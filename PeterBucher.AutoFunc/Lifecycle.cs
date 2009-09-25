namespace PeterBucher.AutoFunc
{
    /// <summary>
    /// Represents the lifecycle for a registered type.
    /// </summary>
    public enum Lifecycle
    {
        /// <summary>
        /// New object for every resolve request.
        /// </summary>
        Transient,

        /// <summary>
        /// One object for all resolve requests.
        /// </summary>
        Singleton
    }
}