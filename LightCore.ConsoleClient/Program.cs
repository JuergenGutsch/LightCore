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

            builder.RegisterModule(module);


            var container = builder.Build();

            var foo = container.Resolve<IFoo>();
            var repository = container.Resolve<IRepository<Foo, int>>();

            var namedScreen = container.Resolve<WelcomeScreen>();
            namedScreen.WriteText();

            Console.Read();
        }
    }
}