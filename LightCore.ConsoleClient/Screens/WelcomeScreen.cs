namespace PeterBucher.AutoFunc.ConsoleClient.Screens
{
    /// <summary>
    /// Represents the welcome screen.
    /// </summary>
    public class WelcomeScreen : ScreenBase
    {
        /// <summary>
        /// Initializes a new instance of <see cref="WelcomeScreen" />.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="text">The text.</param>
        public WelcomeScreen(IWriter writer, string text)
            : base(writer)
        {
            this.Text = text;
        }

        /// <summary>
        /// Executes the screen and writes hello world to the writer.
        /// </summary>
        public override void Execute()
        {
            this.Writer.WriteLine("Hello World, that works");
        }
    }
}