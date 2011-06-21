using System;

using LightCore.Activation.Activators;
using LightCore.Lifecycle;
using LightCore.TestTypes;

using NUnit.Framework;

namespace LightCore.Tests.Registration.RegistrationSource.LazyRegistrationSource
{
    [TestFixture]
    public class WhenGetRegistrationForCoreIsCalled : RegistrationSourceFixture
    {
        [Test]
        public void WithLazyType_RegistrationItemReturned()
        {
            var registrationSource = this.GetLazyRegistrationSource( typeof( IFoo ) );

            Assert.That(registrationSource.GetRegistrationFor(typeof(Lazy<IFoo>), null), Is.Not.Null);
        }
    }
}