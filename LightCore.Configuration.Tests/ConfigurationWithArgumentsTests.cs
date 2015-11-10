using LightCore.TestTypes;

using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace LightCore.Configuration.Tests
{
    public class ConfigurationWithArgumentsTests
    {
        [Fact]
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

            var foo = container.Resolve<IFoo>() as Foo;

            foo.Should().NotBeNull();
            foo.Arg1.Should().Be("Peter");
            foo.Arg2.Should().Be(true);
        }
    }
}