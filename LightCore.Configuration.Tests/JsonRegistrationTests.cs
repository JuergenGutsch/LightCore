using FluentAssertions;
using LightCore.TestTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace LightCore.Configuration.Tests
{
    public class JsonRegistrationTests
    {
        [Fact]
        public void ConfigDoesNotExist()
        {
            var builder = new ContainerBuilder();
            Action act = () => { new JsonRegistrationModule("DarkCore.json"); };

            act.ShouldThrow<FileNotFoundException>();
        }

        [Fact]
        public void Read_simple_JSON_configuration_should_work()
        {
            lock (Locker.Lock)
            {
                var builder = new ContainerBuilder();
                var module = new JsonRegistrationModule("SimpleLightCore.json");
                builder.RegisterModule(module);
                var container = builder.Build();

                var foo = container.Resolve<IFoo>();
                var bar = container.Resolve<IBar>();

                foo.Should().NotBeNull();
                bar.Should().NotBeNull();
                foo.Should().BeOfType<XmlFoo>();
                bar.Should().BeOfType<XmlBar>();
            }
        }



        [Fact]
        public void Can_resolve_active_groups_configuration()
        {
            lock (Locker.Lock)
            {                
                var builder = new ContainerBuilder();
                var module = new JsonRegistrationModule("GroupedLightCore.json");
                builder.RegisterModule(module);
                var container = builder.Build();
                
                var foo = container.Resolve<IFoo>();
                var bar = container.Resolve<IBar>();
                var lorem = container.Resolve<ILorem>();

                foo.Should().NotBeNull();
                foo.Should().BeOfType<XmlFoo>();
                bar.Should().NotBeNull();
                bar.Should().BeOfType<XmlBar>();
                lorem.Should().NotBeNull();
                lorem.Should().BeOfType<TestLorem>();
            }
        }
    }
}
