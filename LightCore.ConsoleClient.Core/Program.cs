using System;
using LightCore.Configuration;
using LightCore.TestTypes;

namespace LightCore.ConsoleClient.Core
{
    public class Program
    {
        public void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            var module = new JsonRegistrationModule();

            builder.Register("Test");

            builder.RegisterModule(module);

            builder.Register<IBar, Bar>();

            var container = builder.Build();

            var func = container.Resolve<Func<string>>();
            var test = func();

            var namedScreen = container.Resolve<WelcomeScreen>();
            namedScreen.WriteText();

        }
    }
}