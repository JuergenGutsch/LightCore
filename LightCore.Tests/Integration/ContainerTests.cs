using System;
using System.IO;
using System.Linq;

using LightCore.Lifecycle;
using LightCore.TestTypes;

using NUnit.Framework;

namespace LightCore.Tests.Integration
{
    [TestFixture]
    public class ContainerTests
    {
        private class StreamContainer
        {
            public StreamContainer()
            {
                
            }

            public StreamContainer(IDisposable disposable)
            {
                this.Stream = disposable;
            }

            public IDisposable Stream
            {
                get;
                set;
            }
        }

        [Test]
        public void Container_resolves_properties_in_transient_lifecycle()
        {
            var builder = new ContainerBuilder();
            builder.Register<IDisposable>(c => new MemoryStream()).ControlledBy<TransientLifecycle>();
            builder.DefaultControlledBy<TransientLifecycle>();

            var container = builder.Build();
            var streamContainerOne = container.Resolve<StreamContainer>();
            var streamContainerTwo = container.Resolve<StreamContainer>();

            Assert.IsFalse(ReferenceEquals(streamContainerOne, streamContainerTwo), "Root objects are equal");
            Assert.IsFalse(ReferenceEquals(streamContainerOne.Stream, streamContainerTwo.Stream), "Contained objects are equal");
        }

        [Test]
        public void Container_can_resolve_concrete_types()
        {
            var builder = new ContainerBuilder();

            var container = builder.Build();

            var foo = container.Resolve<Foo>();

            Assert.That(foo, Is.Not.Null);
        }

        [Test]
        public void Container_can_resolve_concrete_types_with_registered_dependencies()
        {
            var builder = new ContainerBuilder();
            builder.Register<IBar, Bar>();

            var container = builder.Build();

            var foo = container.Resolve<Foo>();

            Assert.That(foo.Bar, Is.Not.Null);
        }

        [Test]
        public void Container_can_resolve_with_arguments()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFoo, Foo>().WithArguments(4);

            var container = builder.Build();

            var foo = container.Resolve<IFoo>();

            Assert.That(((Foo) foo).Arg3, Is.EqualTo(4));
        }

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

            var container = builder.Build();

            var fooFactory = container.Resolve<Func<string, IFoo>>();

            const string expected = "Hello World";

            var instance = fooFactory(expected);

            Assert.IsNotNull(fooFactory);
            Assert.IsNotNull(instance);
            Assert.AreEqual(expected, ((Foo)instance).Arg1);

            var fooFactoryTwo = container.Resolve<Func<string, bool, IFoo>>();
            var fooTwo = fooFactoryTwo("Peter", true);

            Assert.IsNotNull(fooFactoryTwo);
            Assert.AreEqual("Peter", ((Foo)fooTwo).Arg1);
            Assert.AreEqual(true, ((Foo)fooTwo).Arg2);
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

            var fooRepository = container.Resolve<IRepository<IFoo>>();
            var barRepository = container.Resolve<IRepository<IBar>>();

            Assert.NotNull(fooRepository);
            Assert.NotNull(barRepository);
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

            var container = builder.Build();

            var instance = container.Resolve<EnumerableTest>();

            Assert.IsNotNull(instance);
            Assert.AreEqual(2, instance.Bars.Count());
        }
    }
}