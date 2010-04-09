using LightCore.TestTypes;

using NUnit.Framework;

using System.Collections.Generic;

namespace LightCore.Configuration.Tests
{
    [TestFixture]
    public class GroupConfigurationTests
    {
        [Test]
        public void Exception_is_thrown_when_a_given_active_group_was_not_found()
        {
            var configuration = new LightCoreConfiguration();
            configuration.ActiveRegistrationGroups = "test";

            configuration.RegistrationGroups = GetTestRegistrationGroups();

            var builder = new ContainerBuilder();

            Assert.Throws<ActiveGroupNotFoundException>(
                () => RegistrationLoader.Instance.Register(builder, configuration));
        }

        [Test]
        public void Can_register_group_configurations()
        {
            var configuration = new LightCoreConfiguration();
            configuration.ActiveRegistrationGroups = "Sql";

            configuration.RegistrationGroups = GetTestRegistrationGroups();

            var builder = new ContainerBuilder();

            RegistrationLoader.Instance.Register(builder, configuration);

            builder.Build();
        }

        [Test]
        public void Can_resolve_active_group_configuration()
        {
            var configuration = new LightCoreConfiguration();
            configuration.ActiveRegistrationGroups = "Xml";

            configuration.RegistrationGroups = GetTestRegistrationGroups();

            var builder = new ContainerBuilder();

            RegistrationLoader.Instance.Register(builder, configuration);

            var container = builder.Build();

            Assert.IsInstanceOf<XmlFoo>(container.Resolve<IFoo>());
        }

        [Test]
        public void Can_register_group_configurations_registered_by_code()
        {
            var builder = new ContainerBuilder();

            builder.ActiveRegistrationGroups = "Xml";

            builder.Register<IBar, XmlBar>().WithGroup("Xml");
            builder.Register<IFoo, XmlFoo>().WithGroup("Xml");

            builder.Register<IBar, SqlBar>().WithGroup("Sql");
            builder.Register<IFoo, SqlFoo>().WithGroup("Sql");

            var container = builder.Build();

            Assert.IsInstanceOf<XmlFoo>(container.Resolve<IFoo>());
        }

        [Test]
        public void Can_register_multiple_group_configurations_registered_by_code()
        {
            var builder = new ContainerBuilder();

            builder.ActiveRegistrationGroups = "Xml, Test";

            builder.Register<IBar, XmlBar>().WithGroup("Xml");
            builder.Register<IFoo, XmlFoo>().WithGroup("Xml");

            builder.Register<IBar, SqlBar>().WithGroup("Sql");
            builder.Register<IFoo, SqlFoo>().WithGroup("Sql");

            builder.Register<ILorem, Lorem>().WithGroup("Lalala");
            builder.Register<ILorem, TestLorem>().WithGroup("Test");

            var container = builder.Build();

            Assert.IsInstanceOf<XmlFoo>(container.Resolve<IFoo>());
            Assert.IsInstanceOf<TestLorem>(container.Resolve<ILorem>());
        }

        [Test]
        public void Can_register_multiple_group_configurations_registered_configuration()
        {
            var configuration = new LightCoreConfiguration();
            configuration.ActiveRegistrationGroups = "Xml, Test";

            configuration.RegistrationGroups = GetTestRegistrationGroups();

            var builder = new ContainerBuilder();

            RegistrationLoader.Instance.Register(builder, configuration);

            var container = builder.Build();

            Assert.IsInstanceOf<XmlFoo>(container.Resolve<IFoo>());
            Assert.IsInstanceOf<TestLorem>(container.Resolve<ILorem>());
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