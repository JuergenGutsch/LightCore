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
        public void WithNull_NullReturned()
        {
            var registrationSource = this.GetFactoryRegistrationSource(typeof(object));

            Assert.That(registrationSource.GetRegistrationFor(null, null), Is.Null);
        }

        [Test]
        public void WithNoFuncType_NullReturned()
        {
            var registrationSource = this.GetFactoryRegistrationSource(typeof(Foo));

            Assert.That(registrationSource.GetRegistrationFor(typeof(Foo), null), Is.Null);
        }

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
            Assert.That(registrationItem.Key.ContractType, Is.EqualTo(typeof(Func<string, IFoo>)));
            Assert.That(registrationItem.ImplementationType, Is.EqualTo(typeof(Func<string, IFoo>)));
            Assert.That(registrationItem.Activator, Is.TypeOf<DelegateActivator>());
            Assert.That(registrationItem.Lifecycle, Is.TypeOf<TransientLifecycle>());
        }
    }
}