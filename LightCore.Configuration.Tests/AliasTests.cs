using System;
using System.Collections.Generic;
using FluentAssertions;
using LightCore.TestTypes;
using Xunit;

namespace LightCore.Configuration.Tests
{
    public class AliasTests
    {
        [Fact]
        public void Can_register_and_use_generics_with_configuration_api()
        {
            lock (Locker.Lock)
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

                var expected = new Guid("354c11f1-94e5-41b8-9a13-122e2df2b0c7");

                var actual = container.Resolve<IFoo>() as Foo;

                actual.Should().NotBeNull();
                actual.Arg4.Should().Be(expected);
            }
        }
    }
}