using System;
using LightCore.Configuration;
using LightCore.ConsoleClient.Core.Screens;
using LightCore.TestTypes;

namespace LightCore.ConsoleClient.Core
{
    public class Program
    {
        public void Main(string[] args)
        {
            var builder = new ContainerBuilder();

#if !DNXCORE50
            var module = new JsonRegistrationModule();
#else
            var module = new JsonRegistrationModule(null);
#endif
            builder.Register("Test");

            builder.RegisterModule(module);

            builder.Register<IBar, Bar>();

            var container = builder.Build();

            var func = container.Resolve<Func<string>>();
            var test = func();

            var namedScreen = container.Resolve<WelcomeScreen>();
            namedScreen.WriteText();

            Console.ReadKey();
        }
    }
}