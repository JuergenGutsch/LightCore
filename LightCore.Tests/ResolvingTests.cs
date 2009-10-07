using LightCore.TestTypes;

using NUnit.Framework;

namespace LightCore.Tests
{
    /// <summary>
    /// Summary description for ResolvingTests
    /// </summary>
    [TestFixture]
    public class ResolvingTests
    {
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