using System.Linq;
using LightCore.Lifecycle;
using LightCore.TestTypes;
using NUnit.Framework;

namespace LightCore.Tests
{
    [TestFixture]
    public class ContainerTests
    {
        [Test]
        public void ContainerBuilder_can_initialize_controller()
        {
            var builder = new ContainerBuilder();
            var container = builder.Build();

            Assert.IsNotNull(container);
        }

        [Test]
        public void Container_can_resolve_all_instances_based_on_a_predicate()
        {
            var builder = new ContainerBuilder();

            builder.Register<IFoo, Foo>().ControlledBy<TransientLifecycle>();
            builder.Register<IBar, Bar>().ControlledBy<TransientLifecycle>();
            builder.Register<ILorem, TestLorem>();

            var container = builder.Build();

            var allInstances = container.ResolveAll(r => r.Lifecycle is TransientLifecycle);

            Assert.AreEqual(2, allInstances.Count());
        }
    }
}