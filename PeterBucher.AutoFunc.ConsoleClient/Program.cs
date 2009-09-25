using System;

using PeterBucher.AutoFunc.ConsoleClient.Screens;
using PeterBucher.AutoFunc.ConsoleClient.Writers;

namespace PeterBucher.AutoFunc.ConsoleClient
{
    /// <summary>
    /// Represents the console test app.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main entry point for the program.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            // Instantiate the container and register some dependencies.
            IContainer container = new AutoFuncContainer();
            container.Register<IScreen, WelcomeScreen>();

            // Uncomment this and comment ConsoleWriter below.
            //container.Register<IWriter, DebugWindowWriter>();
            container.Register<IWriter, ConsoleWriter>();

            // Get an instance of curent registered type for this contract.
            var screen = container.Resolve<IScreen>();

            // Execute the screen.
            screen.Execute();

            // Waiting for user input.
            Console.Read();
        }
    }
}