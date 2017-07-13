using System;
using LightCore.TestTypes;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace LightCore.Configuration.Tests
{
    public class GroupConfigurationTests
    {
        [Fact]
        public void Exception_is_thrown_when_a_given_active_group_was_not_found()
        {
            lock (Locker.Lock)
            {
                var configuration = new LightCoreConfiguration
                {
                    ActiveRegistrationGroups = "test",
                    RegistrationGroups = GetTestRegistrationGroups()
                };


                var builder = new ContainerBuilder();

                Action act = () => RegistrationLoader.Instance.Register(builder, configuration);

                act.ShouldThrow<ActiveGroupNotFoundException>();
            }
        }

        [Fact]
        public void Can_register_group_configurations()
        {
            lock (Locker.Lock)
            {
                var configuration = new LightCoreConfiguration
                {
                    ActiveRegistrationGroups = "Sql",
                    RegistrationGroups = GetTestRegistrationGroups()
                };


                var builder = new ContainerBuilder();

                RegistrationLoader.Instance.Register(builder, configuration);

                builder.Build();
            }
        }

        [Fact]
        public void Can_resolve_active_group_configuration()
        {
            lock (Locker.Lock)
            {
                var configuration = new LightCoreConfiguration
                {
                    ActiveRegistrationGroups = "Xml",
                    RegistrationGroups = GetTestRegistrationGroups()
                };

                var builder = new ContainerBuilder();

                RegistrationLoader.Instance.Register(builder, configuration);

                var container = builder.Build();

                var actual = container.Resolve<IFoo>();

                actual.Should().BeOfType<XmlFoo>();
            }
        }

        [Fact]
        public void Can_register_group_configurations_registered_by_code()
        {
            var builder = new ContainerBuilder { ActiveRegistrationGroups = "Xml" };


            builder.Register<IBar, XmlBar>().WithGroup("Xml");
            builder.Register<IFoo, XmlFoo>().WithGroup("Xml");

            builder.Register<IBar, SqlBar>().WithGroup("Sql");
            builder.Register<IFoo, SqlFoo>().WithGroup("Sql");

            var container = builder.Build();
            var actual = container.Resolve<IFoo>();

            actual.Should().BeOfType<XmlFoo>();
        }

        [Fact]
        public void Can_register_multiple_group_configurations_registered_by_code()
        {
            var builder = new ContainerBuilder { ActiveRegistrationGroups = "Xml, Test" };


            builder.Register<IBar, XmlBar>().WithGroup("Xml");
            builder.Register<IFoo, XmlFoo>().WithGroup("Xml");

            builder.Register<IBar, SqlBar>().WithGroup("Sql");
            builder.Register<IFoo, SqlFoo>().WithGroup("Sql");

            builder.Register<ILorem, Lorem>().WithGroup("Lalala");
            builder.Register<ILorem, TestLorem>().WithGroup("Test");

            var container = builder.Build();

            var actualFoo = container.Resolve<IFoo>();
            var actualLorem = container.Resolve<ILorem>();

            actualFoo.Should().BeOfType<XmlFoo>();
            actualLorem.Should().BeOfType<TestLorem>();
        }

        [Fact]
        public void Can_register_multiple_group_configurations_registered_configuration()
        {
            lock (Locker.Lock)
            {
                var configuration = new LightCoreConfiguration
                {
                    ActiveRegistrationGroups = "Xml, Test",
                    RegistrationGroups = GetTestRegistrationGroups()
                };

                var builder = new ContainerBuilder();

                RegistrationLoader.Instance.Register(builder, configuration);

                var container = builder.Build();

                var actualFoo = container.Resolve<IFoo>();
                var actualLorem = container.Resolve<ILorem>();

                actualFoo.Should().BeOfType<XmlFoo>();
                actualLorem.Should().BeOfType<TestLorem>();
            }
        }

        private static List<RegistrationGroup> GetTestRegistrationGroups()
        {
            return new List<RegistrationGroup>
                       {
                           new RegistrationGroup
                               {
                                   Name = "Xml",
                                   Registrations = new List<Registration>
                                                       {
                                                           new Registration
                                                               {
                                                                   ContractType = typeof (IBar).AssemblyQualifiedName,
                                                                   ImplementationType =
                                                                       typeof (XmlBar).AssemblyQualifiedName,
                                                               },
                                                           new Registration
                                                               {
                                                                   ContractType = typeof (IFoo).AssemblyQualifiedName,
                                                                   ImplementationType =
                                                                       typeof (XmlFoo).AssemblyQualifiedName,
                                                               }
                                                       }
                               },
                           new RegistrationGroup
                               {
                                   Name = "Sql",
                                   Registrations = new List<Registration>
                                                       {
                                                           new Registration
                                                               {
                                                                   ContractType = typeof (IBar).AssemblyQualifiedName,
                                                                   ImplementationType =
                                                                       typeof (SqlBar).AssemblyQualifiedName,
                                                               },
                                                           new Registration
                                                               {
                                                                   ContractType = typeof (IFoo).AssemblyQualifiedName,
                                                                   ImplementationType =
                                                                       typeof (SqlFoo).AssemblyQualifiedName
                                                               }
                                                       }
                               },
                           new RegistrationGroup
                               {
                                   Name = "Lalala",
                                   Registrations = new List<Registration>
                                                       {
                                                           new Registration
                                                               {
                                                                   ContractType = typeof (ILorem).AssemblyQualifiedName,
                                                                   ImplementationType = typeof(Lorem).AssemblyQualifiedName
                                                               }
                                                       }
                               },
                           new RegistrationGroup
                               {
                                   Name = "Test",
                                   Registrations = new List<Registration>
                                                       {
                                                           new Registration
                                                               {
                                                                   ContractType = typeof (ILorem).AssemblyQualifiedName,
                                                                   ImplementationType = typeof(TestLorem).AssemblyQualifiedName
                                                               }
                                                       }
                               }
                       };
        }
    }
}