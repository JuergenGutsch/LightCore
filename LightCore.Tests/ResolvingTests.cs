using System.Linq;

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
            builder.Register<IFoo, Foo>();

            var container = builder.Build();
            var instance = container.Resolve<IFoo>();

            Assert.IsInstanceOf<Foo>(instance);
        }

        [Test]
        public void Container_resolves_registed_abstract_class()
        {
            var builder = new ContainerBuilder();
            builder.Register<ProviderBase, DefaultProvider>();

            var container = builder.Build();
            var instance = container.Resolve<ProviderBase>();

            Assert.IsInstanceOf<DefaultProvider>(instance);
        }

        [Test]
        public void Container_resolves_a_whole_object_tree()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFooRepository, FooRepository>();
            builder.Register<IFooService, FooService>();
            builder.Register<ILogger, Logger>();

            var container = builder.Build();
            var instance = container.Resolve<IFooService>();

            Assert.IsInstanceOf<FooService>(instance);
            Assert.IsTrue(instance.GetFoos().Count() > 0);
            Assert.IsInstanceOf<Logger>(instance.Logger);
        }
    }
}