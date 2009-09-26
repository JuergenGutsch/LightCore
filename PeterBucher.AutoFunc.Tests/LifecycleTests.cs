using Microsoft.VisualStudio.TestTools.UnitTesting;

using PeterBucher.AutoFunc.Tests.TestTypes;

namespace PeterBucher.AutoFunc.Tests
{
    /// <summary>
    /// Summary description for LifecycleTests
    /// </summary>
    [TestClass]
    public class LifecycleTests
    {
        [TestMethod]
        public void Can_handles_default_transient_lifecycle_correct()
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
        public void Can_take_care_of_lifecycle_singleton()
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