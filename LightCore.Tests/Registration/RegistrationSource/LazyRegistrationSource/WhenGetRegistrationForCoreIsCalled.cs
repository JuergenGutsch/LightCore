using System;
using FluentAssertions;
using LightCore.TestTypes;
using Xunit;

namespace LightCore.Tests.Registration.RegistrationSource.LazyRegistrationSource
{
    public class WhenGetRegistrationForCoreIsCalled : RegistrationSourceFixture
    {
        [Fact]
        public void WithLazyType_RegistrationItemReturned()
        {
            var registrationSource = GetLazyRegistrationSource(typeof (IFoo));

            var actual = registrationSource.GetRegistrationFor(typeof (Lazy<IFoo>), null);

            actual.Should().NotBeNull();
        }
    }
}