using LightCore.TestTypes;

using NUnit.Framework;

namespace LightCore.Tests.Integration
{
    /// <summary>
    /// Summary description for ResolvingTests
    /// </summary>
    [TestFixture]
    public class ResolvingTests
    {
        [Test]
        public void Foo_is_resolved_with_default_constructor_if_no_bar_is_registered()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFoo, Foo>();

            var container = builder.Build();

            var instance = container.Resolve<IFoo>();
        }

        [Test]
        public void Foo_is_resolved_with_the_bar_only_constructor_if_bar_is_registered()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFoo, Foo>();
            builder.Register<IBar, Bar>();

            var container = builder.Build();

            var instance = container.Resolve<IFoo>();

            Assert.IsNotNull(instance);
            Assert.IsNotNull(instance.Bar);
        }

        [Test]
        public void Foo_is_resolved_with_the_bar_with_arguments_constructor_if_bar_registered_and_arguments_supported()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFoo, Foo>().WithArguments("Test", true);
            builder.Register<IBar, Bar>();

            var container = builder.Build();

            var instance = container.Resolve<IFoo>();

            //Assert.IsNotNull(instance);
            //Assert.IsNotNull(instance.Bar);
            //Assert.IsNotNull((instance as Foo).Arg1);
            //Assert.AreEqual(true, (instance as Foo).Arg2);
        }

        [Test]
        public void Foo_is_resolved_with_bool_argument_constructor_if_only_that_argument_is_supported()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFoo, Foo>().WithArguments(true);

            var container = builder.Build();

            var instance = container.Resolve<IFoo>();

            Assert.IsNotNull(instance);
            Assert.AreEqual(true, (instance as Foo).Arg2);
        }

        [Test]
        public void Can_resolve_dependencies_with_injected_container()
        {
            var builder = new ContainerBuilder();
            builder.Register<IBar>(c => new Bar());

            var container = builder.Build();

            var resolvedContainer = container.Resolve<IContainer>();
            var bar = resolvedContainer.Resolve<IBar>();

            Assert.NotNull(bar);
            Assert.IsInstanceOf<IBar>(bar);

            Assert.NotNull(resolvedContainer);
            Assert.IsInstanceOf<IContainer>(resolvedContainer);
        }

        [Test]
        public void Container_resolves_registered_interface_registration()
        {
            var builder = new ContainerBuilder();

            builder.Register<IBar, Bar>();
            builder.Register<IFoo, Foo>();

            var container = builder.Build();
            var instance = container.Resolve<IFoo>();

            Assert.IsInstanceOf<Foo>(instance);
        }

        [Test]
        public void Container_resolves_registed_abstract_class()
        {
            var builder = new ContainerBuilder();
            builder.Register<IBar, Bar>();
            builder.Register<FooBase, Foo>();

            var container = builder.Build();
            var instance = container.Resolve<FooBase>();

            Assert.IsInstanceOf<FooBase>(instance);
        }

        [Test]
        public void Container_resolves_registered_class_mapped_to_itself()
        {
            var builder = new ContainerBuilder();
            builder.Register<Bar>();

            var container = builder.Build();
            var instance = container.Resolve<Bar>();

            Assert.NotNull(instance);
        }

        [Test]
        public void Container_resolves_a_whole_object_tree()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFoo, Foo>();
            builder.Register<IBar, Bar>();

            var container = builder.Build();
            var instance = container.Resolve<IFoo>();

            Assert.IsInstanceOf<Foo>(instance);
            Assert.IsNotNull(instance.Bar);
        }
    }
}