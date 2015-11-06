using FluentAssertions;
using LightCore.Activation.Activators;
using LightCore.Lifecycle;
using LightCore.TestTypes;
using Xunit;


namespace LightCore.Tests.Registration.RegistrationSource.ArrayRegistrationSource
{
    
    public class WhenGetRegistrationForCoreIsCalled : RegistrationSourceFixture
    {
        [Fact]
        public void WithArrayType_RegistrationItemReturned()
        {
            var registrationSource = this.GetArrayRegistrationSource(typeof(IFoo));

            registrationSource.GetRegistrationFor(typeof(IFoo[]), null).Should().NotBeNull();
        }

        [Fact]
        public void WithArrayType_RegistrationItemReturnedAndHoldsRightData()
        {
            var registrationSource = this.GetArrayRegistrationSource(typeof(IBar));

            var registrationItem = registrationSource.GetRegistrationFor(typeof(IBar[]), null);

            registrationItem.ContractType.Should().BeAssignableTo<IBar[]>();
            registrationItem.ImplementationType.Should().BeAssignableTo<IBar[]>();
            registrationItem.Lifecycle.Should().BeOfType<TransientLifecycle>();
        }
    }
}