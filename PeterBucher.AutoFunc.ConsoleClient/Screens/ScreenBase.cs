namespace PeterBucher.AutoFunc.ConsoleClient.Screens
{
    public abstract class ScreenBase : IScreen
    {
        private readonly IWriter _writer;

        public ScreenBase(IWriter writer)
        {
            this._writer = writer;
        }

        public IWriter Writer
        {
            get
            {
                return this._writer;
            }
        }

        public abstract void Execute();
    }
}