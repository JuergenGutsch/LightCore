namespace PeterBucher.AutoFunc.ConsoleClient
{
    public interface IScreen
    {
        IWriter Writer { get; }
        void Execute();
    }
}