using LightCore.Activation.Activators;
using LightCore.Lifecycle;
using LightCore.TestTypes;

using NUnit.Framework;

namespace LightCore.Tests.Registration.RegistrationSource.ArrayRegistrationSource
{
    [TestFixture]
    public class WhenGetRegistrationForCoreIsCalled : RegistrationSourceFixture
    {
        [Test]
        public void WithArrayType_RegistrationItemReturned()
        {
            var registrationSource = this.GetArrayRegistrationSource(typeof(IFoo));

            Assert.That(registrationSource.GetRegistrationFor(typeof(IFoo[]), null), Is.Not.Null);
        }

        [Test]
        public void WithArrayType_RegistrationItemReturnedAndHoldsRightData()
        {
            var registrationSource = this.GetArrayRegistrationSource(typeof(IBar));

            var registrationItem = registrationSource.GetRegistrationFor(typeof(IBar[]), null);

            Assert.That(registrationItem.ContractType, Is.EqualTo(typeof(IBar[])));
            Assert.That(registrationItem.ImplementationType, Is.EqualTo(typeof(IBar[])));
            Assert.That(registrationItem.Lifecycle, Is.TypeOf<TransientLifecycle>());
        }
    }
}