using Microsoft.VisualStudio.TestTools.UnitTesting;

using PeterBucher.AutoFunc.Tests.TestTypes;

namespace PeterBucher.AutoFunc.Tests
{
    [TestClass]
    public class LifeTimeTests
    {
        [TestMethod]
        public void Can_handles_default_transient_lifetime_correct()
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
        public void Can_take_care_of_lifetime_singleton()
        {
            var builder = new ContainerBuilder();

            builder.Register<IFooRepository, FooRepository>().AsSingleton();
            builder.Register<ILogger, Logger>();

            var container = builder.Build();

            var rep1 = container.Resolve<IFooRepository>();
            var rep2 = container.Resolve<IFooRepository>();

            Assert.IsTrue(ReferenceEquals(rep1, rep2));
        }
    }
}