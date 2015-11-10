using FluentAssertions;
using LightCore.TestTypes;
using Xunit;

namespace LightCore.Tests.Registration.RegistrationSource.ConcreteTypeRegistrationSource
{
    public class WhenDependencySelectorIsCalled : RegistrationSourceFixture
    {
        [Fact]
        public void WithConcreteType_TheSourceCanHandle()
        {
            var registrationSource = GetConcreteRegistrationSource();

            registrationSource.SourceSupportsTypeSelector(typeof (Foo)).Should().BeTrue();
        }

        [Fact]
        public void WithAbstractType_TheSourceCannotHandle()
        {
            var registrationSource = GetConcreteRegistrationSource();

            registrationSource.SourceSupportsTypeSelector(typeof (FooBase)).Should().BeFalse();
        }

        [Fact]
        public void WithInterfaceType_TheSourceCannotHandle()
        {
            var registrationSource = GetConcreteRegistrationSource();

            registrationSource.SourceSupportsTypeSelector(typeof (IFoo)).Should().BeFalse();
        }
    }
}