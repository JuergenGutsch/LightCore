using LightCore.Activation.Activators;
using LightCore.TestTypes;

using NUnit.Framework;

namespace LightCore.Tests.Registration.RegistrationSource.OpenGenericRegistrationSource
{
    [TestFixture]
    public class WhenGetRegistrationForCoreIsCalled : RegistrationSourceFixture
    {
        [Test]
        public void WithNull_NullReturned()
        {
            var registrationSource = this.GetOpenGenericRegistrationSource(typeof(object), typeof(object));

            Assert.That(registrationSource.GetRegistrationFor(null, null), Is.Null);
        }

        [Test]
        public void WithNoOpenGenericType_NullReturned()
        {
            var registrationSource = this.GetOpenGenericRegistrationSource(typeof(IBar), typeof(Bar));

            Assert.That(registrationSource.GetRegistrationFor(typeof(Bar), null), Is.Null);
        }

        [Test]
        public void WithOpenGenericType_RegistrationItemReturned()
        {
            var registrationSource = this.GetOpenGenericRegistrationSource(typeof(IRepository<,>), typeof(Repository<,>));

            Assert.That(registrationSource.GetRegistrationFor(typeof(IRepository<Foo, int>), null), Is.Not.Null);
        }

        [Test]
        public void WithOpenGenericType_RegistrationItemReturnedAndHoldsRightData()
        {
            var registrationSource = this.GetOpenGenericRegistrationSource(typeof(IRepository<>), typeof(Repository<>));

            var registrationItem = registrationSource.GetRegistrationFor(typeof(IRepository<Foo>), null);

            Assert.That(registrationItem, Is.Not.Null);
            Assert.That(registrationItem.Key.ContractType, Is.EqualTo(typeof(IRepository<Foo>)));
            Assert.That(registrationItem.ImplementationType, Is.EqualTo(typeof(Repository<Foo>)));
            Assert.That(registrationItem.Activator, Is.TypeOf<ReflectionActivator>());
        }
    }
}