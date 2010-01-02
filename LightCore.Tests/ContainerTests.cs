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
        public void Container_can_resolve_open_generic_types()
        {
            var builder = new ContainerBuilder();
            builder.Register(typeof (IRepository<>), typeof (Repository<>));

            var container = builder.Build();

            var fooRepository = container.Resolve<IRepository<Foo>>();

            Assert.NotNull(fooRepository);
            Assert.IsInstanceOf<IRepository<Foo>>(fooRepository);
        }

        [Test]
        public void Container_can_resolve_open_generic_types_with_few_arguments()
        {
            var builder = new ContainerBuilder();
            builder.Register(typeof(IRepository<,>), typeof(Repository<,>));

            var container = builder.Build();

            var fooRepository = container.Resolve<IRepository<Foo, int>>();

            Assert.NotNull(fooRepository);
            Assert.IsInstanceOf<IRepository<Foo, int>>(fooRepository);
        }

        [Test]
        public void Container_can_resolve_types_to_self_automatically()
        {
            var builder = new ContainerBuilder();
            var container = builder.Build();

            Foo resolvedInstance = container.Resolve<Foo>();

            Assert.IsNotNull(resolvedInstance);
        }

        [Test]
        public void IContainer_is_automatically_registered_when_container_was_build()
        {
            var builder = new ContainerBuilder();
            var container = builder.Build();

            var resolvedContainer = container.Resolve<IContainer>();

            Assert.NotNull(resolvedContainer);
            Assert.IsInstanceOf<IContainer>(resolvedContainer);
        }

        [Test]
        public void ContainerBuilder_can_initialize_container()
        {
            var builder = new ContainerBuilder();
            var container = builder.Build();

            Assert.IsNotNull(container);
        }

        [Test]
        public void Container_can_resolve_all_instances_based_on_a_predicate()
        {
            var builder = new ContainerBuilder();

            builder.Register<IFoo, Foo>()
                .ControlledBy<TransientLifecycle>();

            builder.Register<IFoo, Foo>()
                .ControlledBy<SingletonLifecycle>()
                .WithName("test");

            builder.Register<IBar, Bar>()
                .ControlledBy<TransientLifecycle>();

            builder.Register<ILorem, TestLorem>();

            var container = builder.Build();

            var allInstances = container.ResolveAll<IFoo>();

            Assert.AreEqual(2, allInstances.Count());
        }

        [Test]
        public void Container_can_resolve_enumerables_of_contract()
        {
            var builder = new ContainerBuilder();

            builder.Register<IBar>(c => new Bar());
            builder.Register<IBar>(c => new Bar()).WithName("Foo");
            builder.Register<EnumerableTest>();

            var container = builder.Build();

            var instance = container.Resolve<EnumerableTest>();

            Assert.IsNotNull(instance);
            Assert.AreEqual(2, instance.Bars.Count());
        }
    }
}