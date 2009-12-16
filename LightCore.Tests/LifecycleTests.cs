using System.Threading;

using LightCore.Lifecycle;
using LightCore.TestTypes;

using NUnit.Framework;

namespace LightCore.Tests
{
    [TestFixture]
    public class LifecycleTests
    {
        [Test]
        public void Instance_is_not_reused_when_controlled_by_transient_lifecycle()
        {
            var builder = new ContainerBuilder();

            builder.Register<IFoo, Foo>();
            builder.Register<IBar, Bar>();

            var container = builder.Build();
            var foo1 = container.Resolve<IFoo>();
            var foo2 = container.Resolve<IFoo>();

            Assert.IsFalse(ReferenceEquals(foo1, foo2));
        }

        [Test]
        public void Instance_is_reused_when_controlled_by_singleton_lifecycle()
        {
            var builder = new ContainerBuilder();
            builder.DefaultControlledBy<SingletonLifecycle>();

            builder.Register<IFoo, Foo>();
            builder.Register<IBar, Bar>();

            var container = builder.Build();
            var foo1 = container.Resolve<IFoo>();
            var foo2 = container.Resolve<IFoo>();

            Assert.IsTrue(ReferenceEquals(foo1, foo2));
        }

        [Test]
        public void Instance_is_reused_on_same_thread_when_controlled_by_threadsingleton_lifecycle()
        {
            var builder = new ContainerBuilder();

            builder.DefaultControlledBy<ThreadSingletonLifecycle>();
            builder.Register<IFoo, Foo>();
            builder.Register<IBar, Bar>();

            var container = builder.Build();

            var threadDataOne = new ThreadData(container);
            var threadOne = new Thread(threadDataOne.ResolveFoo);

            var threadDataTwo = new ThreadData(container);
            var threadTwo = new Thread(threadDataTwo.ResolveFoo);

            threadOne.Start();
            threadTwo.Start();

            threadOne.Join();
            threadTwo.Join();

            Assert.AreNotSame(threadDataOne.Foo, threadDataTwo.Foo);
        }

        private class ThreadData
        {
            private readonly IContainer _container;

            public ThreadData(IContainer container)
            {
                this._container = container;
            }

            public IFoo Foo
            {
                get;
                set;
            }

            public void ResolveFoo()
            {
                this.Foo = this._container.Resolve<IFoo>();
            }
        }
    }
}