using LightCore.Lifecycle;
using LightCore.Registration;

using NUnit.Framework;

namespace LightCore.Tests.Fluent.FluentRegistration
{
    [TestFixture]
    public class WhenControlledByIsCalled : FluentFixture
    {
        [Test]
        public void WithGenericSingletonLifecycleArgument_LifecycleIsSetToSingleton()
        {
            var registrationItem = new RegistrationItem();
            var fluentRegistration = this.GetRegistration(registrationItem);

            fluentRegistration.ControlledBy<SingletonLifecycle>();

            Assert.That(registrationItem.Lifecycle, Is.InstanceOf<SingletonLifecycle>());
        }

        [Test]
        public void WithGenericTransientLifecycleArgument_LifecycleIsSetToTransient()
        {
            var registrationItem = new RegistrationItem();
            var fluentRegistration = this.GetRegistration(registrationItem);

            fluentRegistration.ControlledBy<TransientLifecycle>();

            Assert.That(registrationItem.Lifecycle, Is.InstanceOf<TransientLifecycle>());
        }

        [Test]
        public void WithSingletonLifecycleTypeArgument_LifecycleIsSetToSingleton()
        {
            var registrationItem = new RegistrationItem();
            var fluentRegistration = this.GetRegistration(registrationItem);

            fluentRegistration.ControlledBy(typeof(SingletonLifecycle));

            Assert.That(registrationItem.Lifecycle, Is.InstanceOf<SingletonLifecycle>());
        }

        [Test]
        public void WithObjectAsLifecycle_ArgumentExceptionThrown()
        {
            var registrationItem = new RegistrationItem();
            var fluentRegistration = this.GetRegistration(registrationItem);

            Assert.That(() => fluentRegistration.ControlledBy(typeof(object)), Throws.ArgumentException);
        }

        [Test]
        public void WithNullAsLifecycle_ArgumentExceptionThrown()
        {
            var registrationItem = new RegistrationItem();
            var fluentRegistration = this.GetRegistration(registrationItem);

            Assert.That(() => fluentRegistration.ControlledBy(null), Throws.ArgumentException);
        }
    }
}