using System.Threading;
using LightCore.TestTypes;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LightCore.Tests
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

        [TestMethod]
        public void Singleton_locking_for_threads_works()
        {
            var builder = new ContainerBuilder();
            builder.Register<ILogger, Logger>().ScopedToSingleton();

            var container = builder.Build();

            AutoResetEvent are = new AutoResetEvent(false);

            ILogger firstLogger = null;

            ThreadPool.QueueUserWorkItem(s =>
            {
                firstLogger = container.Resolve<ILogger>();

                are.Set();
                Thread.Sleep(300);
            });

            are.WaitOne();

            ILogger secondLogger = container.Resolve<ILogger>();

            Assert.AreSame(firstLogger, secondLogger);
        }
    }
}