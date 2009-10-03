namespace LightCore.ConsoleClient
{
    /// <summary>
    /// Represents the contract for a writer that can write lines out to something.
    /// </summary>
    public interface IWriter
    {
        /// <summary>
        /// Write a line to something.
        /// </summary>
        /// <param name="text">The text.</param>
        void WriteLine(string text);
    }
}