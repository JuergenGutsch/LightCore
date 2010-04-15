using System.Collections.Generic;

using LightCore.TestTypes;

using NUnit.Framework;

namespace LightCore.Tests.Registration.RegistrationSource.RegistrationSource
{
    [TestFixture]
    public class WhenGetRegistrationForCoreIsCalled : RegistrationSourceFixture
    {
        [Test]
        public void WithNull_NullReturned()
        {
            var registrationSource = this.GetRegistrationSource();

            Assert.That(registrationSource.GetRegistrationFor(null, null), Is.Null);
        }

        [Test]
        public void WithAnyType_NullReturned()
        {
            var registrationSource = this.GetRegistrationSource();

            Assert.That(registrationSource.GetRegistrationFor(typeof(Bar), null), Is.Null);
            Assert.That(registrationSource.GetRegistrationFor(typeof(IBar), null), Is.Null);
            Assert.That(registrationSource.GetRegistrationFor(typeof(IEnumerable<object>), null), Is.Null);
        }
    }
}