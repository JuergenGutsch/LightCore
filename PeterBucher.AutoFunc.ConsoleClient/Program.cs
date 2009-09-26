using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            
            builder.Register<IScreen, WelcomeScreen>();

            // Uncomment this and comment ConsoleWriter below.
            //container.Register<IWriter, DebugWindowWriter>();
            builder.Register<IWriter, ConsoleWriter>();

            var container = builder.Build();

            // Get an instance of curent registered type for this contract.

            Stopwatch stopWatch = new Stopwatch();

            int iterations = 1000;

            var screens = new List<IScreen>();

            stopWatch.Start();

            for (int i = 0; i < iterations; i++)
            {
                screens.Add(container.Resolve<IScreen>());
            }

            stopWatch.Stop();

            IWriter writer = screens.First().Writer;
            writer.WriteLine("Hello World, it works!");

            Console.WriteLine("{0}ms for {1} iterations", stopWatch.ElapsedMilliseconds, iterations);

            // Waiting for user input.
            Console.Read();
        }
    }
}