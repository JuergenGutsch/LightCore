using FluentAssertions;
using LightCore.Activation.Activators;
using LightCore.TestTypes;
using Xunit;

namespace LightCore.Tests.Registration.RegistrationSource.OpenGenericRegistrationSource
{
    public class WhenGetRegistrationForCoreIsCalled : RegistrationSourceFixture
    {
        [Fact]
        public void WithOpenGenericType_RegistrationItemReturned()
        {
            var registrationSource = this.GetOpenGenericRegistrationSource(typeof(IRepository<,>), typeof(Repository<,>));

            var actual = registrationSource.GetRegistrationFor(typeof(IRepository<Foo, int>), this.BootStrapContainer);

            actual.Should().NotBeNull();
        }

        [Fact]
        public void WithOpenGenericType_RegistrationItemReturnedAndHoldsRightData()
        {
            var registrationSource = this.GetOpenGenericRegistrationSource(typeof(IRepository<>), typeof(Repository<>));

            var actual = registrationSource.GetRegistrationFor(typeof(IRepository<Foo>), this.BootStrapContainer);

            actual.Should().NotBeNull();
            actual.ContractType.Should().BeAssignableTo<IRepository<Foo>>();
            actual.ImplementationType.Should().BeAssignableTo<Repository<Foo>>();
            actual.Activator.Should().BeOfType<ReflectionActivator>();
        }
    }
}