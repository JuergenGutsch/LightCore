using FluentAssertions;
using Xunit;

namespace LightCore.Tests.Lifecycle.SingletonLifecycle
{
    
    public class WhenRetrieveInstanceInLifecycleIsCalled : LifecycleFixture
    {
        [Fact]
        public void WithActivationFunction_SameObjectsAreReturned()
        {
            var lifecycle = new LightCore.Lifecycle.SingletonLifecycle();
            var factory = GetActivationFactory();

            var instanceOne = lifecycle.ReceiveInstanceInLifecycle(factory);
            var instanceTwo = lifecycle.ReceiveInstanceInLifecycle(factory);

            instanceOne.Should().Be(instanceTwo);
        }
    }
}