using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using PeterBucher.AutoFunc.Build;
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
            var builder = new ContainerBuilder();

            builder.Register<IScreen, WelcomeScreen>().WithArguments("Test");

            // Uncomment this and comment ConsoleWriter below.
            //container.Register<IWriter, DebugWindowWriter>();
            builder.Register<IWriter, ConsoleWriter>();
            builder.Register<IScreen, WelcomeScreen>()
                .WithArguments("Hello World, it works")
                .WithName("NamedScreen");

            var container = builder.Build();

            int iterations = 1000;
            var screens = new List<IScreen>();

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            for (int i = 0; i < iterations; i++)
            {
                screens.Add(container.Resolve<IScreen>());
            }

            stopWatch.Stop();

            IWriter writer = screens.First().Writer;
            writer.WriteLine("Hello World, it works!");

            var namedScreen = container.ResolveNamed<IScreen>("NamedScreen");
            namedScreen.WriteText();

            Console.WriteLine("{0}ms for {1} iterations", stopWatch.ElapsedMilliseconds, iterations);

            // Waiting for user input.
            Console.Read();
        }
    }
}