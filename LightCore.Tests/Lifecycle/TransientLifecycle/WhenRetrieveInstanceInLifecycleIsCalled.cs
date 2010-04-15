using NUnit.Framework;

namespace LightCore.Tests.Lifecycle.TransientLifecycle
{
    [TestFixture]
    public class WhenRetrieveInstanceInLifecycleIsCalled : LifecycleFixture
    {
        [Test]
        public void WithActivationFunction_DifferentObjectsAreReturned()
        {
            var lifecycle = new LightCore.Lifecycle.TransientLifecycle();
            var factory = this.GetActivationFactory();

            object instanceOne = lifecycle.ReceiveInstanceInLifecycle(factory);
            object instanceTwo = lifecycle.ReceiveInstanceInLifecycle(factory);

            Assert.That(instanceOne, Is.Not.SameAs(instanceTwo));
        }
    }
}