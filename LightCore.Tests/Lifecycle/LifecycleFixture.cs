using System;

using LightCore.TestTypes;

namespace LightCore.Tests.Lifecycle
{
    public class LifecycleFixture
    {
        internal Func<object> GetActivationFactory()
        {
            return () => new Foo();
        }
    }
}