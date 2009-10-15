using LightCore.Lifecycle;
using LightCore.TestTypes;

using NUnit.Framework;

namespace LightCore.Tests
{
    [TestFixture]
    public class ReuseStrategiesTests
    {
        [Test]
        public void Instance_is_not_reused_on_transient_strategy()
        {
            var builder = new ContainerBuilder();

            builder.DefaultControlledBy<TransientLifecycle>();
            builder.Register<IFoo, Foo>();
            builder.Register<IBar, Bar>();

            var container = builder.Build();
            var foo1 = container.Resolve<IFoo>();
            var foo2 = container.Resolve<IFoo>();

            Assert.IsFalse(ReferenceEquals(foo1, foo2));
        }

        [Test]
        public void Instance_is_reused_on_singleton_strategy()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFoo, Foo>();
            builder.Register<IBar, Bar>();

            var container = builder.Build();
            var foo1 = container.Resolve<IFoo>();
            var foo2 = container.Resolve<IFoo>();

            Assert.IsTrue(ReferenceEquals(foo1, foo2));
        }
    }
}