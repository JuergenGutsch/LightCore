using System.Collections.Generic;

using Microsoft.Practices.ServiceLocation;

using NUnit.Framework;

namespace LightCore.CommonServiceLocator.Tests
{
    [TestFixture]
    public class LightCoreAdapterTests
    {
        [Test]
        public void Can_resolve_one_instance()
        {
            var builder = new ContainerBuilder();

            builder.Register<IDictionary<string, string>>(c => new Dictionary<string, string>());

            var container = builder.Build();

            IServiceLocator locator = new LightCoreAdapter(container);

            var dictionary = locator.GetInstance(typeof(IDictionary<string, string>));

            Assert.IsNotNull(dictionary);
            Assert.IsInstanceOf<IDictionary<string, string>>(dictionary);
        }

        [Test]
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

            Assert.IsNotNull(instances);
            Assert.IsInstanceOf<IEnumerable<object>>(instances);
        }

        [Test]
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

            var instances = locator.GetAllInstances<IList<string>>();

            Assert.IsNotNull(instances);
            Assert.IsInstanceOf<IEnumerable<IList<string>>>(instances);
        }
    }
}