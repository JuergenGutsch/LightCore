using System.Diagnostics;

namespace LightCore.ConsoleClient.Writers
{
    /// <summary>
    /// Represents a writer that can write lines out to the debug window.
    /// </summary>
    public class DebugWriter : IWriter
    {
        /// <summary>
        /// Write a line to the debug window.
        /// </summary>
        /// <param name="text">The text.</param>
        public void WriteLine(string text)
        {
            Debug.WriteLine(text);
        }
    }
}