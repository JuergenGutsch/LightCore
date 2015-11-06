using LightCore.Activation.Activators;
using LightCore.TestTypes;

using NUnit.Framework;

namespace LightCore.Tests.Registration.RegistrationSource.OpenGenericRegistrationSource
{
    [TestFixture]
    public class WhenGetRegistrationForCoreIsCalled : RegistrationSourceFixture
    {
        [Test]
        public void WithOpenGenericType_RegistrationItemReturned()
        {
            var registrationSource = this.GetOpenGenericRegistrationSource(typeof(IRepository<,>), typeof(Repository<,>));

            Assert.That(registrationSource.GetRegistrationFor(typeof(IRepository<Foo, int>), this.BootStrapContainer), Is.Not.Null);
        }

        [Test]
        public void WithOpenGenericType_RegistrationItemReturnedAndHoldsRightData()
        {
            var registrationSource = this.GetOpenGenericRegistrationSource(typeof(IRepository<>), typeof(Repository<>));

            var registrationItem = registrationSource.GetRegistrationFor(typeof(IRepository<Foo>), this.BootStrapContainer);

            Assert.That(registrationItem, Is.Not.Null);
            Assert.That(registrationItem.ContractType, Is.EqualTo(typeof(IRepository<Foo>)));
            Assert.That(registrationItem.ImplementationType, Is.EqualTo(typeof(Repository<Foo>)));
            Assert.That(registrationItem.Activator, Is.TypeOf<ReflectionActivator>());
        }
    }
}