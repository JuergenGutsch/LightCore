using System.Collections.Generic;

using LightCore.TestTypes;

using FluentAssertions;
using Xunit;

namespace LightCore.Tests.Registration.RegistrationSource.EnumerableRegistrationSource
{
    
    public class WhenDependencySelectorIsCalled : RegistrationSourceFixture
    {
        [Fact]
        public void WithEnumerableType_TheSourceCanHandle()
        {
            var registrationSource = this.GetEnumerableRegistrationSource(typeof(object));

            registrationSource.SourceSupportsTypeSelector(typeof(IEnumerable<object>)).Should().BeTrue();
        }

        [Fact]
        public void WithAbstractType_TheSourceCannotHandle()
        {
            var registrationSource = this.GetEnumerableRegistrationSource();

            registrationSource.SourceSupportsTypeSelector(typeof(FooBase)).Should().BeFalse();
        }

        [Fact]
        public void WithConcreteType_TheSourceCannotHandle()
        {
            var registrationSource = this.GetEnumerableRegistrationSource();

            registrationSource.SourceSupportsTypeSelector(typeof(Foo)).Should().BeFalse();
        }
    }
}