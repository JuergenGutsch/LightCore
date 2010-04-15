using System;
using System.Linq;

using LightCore.TestTypes;

using NUnit.Framework;

namespace LightCore.Tests.Integration
{
    [TestFixture]
    public class ContainerTests
    {
        [Test]
        public void Container_can_resolve_factories()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFoo, Foo>();

            var container = builder.Build();

            var fooFactory = container.Resolve<Func<IFoo>>();
            var fooOne = fooFactory();
            var fooTwo = fooFactory();

            Assert.IsNotNull(fooFactory);
            Assert.IsInstanceOf<Func<IFoo>>(fooFactory);

            Assert.IsNotNull(fooOne);
            Assert.IsInstanceOf<IFoo>(fooOne);

            Assert.IsNotNull(fooTwo);
            Assert.IsInstanceOf<IFoo>(fooTwo);
        }

        [Test]
        public void Container_can_resolve_factories_with_arguments()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFoo, Foo>();

            string expected = "Hello World";

            var container = builder.Build();

            var fooFactory = container.Resolve<Func<string, IFoo>>();
            var instance = fooFactory(expected);

            Assert.IsNotNull(fooFactory);
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, ((Foo)instance).Arg1);

            var fooFactoryTwo = container.Resolve<Func<string, bool, IFoo>>();
            var fooTwo = fooFactoryTwo("Peter", true);

            Assert.IsNotNull(fooFactoryTwo);
            Assert.AreEqual("Peter", ((Foo) fooTwo).Arg1);
            Assert.AreEqual(true, ((Foo) fooTwo).Arg2);
        }

        [Test]
        public void Container_can_resolve_factories_as_dependendy()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFoo, Foo>();

            var container = builder.Build();

            var fooFactoryConsumer = container.Resolve<FooFactoryConsumer>();

            Assert.IsNotNull(fooFactoryConsumer);
            Assert.IsNotNull(fooFactoryConsumer.Foo);
        }

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
        public void Container_can_resolve_a_few_open_generic_types()
        {
            var builder = new ContainerBuilder();
            builder.Register(typeof (IRepository<>), typeof (Repository<>));

            var container = builder.Build();

            var fooRepository = container.Resolve<IRepository<Foo>>();
            var barRepository = container.Resolve<IRepository<Bar>>();

            Assert.NotNull(fooRepository);
            Assert.NotNull(barRepository);

            Assert.IsInstanceOf<IRepository<Foo>>(fooRepository);
            Assert.IsInstanceOf<IRepository<Bar>>(barRepository);
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

            builder.Register<IBar, Bar>();

            builder.Register<IFoo, Foo>();
            builder.Register<IFoo, FooTwo>();

            //builder.Register<ILorem, TestLorem>();

            var container = builder.Build();

            var allInstances = container.ResolveAll<IFoo>();

            Assert.AreEqual(2, allInstances.Count());
        }

        [Test]
        public void Container_can_resolve_enumerables_of_contract()
        {
            var builder = new ContainerBuilder();

            builder.Register<IBar>(c => new Bar());
            builder.Register<IBar>(c => new Bar());
            builder.Register<EnumerableTest>();

            var container = builder.Build();

            var instance = container.Resolve<EnumerableTest>();

            Assert.IsNotNull(instance);
            Assert.AreEqual(2, instance.Bars.Count());
        }
    }
}