using LightCore.Activation;
using LightCore.Activation.Activators;

using NUnit.Framework;

namespace LightCore.Tests.Activation.Activators.InstanceActivator
{
    [TestFixture]
    public class WhenActivateInstanceIsCalled
    {
        private IActivator GetActivator<TContract>(TContract instance)
        {
            return new InstanceActivator<TContract>(instance);
        }

        [Test]
        public void WithInstanceAndEmptyResolutionContext_SameInstanceReturned()
        {
            object instance = new object();
            var instanceActivator = this.GetActivator(instance);

            object result = instanceActivator.ActivateInstance(new ResolutionContext());

            Assert.That(result, Is.SameAs(instance));
        }

        [Test]
        public void WithNullAndEmptyResolutionContext_NullReturned()
        {
            object instance = null;
            var instanceActivator = this.GetActivator(instance);

            object result = instanceActivator.ActivateInstance(new ResolutionContext());

            Assert.That(result, Is.SameAs(null));
        }
    }
}