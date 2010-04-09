using System;

using LightCore.Configuration;
using LightCore.ConsoleClient.Screens;

namespace LightCore.ConsoleClient
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
            var builder = new ContainerBuilder();

            var module = new XamlRegistrationModule();

            builder.RegisterModule(module);


            var container = builder.Build();

            var namedScreen = container
                .Resolve<WelcomeScreen>("Hello World");

            namedScreen.WriteText();

            Console.Read();
        }
    }
}