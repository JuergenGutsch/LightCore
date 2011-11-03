namespace LightCore
{
    ///<summary>
    /// Represents an anonymous argument,
    /// for registering named arguments.
    ///</summary>
    public class AnonymousArgument
    {
        ///<summary>
        /// The anonymous type.
        /// 
        /// e.g. new { arg1 = true, arg2 = "Peter" }.
        ///</summary>
        public object AnonymousType
        {
            get;
            set;
        }

        ///<summary>
        /// Initializes a new instance of <see cref="AnonymousArgument" />.
        ///</summary>
        ///<param name="anonymousNamedArguments">The anonymous type, e.g. new { arg1 = true, arg2 = "Peter" }.</param>
        public AnonymousArgument(object anonymousNamedArguments)
        {
            this.AnonymousType = anonymousNamedArguments;
        }
    }
}