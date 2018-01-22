using System;
using LightCore.Lifecycle;
using LightCore.TestTypes;

namespace LightCore.Tests.Lifecycle
{
    public class ThreadData
    {
        private readonly IContainer _container;
        private readonly Func<object> _factory;

        private readonly ILifecycle _lifecycle;

        public ThreadData(IContainer container)
        {
            _container = container;
        }

        public ThreadData(ILifecycle lifecycle, Func<object> factory)
        {
            _lifecycle = lifecycle;
            _factory = factory;
        }

        public IFoo FooOne { get; private set; }

        public IFoo FooTwo { get; private set; }

        public void ResolveFoo1WithContainer()
        {
            FooOne = _container.Resolve<IFoo>();
        }
        public void ResolveFoo2WithContainer()
        {
            FooTwo = _container.Resolve<IFoo>();
        }

        public void ResolveFoo1WithLifecycle()
        {
            FooOne = (IFoo) _lifecycle.ReceiveInstanceInLifecycle(_factory);
        }
        public void ResolveFoo2WithLifecycle()
        {
            FooTwo = (IFoo)_lifecycle.ReceiveInstanceInLifecycle(_factory);
        }
    }
}