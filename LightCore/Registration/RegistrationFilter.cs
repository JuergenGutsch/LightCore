namespace LightCore.Registration
{
    ///<summary>
    /// Represents a filter, that indicated whether to filter nothing (None),
    /// or filter a specific thing, like (ResolveAnything).
    ///</summary>
    internal enum RegistrationFilter
    {
        ///<summary>
        /// No Filter is set.
        ///</summary>
        None,

        ///<summary>
        /// Filter ResolveAnything.
        ///</summary>
        SkipResolveAnything
    }
}