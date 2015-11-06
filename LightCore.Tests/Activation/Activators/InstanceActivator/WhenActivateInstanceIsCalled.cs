using FluentAssertions;
using LightCore.Activation;
using LightCore.Activation.Activators;
using Xunit;


namespace LightCore.Tests.Activation.Activators.InstanceActivator
{
    
    public class WhenActivateInstanceIsCalled
    {
        private IActivator GetActivator<TContract>(TContract instance)
        {
            return new InstanceActivator<TContract>(instance);
        }

        [Fact]
        public void WithInstanceAndEmptyResolutionContext_SameInstanceReturned()
        {
            var instance = new object();
            var instanceActivator = GetActivator(instance);

            var result = instanceActivator.ActivateInstance(new ResolutionContext());

            result.Should().BeSameAs(instance);
        }

        [Fact]
        public void WithNullAndEmptyResolutionContext_NullReturned()
        {
            object instance = null;
            var instanceActivator = GetActivator(instance);

            var result = instanceActivator.ActivateInstance(new ResolutionContext());

            result.Should().BeSameAs(null);
        }
    }
}