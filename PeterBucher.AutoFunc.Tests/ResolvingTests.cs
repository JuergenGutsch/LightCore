using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using PeterBucher.AutoFunc.Build;
using PeterBucher.AutoFunc.TestTypes;

namespace PeterBucher.AutoFunc.Tests
{
    /// <summary>
    /// Summary description for ResolvingTests
    /// </summary>
    [TestClass]
    public class ResolvingTests
    {
        [TestMethod]
        public void Container_resolves_interface_registration()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFoo, Foo>();

            var container = builder.Build();
            var instance = container.Resolve<IFoo>();

            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, typeof(Foo));
        }

        [TestMethod]
        public void Container_resolves_registed_abstract_class()
        {
            var builder = new ContainerBuilder();
            builder.Register<ProviderBase, DefaultProvider>();

            var container = builder.Build();
            var instance = container.Resolve<ProviderBase>();

            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, typeof(DefaultProvider));
        }

        [TestMethod]
        public void Container_resolves_a_whole_object_tree()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFooRepository, FooRepository>();
            builder.Register<IFooService, FooService>();
            builder.Register<ILogger, Logger>();

            var container = builder.Build();
            var instance = container.Resolve<IFooService>();

            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, typeof(FooService));

            Assert.IsTrue(instance.GetFoos().Count() > 0);

            Assert.IsNotNull(instance.Logger);
            Assert.IsInstanceOfType(instance.Logger, typeof(Logger));
        }
    }
}