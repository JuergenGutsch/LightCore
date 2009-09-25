namespace PeterBucher.AutoFunc.ConsoleClient.Screens
{
    public class WelcomeScreen : ScreenBase
    {
        public WelcomeScreen(IWriter writer)
            : base(writer)
        {

        }

        public override void Execute()
        {
            this.Writer.WriteLine("Hello World, that works");
        }
    }
}