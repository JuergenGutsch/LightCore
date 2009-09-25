using System;

using PeterBucher.AutoFunc.ConsoleClient.Screens;
using PeterBucher.AutoFunc.ConsoleClient.Writers;

namespace PeterBucher.AutoFunc.ConsoleClient
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Instantiate the container and register some dependencies.
            IContainer container = new AutoFuncContainer();
            container.Register<IScreen, WelcomeScreen>();
            container.Register<IWriter, ConsoleWriter>();

            // Uncomment this and comment ConsoleWriter above.
            //container.Register<IWriter, DebugWindowWriter>();

            // Get an instance of curent registered type for this contract.
            var screen = container.Resolve<IScreen>();
            
            // Execute the screen.
            screen.Execute();

            Console.Read();
        }
    }
}