using System;

using NUnit.Framework;

using System.Collections.Generic;

using LightCore.TestTypes;

namespace LightCore.Configuration.Tests
{
    [TestFixture]
    public class AliasTests
    {
        [Test]
        public void Can_register_and_use_generics_with_configuration_api()
        {
            var configuration = new LightCoreConfiguration();
            var registrations = new List<Registration>
                                    {
                                        new Registration
                                            {
                                                ContractType = typeof(IFoo).AssemblyQualifiedName,
                                                ImplementationType = typeof(Foo).AssemblyQualifiedName,
                                                Arguments = new List<Argument>
                                                                {
                                                                    new Argument
                                                                        {
                                                                            Type = "Guid",
                                                                            Value = "354c11f1-94e5-41b8-9a13-122e2df2b0c7"
                                                                        }
                                                                }
                                            }
                                    };

            configuration.Registrations = registrations;

            var builder = new ContainerBuilder();

            RegistrationLoader.Instance.Register(builder, configuration);

            var container = builder.Build();

            Guid expected = new Guid("354c11f1-94e5-41b8-9a13-122e2df2b0c7");

            Assert.AreEqual(expected, ((Foo)container.Resolve<IFoo>()).Arg4);
        }
    }
}