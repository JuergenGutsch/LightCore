using FluentAssertions;
using LightCore.TestTypes;

using Xunit;

namespace LightCore.Tests.Registration.RegistrationSource.ArrayRegistrationSource
{
    
    public class WhenDependencySelectorIsCalled : RegistrationSourceFixture
    {
        [Fact]
        public void WithArrayType_TheSourceCanHandle()
        {
            var registrationSource = this.GetArrayRegistrationSource(typeof(object));

            registrationSource.SourceSupportsTypeSelector(typeof(object[])).Should().BeTrue();;
        }

        [Fact]
        public void WithAbstractType_TheSourceCannotHandle()
        {
            var registrationSource = this.GetArrayRegistrationSource();

            registrationSource.SourceSupportsTypeSelector(typeof(FooBase)).Should().BeFalse();;
        }

        [Fact]
        public void WithConcreteType_TheSourceCannotHandle()
        {
            var registrationSource = this.GetArrayRegistrationSource();

            registrationSource.SourceSupportsTypeSelector(typeof(Foo)).Should().BeFalse();;
        }
    }
}