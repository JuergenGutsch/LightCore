using LightCore.Activation.Activators;
using LightCore.Lifecycle;
using LightCore.TestTypes;

using NUnit.Framework;

using System.Collections.Generic;

namespace LightCore.Tests.Registration.RegistrationSource.EnumerableRegistrationSource
{
    [TestFixture]
    public class WhenGetRegistrationForCoreIsCalled : RegistrationSourceFixture
    {
        [Test]
        public void WithEnumerableType_RegistrationItemReturned()
        {
            var registrationSource = this.GetEnumerableRegistrationSource(typeof(IFoo));

            Assert.That(registrationSource.GetRegistrationFor(typeof(IEnumerable<IFoo>), this.BootStrapContainer), Is.Not.Null);
        }

        [Test]
        public void WithEnumerableType_RegistrationItemReturnedAndHoldsRightData()
        {
            var registrationSource = this.GetEnumerableRegistrationSource(typeof(IBar));

            var registrationItem = registrationSource.GetRegistrationFor(typeof(IEnumerable<IBar>), this.BootStrapContainer);

            Assert.That(registrationItem, Is.Not.Null);
            Assert.That(registrationItem.ContractType, Is.EqualTo(typeof(IEnumerable<IBar>)));
            Assert.That(registrationItem.ImplementationType, Is.EqualTo(typeof(IEnumerable<IBar>)));
            Assert.That(registrationItem.Lifecycle, Is.TypeOf<TransientLifecycle>());
        }
    }
}