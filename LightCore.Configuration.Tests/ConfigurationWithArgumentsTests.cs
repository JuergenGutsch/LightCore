using LightCore.TestTypes;

using NUnit.Framework;

using System.Collections.Generic;

namespace LightCore.Configuration.Tests
{
    [TestFixture]
    public class ConfigurationWithArgumentsTests
    {
        [Test]
        public void Can_register_and_use_named_arguments()
        {
            var configuration = new LightCoreConfiguration();
            var registrations = new List<Registration>
                                    {
                                        new Registration
                                            {
                                                ContractType = typeof (IFoo).AssemblyQualifiedName,
                                                ImplementationType = typeof (Foo).AssemblyQualifiedName,
                                                Arguments = new List<Argument>
                                                                {
                                                                    new Argument
                                                                        {
                                                                            Name = "arg1",
                                                                            Value = "Peter"
                                                                        },
                                                                    new Argument
                                                                        {
                                                                            Name = "arg2",
                                                                            Value = "true",
                                                                            Type = "System.Boolean"
                                                                        }
                                                                }
                                            }
                                    };

            configuration.Registrations = registrations;

            var builder = new ContainerBuilder();

            RegistrationLoader.Instance.Register(builder, configuration);

            var container = builder.Build();

            var foo = container.Resolve<IFoo>();

            Assert.IsNotNull(foo);
            Assert.AreEqual("Peter", ((Foo)foo).Arg1);
            Assert.AreEqual(true, ((Foo) foo).Arg2);
        }
    }
}