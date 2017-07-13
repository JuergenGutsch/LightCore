using System;

namespace LightCore.ConsoleClient.Core.Writers
{
    /// <summary>
    /// Represents a writer that can write lines out to the console.
    /// </summary>
    public class ConsoleWriter : IWriter
    {
        /// <summary>
        /// Write a line to the console.
        /// </summary>
        /// <param name="text">The text.</param>
        public void WriteLine(string text)
        {
            Console.WriteLine(text);
        }
    }
}