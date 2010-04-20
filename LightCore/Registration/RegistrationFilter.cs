namespace LightCore.Registration
{
    ///<summary>
    /// Represents a filter for registration lookup.
    ///</summary>
    public enum RegistrationFilter
    {
        ///<summary>
        /// Search in all registrations, not in registration sources.
        ///</summary>
        AllContractRegistrations,

        ///<summary>
        /// Search only registration source.
        ///</summary>
        RegistrationSourceOnly
    }
}