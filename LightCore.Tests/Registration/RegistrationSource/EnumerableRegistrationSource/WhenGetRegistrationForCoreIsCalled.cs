using LightCore.Lifecycle;
using LightCore.TestTypes;

using Xunit;

using System.Collections.Generic;
using FluentAssertions;

namespace LightCore.Tests.Registration.RegistrationSource.EnumerableRegistrationSource
{
    
    public class WhenGetRegistrationForCoreIsCalled : RegistrationSourceFixture
    {
        [Fact]
        public void WithEnumerableType_RegistrationItemReturned()
        {
            var registrationSource = GetEnumerableRegistrationSource(typeof(IFoo));

            registrationSource.GetRegistrationFor(typeof(IEnumerable<IFoo>), BootStrapContainer).Should().NotBeNull();
        }

        [Fact]
        public void WithEnumerableType_RegistrationItemReturnedAndHoldsRightData()
        {
            var registrationSource = GetEnumerableRegistrationSource(typeof(IBar));

            var registrationItem = registrationSource.GetRegistrationFor(typeof(IEnumerable<IBar>), BootStrapContainer);

            registrationItem.Should().NotBeNull();
            registrationItem.ContractType.Should().BeAssignableTo<IEnumerable<IBar>>();
            registrationItem.ImplementationType.Should().BeAssignableTo<IEnumerable<IBar>>();
            registrationItem.Lifecycle.Should().BeOfType<TransientLifecycle>();
        }
    }
}