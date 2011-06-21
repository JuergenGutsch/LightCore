using NUnit.Framework;

namespace LightCore.Tests.Lifecycle.SingletonLifecycle
{
    [TestFixture]
    public class WhenRetrieveInstanceInLifecycleIsCalled : LifecycleFixture
    {
        [Test]
        public void WithActivationFunction_SameObjectsAreReturned()
        {
            var lifecycle = new LightCore.Lifecycle.SingletonLifecycle();
            var factory = this.GetActivationFactory();

            object instanceOne = lifecycle.ReceiveInstanceInLifecycle(factory);
            object instanceTwo = lifecycle.ReceiveInstanceInLifecycle(factory);

            Assert.That(instanceOne, Is.SameAs(instanceTwo));
        }
    }
}