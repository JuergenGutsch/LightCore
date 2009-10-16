using System;
using LightCore.TestTypes;

using NUnit.Framework;

using System.Collections.Generic;

namespace LightCore.Configuration.Tests
{
    [TestFixture]
    public class FooTests
    {
        [Test]
        public void Foo()
        {
            var configuration = new LightCoreConfiguration();

            configuration.Defaults = new LightCoreConfigurationDefaults();

            configuration.Defaults.DefaultAssembly = "LightCore.TestTypes";
            configuration.Defaults.DefaultContractNamespace = "LightCore.TestTypes";
            configuration.Defaults.DefaultImplementationNamespace = "LightCore.TestTypes";

            var registrations = new List<Registration>
                                    {
                                        new Registration
                                            {
                                                Arguments = String.Empty,
                                                ContractType = "IFoo",
                                                ImplementationType = "Foo",
                                            }
                                    };

            configuration.Registrations = registrations;

            var builder = new ContainerBuilder();

            RegistrationLoader.Instance.Register(builder, configuration);

            var container = builder.Build();

            var foo = container.Resolve<IFoo>();
        }
    }
}