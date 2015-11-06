using FluentAssertions;
using LightCore.Registration;
using Xunit;


namespace LightCore.Tests.Fluent.FluentRegistration
{
    
    public class WhenWithGroupIsCalled : FluentFixture
    {
        [Fact]
        public void WithNull_GroupIsSetToNull()
        {
            var registrationItem = new RegistrationItem(null);
            var fluentRegistration = this.GetRegistration(registrationItem);

            fluentRegistration.WithGroup(null);

            registrationItem.Group.Should().Be(null);
        }

        [Fact]
        public void WithString_GroupIsSetToStringValue()
        {
            var registrationItem = new RegistrationItem(null);
            var fluentRegistration = this.GetRegistration(registrationItem);
            var group = "test";

            fluentRegistration.WithGroup(group);

            registrationItem.Group.Should().Be(group);
        }
    }
}