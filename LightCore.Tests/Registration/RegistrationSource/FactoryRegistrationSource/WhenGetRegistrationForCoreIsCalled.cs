using System;

using LightCore.Lifecycle;
using LightCore.TestTypes;
using FluentAssertions;
using LightCore.Activation.Activators;
using LightCore.Tests.Integration;
using Xunit;

namespace LightCore.Tests.Registration.RegistrationSource.FactoryRegistrationSource
{
    public class WhenGetRegistrationForCoreIsCalled : RegistrationSourceFixture
    {
        [Fact]
        public void WithFuncType_RegistrationItemReturned()
        {
            var registrationSource = this.GetFactoryRegistrationSource(typeof(IFoo));

            var actual = registrationSource.GetRegistrationFor(typeof(Func<IFoo>), null);

            actual.Should().NotBeNull();
        }

        [Fact]
        public void WithFuncTypeAndArguments_RegistrationReturned()
        {
            var registrationSource = this.GetFactoryRegistrationSource(typeof(IFoo));

            var actual = registrationSource.GetRegistrationFor(typeof(Func<string, IFoo>), null);

            actual.Should().NotBeNull();
        }

        [Fact]
        public void WithFuncTypeAndArguments_RegistrationItemReturnedAndHoldsRightData()
        {
            var registrationSource = this.GetFactoryRegistrationSource(typeof(IFoo));

            var actual = registrationSource.GetRegistrationFor(typeof(Func<string, IFoo>), null);

            actual.Should().NotBeNull();
            actual.ContractType.Should().BeAssignableTo<Func<string, IFoo>>();
            actual.ImplementationType.Should().BeAssignableTo<Func<string, IFoo>>();
            actual.Activator.Should().BeOfType<DelegateActivator>();
        }
    }
}