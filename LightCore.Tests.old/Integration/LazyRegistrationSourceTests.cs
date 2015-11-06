using System;

using LightCore.TestTypes;

using NUnit.Framework;

namespace LightCore.Tests.Integration
{
    [TestFixture]
    public class LazyRegistrationSourceTests
    {
        [Test]
        public void LazyDependencies_AreNotSharedBetweenCallers()
        {
            var builder = new ContainerBuilder();

            builder.Register<IFoo, Foo>();

            var container = builder.Build();

            var lazyInstance = container.Resolve<Lazy<IFoo>>();

            IFoo value = lazyInstance.Value;

            var lazyInstanceTwo = container.Resolve<Lazy<IFoo>>();

            Assert.That( lazyInstanceTwo.IsValueCreated, Is.False );
        }
    }
}