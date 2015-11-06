using System;

using LightCore.Lifecycle;
using LightCore.TestTypes;

namespace LightCore.Tests.Lifecycle
{
    public class ThreadData
    {
        private readonly IContainer _container;

        private readonly ILifecycle _lifecycle;
        private readonly Func<object> _factory;

        public ThreadData(IContainer container)
        {
            this._container = container;
        }

        public ThreadData(ILifecycle lifecycle, Func<object> factory)
        {
            this._lifecycle = lifecycle;
            this._factory = factory;
        }

        public IFoo FooOne
        {
            get;
            private set;
        }

        public IFoo FooTwo
        {
            get;
            private set;
        }

        public void ResolveFoosWithContainer()
        {
            this.FooOne = this._container.Resolve<IFoo>();
            this.FooTwo = this._container.Resolve<IFoo>();
        }

        public void ResolveFoosWithLifecycle()
        {
            this.FooOne = (IFoo)this._lifecycle.ReceiveInstanceInLifecycle(this._factory);
            this.FooTwo = (IFoo)this._lifecycle.ReceiveInstanceInLifecycle(this._factory);
        }
    }
}