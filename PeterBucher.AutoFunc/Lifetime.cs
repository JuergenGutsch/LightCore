namespace PeterBucher.AutoFunc
{
    /// <summary>
    /// Represents the LifeTime for a registered type.
    /// </summary>
    public enum LifeTime
    {
        /// <summary>
        /// New object for every resolve request.
        /// </summary>
        Transient,

        /// <summary>
        /// One object for all resolve requests in the same container.
        /// </summary>
        Singleton
    }
}