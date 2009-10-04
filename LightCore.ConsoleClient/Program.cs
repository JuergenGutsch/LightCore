using System;

using LightCore.Configuration;

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
            // Instantiate the container and register some dependencies.
            var builder = new ContainerBuilder();

            // Uncomment this and comment ConsoleWriter below.
            //builder.Register<IWriter, DebugWindowWriter>();
            //builder.Register<IWriter, ConsoleWriter>();

            //builder.Register<IScreen, WelcomeScreen>()
            //    .WithArguments("Hello World, it works")
            //    .WithName("NamedScreen");

            var module = new XamlRegistrationModule();

            builder.RegisterModule(module);


            var container = builder.Build();

            var namedScreen = container.Resolve<IScreen>("NamedScreen");
            namedScreen.WriteText();

            Console.Read();
        }
    }
}