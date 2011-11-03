using System;

using LightCore.Activation.Activators;
using LightCore.Lifecycle;
using LightCore.TestTypes;

using NUnit.Framework;

namespace LightCore.Tests.Registration.RegistrationSource.FactoryRegistrationSource
{
    [TestFixture]
    public class WhenGetRegistrationForCoreIsCalled : RegistrationSourceFixture
    {
        [Test]
        public void WithFuncType_RegistrationItemReturned()
        {
            var registrationSource = this.GetFactoryRegistrationSource(typeof(IFoo));

            Assert.That(registrationSource.GetRegistrationFor(typeof(Func<IFoo>), null), Is.Not.Null);
        }

        [Test]
        public void WithFuncTypeAndArguments_RegistrationReturned()
        {
            var registrationSource = this.GetFactoryRegistrationSource(typeof(IFoo));

            Assert.That(registrationSource.GetRegistrationFor(typeof(Func<string, IFoo>), null), Is.Not.Null);
        }

        [Test]
        public void WithFuncTypeAndArguments_RegistrationItemReturnedAndHoldsRightData()
        {
            var registrationSource = this.GetFactoryRegistrationSource(typeof(IFoo));

            var registrationItem = registrationSource.GetRegistrationFor(typeof(Func<string, IFoo>), null);

            Assert.That(registrationItem, Is.Not.Null);
            Assert.That(registrationItem.ContractType, Is.EqualTo(typeof(Func<string, IFoo>)));
            Assert.That(registrationItem.ImplementationType, Is.EqualTo(typeof(Func<string, IFoo>)));
            Assert.That(registrationItem.Lifecycle, Is.TypeOf<TransientLifecycle>());
        }
    }
}