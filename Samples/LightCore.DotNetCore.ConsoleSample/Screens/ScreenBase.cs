namespace LightCore.ConsoleClient.Core.Screens
{
    /// <summary>
    /// Represents the base implementation for a screen.
    /// </summary>
    public abstract class ScreenBase : IScreen
    {
        /// <summary>
        /// The text.
        /// </summary>
        public string Text
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ScreenBase" />.
        /// </summary>
        /// <param name="writer">The writer.</param>
        protected ScreenBase(IWriter writer)
        {
            Writer = writer;
        }

        /// <summary>
        /// The writer.
        /// </summary>
        public IWriter Writer { get; }

        /// <summary>
        /// Executes the screen.
        /// </summary>
        public abstract void Execute();

        /// <summary>
        /// Writes the <see cref="IScreen.Text" /> property to the current writer.
        /// </summary>
        public void WriteText()
        {
            Writer.WriteLine(Text);
        }
    }
}