namespace PeterBucher.AutoFunc.Tests.TestTypes
{
    public class NullLogger : ILogger
    {
        public void Log(string message)
        {
            // do nothing.
        }
    }
}