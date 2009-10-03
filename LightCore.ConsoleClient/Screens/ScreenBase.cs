namespace LightCore.ConsoleClient.Screens
{
    /// <summary>
    /// Represents the base implementation for a screen.
    /// </summary>
    public abstract class ScreenBase : IScreen
    {
        /// <summary>
        /// The writer.
        /// </summary>
        private readonly IWriter _writer;

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
            this._writer = writer;
        }

        /// <summary>
        /// The writer.
        /// </summary>
        public IWriter Writer
        {
            get
            {
                return this._writer;
            }
        }

        /// <summary>
        /// Executes the screen.
        /// </summary>
        public abstract void Execute();

        /// <summary>
        /// Writes the <see cref="IScreen.Text" /> property to the current writer.
        /// </summary>
        public void WriteText()
        {
            this.Writer.WriteLine(this.Text);
        }
    }
}