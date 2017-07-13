namespace LightCore.ConsoleClient.Core.Screens
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
            Text = text;
        }

        /// <summary>
        /// Executes the screen and writes hello world to the writer.
        /// </summary>
        public override void Execute()
        {
            Writer.WriteLine("Hello World, that works");
        }
    }
}