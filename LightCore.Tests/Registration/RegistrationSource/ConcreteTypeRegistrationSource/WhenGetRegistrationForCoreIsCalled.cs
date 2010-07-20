using LightCore.Activation.Activators;
using LightCore.Lifecycle;
using LightCore.TestTypes;

using NUnit.Framework;

namespace LightCore.Tests.Registration.RegistrationSource.ConcreteTypeRegistrationSource
{
    [TestFixture]
    public class WhenGetRegistrationForCoreIsCalled : RegistrationSourceFixture
    {
        [Test]
        public void WithConcreteType_RegistrationItemReturned()
        {
            var registrationSource = this.GetConcreteRegistrationSource();

            Assert.That(registrationSource.GetRegistrationFor(typeof(Foo), this.BootStrapContainer), Is.Not.Null);
        }

        [Test]
        public void WithConcreteType_RegistrationItemReturnedAndHoldsRightData()
        {
            var registrationSource = this.GetConcreteRegistrationSource();

            var registrationItem = registrationSource.GetRegistrationFor(typeof (Foo), this.BootStrapContainer);

            Assert.That(registrationItem, Is.Not.Null);
            Assert.That(registrationItem.ContractType, Is.EqualTo(typeof(Foo)));
            Assert.That(registrationItem.ImplementationType, Is.EqualTo(typeof (Foo)));
            Assert.That(registrationItem.Activator, Is.TypeOf<ReflectionActivator>());
            Assert.That(registrationItem.Lifecycle, Is.TypeOf<TransientLifecycle>());
        }
    }
}