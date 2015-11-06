using System;
using FluentAssertions;
using LightCore.TestTypes;
using Xunit;

namespace LightCore.Tests.Integration
{
    public class LazyRegistrationSourceTests
    {
        [Fact]
        public void LazyDependencies_AreNotSharedBetweenCallers()
        {
            var builder = new ContainerBuilder();

            builder.Register<IFoo, Foo>();

            var container = builder.Build();

            var lazyInstance = container.Resolve<Lazy<IFoo>>();

            var value = lazyInstance.Value;

            var lazyInstanceTwo = container.Resolve<Lazy<IFoo>>();

            lazyInstanceTwo.IsValueCreated.Should().BeFalse();
        }
    }
}