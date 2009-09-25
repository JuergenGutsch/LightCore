using System.Diagnostics;

namespace PeterBucher.AutoFunc.ConsoleClient.Writers
{
    public class DebugWindowWriter : IWriter
    {
        public void WriteLine(string text)
        {
            Debug.WriteLine(text);
        }
    }
}