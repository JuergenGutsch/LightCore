using LightCore.TestTypes;

using NUnit.Framework;

using System.Collections.Generic;

namespace LightCore.Configuration.Tests
{
    [TestFixture]
    public class GroupConfigurationTests
    {
        [Test]
        public void Can_register_group_configurations()
        {
            var configuration = new LightCoreConfiguration();
            configuration.ActiveGroupConfigurations = "Sql";
            var registrations = GetTestGroupRegistrations();

            configuration.Registrations = registrations;

            var builder = new ContainerBuilder();

            RegistrationLoader.Instance.Register(builder, configuration);

            var container = builder.Build();
        }

        [Test]
        public void Can_resolve_active_group_configuration()
        {
            var configuration = new LightCoreConfiguration();
            configuration.ActiveGroupConfigurations = "Xml";
            var registrations = GetTestGroupRegistrations();

            configuration.Registrations = registrations;

            var builder = new ContainerBuilder();

            RegistrationLoader.Instance.Register(builder, configuration);

            var container = builder.Build();

            Assert.IsInstanceOf<XmlFoo>(container.Resolve<IFoo>());
        }

        private List<Registration> GetTestGroupRegistrations()
        {
            return new List<Registration>
                       {
                           new Registration
                               {
                                   Group = "Xml",
                                   ContractType = "LightCore.TestTypes.IBar, LightCore.TestTypes",
                                   ImplementationType = "LightCore.TestTypes.XmlBar, LightCore.TestTypes",
                               },
                           new Registration
                               {
                                   Group = "Xml",
                                   ContractType = "LightCore.TestTypes.IFoo, LightCore.TestTypes",
                                   ImplementationType = "LightCore.TestTypes.XmlFoo, LightCore.TestTypes",
                               },
                           new Registration
                               {
                                   Group = "Sql",
                                   ContractType = "LightCore.TestTypes.IBar, LightCore.TestTypes",
                                   ImplementationType = "LightCore.TestTypes.SqlBar, LightCore.TestTypes",
                               },
                           new Registration
                               {
                                   Group = "Sql",
                                   ContractType = "LightCore.TestTypes.IFoo, LightCore.TestTypes",
                                   ImplementationType = "LightCore.TestTypes.SqlFoo, LightCore.TestTypes",
                               }
                       };
        }
    }
}