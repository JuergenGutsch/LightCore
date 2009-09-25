namespace PeterBucher.AutoFunc.ConsoleClient
{
    /// <summary>
    /// Represents the contract for a screen.
    /// </summary>
    public interface IScreen
    {
        /// <summary>
        /// The writer.
        /// </summary>
        IWriter Writer { get; }

        /// <summary>
        /// Executes the screen.
        /// </summary>
        void Execute();
    }
}