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
        public void Can_create_an_instance_from_contract()
        {
            var builder = new ContainerBuilder();

            builder.Register<IFooRepository, FooRepository>();
            builder.Register<ILogger, Logger>();

            var container = builder.Build();

            IFooRepository fooRepository = container.Resolve<IFooRepository>();

            Assert.IsNotNull(fooRepository);
            Assert.IsTrue(fooRepository.GetFoos().Count() > 0);
        }

        [TestMethod]
        public void Can_resolve_a_registered_abstract_class()
        {
            var builder = new ContainerBuilder();

            builder.Register<ProviderBase, DefaultProvider>();

            var container = builder.Build();

            var instance = container.Resolve<ProviderBase>();

            Assert.IsNotNull(instance);
            Assert.IsInstanceOfType(instance, typeof (DefaultProvider));
        }

        [TestMethod]
        public void ResolveUp_a_object_tree_works()
        {
            var builder = new ContainerBuilder();

            builder.Register<IFooRepository, FooRepository>();
            builder.Register<IFooService, FooService>();
            builder.Register<ILogger, Logger>();

            var container = builder.Build();

            var instance = container.Resolve<IFooService>();

            Assert.IsNotNull(instance);
            Assert.IsTrue(instance.GetFoos().Count() > 0);
            Assert.IsNotNull(instance.Logger);
        }
    }
}