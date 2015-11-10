using FluentAssertions;
using LightCore.Activation.Activators;
using LightCore.Lifecycle;
using LightCore.TestTypes;
using Xunit;

namespace LightCore.Tests.Registration.RegistrationSource.ConcreteTypeRegistrationSource
{
    public class WhenGetRegistrationForCoreIsCalled : RegistrationSourceFixture
    {
        [Fact]
        public void WithConcreteType_RegistrationItemReturned()
        {
            var registrationSource = GetConcreteRegistrationSource();

            registrationSource.GetRegistrationFor(typeof (Foo), BootStrapContainer).Should().NotBeNull();
        }

        [Fact]
        public void WithConcreteType_RegistrationItemReturnedAndHoldsRightData()
        {
            var registrationSource = GetConcreteRegistrationSource();

            var registrationItem = registrationSource.GetRegistrationFor(typeof (Foo), BootStrapContainer);

            registrationItem.Should().NotBeNull();
            registrationItem.ContractType.Should().BeAssignableTo<Foo>();
            registrationItem.ImplementationType.Should().BeAssignableTo<Foo>();
            registrationItem.Activator.Should().BeOfType<ReflectionActivator>();
            registrationItem.Lifecycle.Should().BeOfType<TransientLifecycle>();
        }
    }
}