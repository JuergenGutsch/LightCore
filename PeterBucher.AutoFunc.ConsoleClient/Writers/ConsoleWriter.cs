using System;

namespace PeterBucher.AutoFunc.ConsoleClient.Writers
{
    public class ConsoleWriter : IWriter
    {
        public void WriteLine(string text)
        {
            Console.WriteLine(text);
        }
    }
}