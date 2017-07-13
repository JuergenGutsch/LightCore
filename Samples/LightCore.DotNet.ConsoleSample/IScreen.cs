namespace LightCore.ConsoleClient
{
    /// <summary>
    /// Represents the contract for a screen.
    /// </summary>
    public interface IScreen
    {
        /// <summary>
        /// The text.
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// The writer.
        /// </summary>
        IWriter Writer { get; }

        /// <summary>
        /// Executes the screen.
        /// </summary>
        void Execute();

        /// <summary>
        /// Writes the <see cref="Text" /> property to the current writer.
        /// </summary>
        void WriteText();
    }
}