using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using PeterBucher.AutoFunc.Tests.TestTypes;

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
            IContainer container = new Container();
            container.Register<IFooRepository, FooRepository>();
            container.Register<ILogger, Logger>();

            IFooRepository fooRepository = container.Resolve<IFooRepository>();

            Assert.IsNotNull(fooRepository);
            Assert.IsTrue(fooRepository.GetFoos().Count() > 0);
        }

        [TestMethod]
        public void ResolveUp_a_object_tree_works()
        {
            IContainer container = new Container();
            container.Register<IFooRepository, FooRepository>();
            container.Register<IFooService, FooService>();
            container.Register<ILogger, Logger>();

            var instance = container.Resolve<IFooService>();

            Assert.IsNotNull(instance);
            Assert.IsTrue(instance.GetFoos().Count() > 0);
            Assert.IsNotNull(instance.Logger);
        }
    }
}