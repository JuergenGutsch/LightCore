using System;
using System.Threading;
using FluentAssertions;
using LightCore.Lifecycle;
using LightCore.Tests.Lifecycle;
using LightCore.TestTypes;
using Xunit;

namespace LightCore.Tests.Integration
{
    public class LifecycleTests
    {
        [Fact]
        public void ThreadSingletonShouldBecollectedwhenThreadFinished()
        {
            var builder = new ContainerBuilder();
            builder.Register<IFoo, Foo>().ControlledBy<ThreadSingletonLifecycle>();
            var container = builder.Build();
            WeakReference obj = null;

            Action action = () =>
            {
                var o = container.Resolve<IFoo>();
                obj = new WeakReference(o);
            };

            var thread = new Thread(new ThreadStart(action));
            thread.Start();
            thread.Join();
            obj.IsAlive.Should().BeTrue();
            thread = null;
            GC.Collect(2);
            obj.IsAlive.Should().BeFalse("Objekt sollte nicht mehr existieren");
        }

        [Fact]
        public void Instance_is_not_reused_when_controlled_by_transient_lifecycle()
        {
            var builder = new ContainerBuilder();

            builder.Register<IFoo, Foo>();
            builder.Register<IBar, Bar>();

            var container = builder.Build();
            var foo1 = container.Resolve<IFoo>();
            var foo2 = container.Resolve<IFoo>();

            ReferenceEquals(foo1, foo2);
        }

        [Fact]
        public void Instance_is_reused_when_controlled_by_singleton_lifecycle()
        {
            var builder = new ContainerBuilder();
            builder.DefaultControlledBy<SingletonLifecycle>();

            builder.Register<IFoo, Foo>();
            builder.Register<IBar, Bar>();

            var container = builder.Build();
            var foo1 = container.Resolve<IFoo>();
            var foo2 = container.Resolve<IFoo>();

            ReferenceEquals(foo1, foo2).Should().BeTrue();
        }
        
        [Fact]
        public void Instance_is_reused_on_same_thread_when_controlled_by_threadsingleton_lifecycle()
        {
            var builder = new ContainerBuilder();

            //builder.DefaultControlledBy<ThreadSingletonLifecycle>();
            builder.Register<IFoo, Foo>().ControlledBy(new ThreadSingletonLifecycle());
            builder.Register<IBar, Bar>().ControlledBy(new ThreadSingletonLifecycle());

            var container = builder.Build();

            var threadData = new ThreadData(container);
            var thread = new Thread(threadData.ResolveFoosWithContainer);

            var threadDataTwo = new ThreadData(container);
            var threadTwo = new Thread(threadDataTwo.ResolveFoosWithContainer);

            thread.Start();
            threadTwo.Start();

            thread.Join();
            threadTwo.Join();

            threadData.FooOne.Should().BeSameAs(threadData.FooTwo);
        }
    }
}