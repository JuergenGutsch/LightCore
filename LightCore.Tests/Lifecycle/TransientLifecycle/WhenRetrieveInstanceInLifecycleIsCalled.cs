using FluentAssertions;
using Xunit;

namespace LightCore.Tests.Lifecycle.TransientLifecycle
{
    
    public class WhenRetrieveInstanceInLifecycleIsCalled : LifecycleFixture
    {
        [Fact]
        public void WithActivationFunction_DifferentObjectsAreReturned()
        {
            var lifecycle = new LightCore.Lifecycle.TransientLifecycle();
            var factory = GetActivationFactory();

            var instanceOne = lifecycle.ReceiveInstanceInLifecycle(factory);
            var instanceTwo = lifecycle.ReceiveInstanceInLifecycle(factory);

            instanceOne.Should().NotBeSameAs(instanceTwo);
        }
    }
}