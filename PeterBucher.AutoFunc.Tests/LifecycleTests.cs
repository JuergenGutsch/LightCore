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
            IContainer container = new AutoFuncContainer();
            container.Register<IFooRepository, FooRepository>();
            container.Register<ILogger, Logger>();

            var rep1 = container.Resolve<IFooRepository>();
            var rep2 = container.Resolve<IFooRepository>();

            Assert.IsFalse(ReferenceEquals(rep1, rep2));
        }

        [TestMethod]
        public void Can_take_care_of_lifecycle_singleton()
        {
            IContainer container = new AutoFuncContainer();
            container.Register<IFooRepository, FooRepository>().AsSingleton();
            container.Register<ILogger, Logger>();

            var rep1 = container.Resolve<IFooRepository>();
            var rep2 = container.Resolve<IFooRepository>();

            Assert.IsTrue(ReferenceEquals(rep1, rep2));
        }
    }
}