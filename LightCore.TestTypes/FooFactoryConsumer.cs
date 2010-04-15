using System;

namespace LightCore.TestTypes
{
    public class FooFactoryConsumer
    {
        public IFoo Foo
        {
            get;
            set;
        }

        public FooFactoryConsumer(Func<IFoo> foo)
        {
            this.Foo = foo();
        }
    }
}