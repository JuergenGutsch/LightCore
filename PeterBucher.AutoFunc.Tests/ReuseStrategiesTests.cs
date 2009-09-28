using Microsoft.VisualStudio.TestTools.UnitTesting;

using PeterBucher.AutoFunc.Build;
using PeterBucher.AutoFunc.TestTypes;

namespace PeterBucher.AutoFunc.Tests
{
    [TestClass]
    public class ReuseStrategiesTests
    {
        [TestMethod]
        public void Instance_is_not_reused_on_transient_strategy()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFooRepository, FooRepository>();
            builder.Register<ILogger, Logger>();

            var container = builder.Build();
            var rep1 = container.Resolve<IFooRepository>();
            var rep2 = container.Resolve<IFooRepository>();

            Assert.IsFalse(ReferenceEquals(rep1, rep2));
        }

        [TestMethod]
        public void Instance_is_reused_on_singleton_strategy()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFooRepository, FooRepository>().ScopedToSingleton();
            builder.Register<ILogger, Logger>();

            var container = builder.Build();
            var rep1 = container.Resolve<IFooRepository>();
            var rep2 = container.Resolve<IFooRepository>();

            Assert.IsTrue(ReferenceEquals(rep1, rep2));
        }
    }
}