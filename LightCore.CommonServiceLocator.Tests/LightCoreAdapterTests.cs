using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.Practices.ServiceLocation;

using Xunit;

namespace LightCore.CommonServiceLocator.Tests
{
    public class LightCoreAdapterTests
    {
        [Fact]
        public void Can_resolve_one_instance()
        {
            var builder = new ContainerBuilder();

            builder.Register<IDictionary<string, string>>(c => new Dictionary<string, string>());

            var container = builder.Build();

            IServiceLocator locator = new LightCoreAdapter(container);

            var dictionary = locator.GetInstance(typeof(IDictionary<string, string>));

            dictionary.Should().NotBeNull();
            dictionary.Should().BeAssignableTo<IDictionary<string, string>>();
        }

        [Fact]
        public void Can_resolve_all_instances_of_type_object()
        {
            var builder = new ContainerBuilder();

            for (int i = 0; i < 10; i++)
            {
                builder.Register(c => new object());
            }

            var container = builder.Build();

            IServiceLocator locator = new LightCoreAdapter(container);

            var instances = locator.GetAllInstances(typeof(object));


            instances.Should().NotBeNull();
            instances.Should().BeAssignableTo<IEnumerable<object>>();
        }

        [Fact]
        public void Can_resolve_all_instances_generic()
        {
            var builder = new ContainerBuilder();

            for (int i = 0; i < 10; i++)
            {
                builder
                    .Register(c => new List<string>());
            }

            var container = builder.Build();

            IServiceLocator locator = new LightCoreAdapter(container);

            var instances = locator.GetAllInstances<List<string>>().ToList();

            instances.Should().NotBeNull();
            instances.Should().BeAssignableTo<List<List<string>>>();
        }
    }
}