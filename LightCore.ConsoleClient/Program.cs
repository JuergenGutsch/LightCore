using System;

using LightCore.Configuration;
using LightCore.ConsoleClient.Screens;
using LightCore.TestTypes;

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

            builder.Register("Test");

            builder.RegisterModule(module);

            builder.Register<IBar, Bar>();

            var container = builder.Build();

            var func = container.Resolve<Func<string>>();
            var test = func();

            var namedScreen = container.Resolve<WelcomeScreen>();
            namedScreen.WriteText();


            Console.Read();
        }
    }
}