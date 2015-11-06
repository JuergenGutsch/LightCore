using System;
using FluentAssertions;
using LightCore.Lifecycle;
using LightCore.Registration;
using Xunit;

namespace LightCore.Tests.Fluent.FluentRegistration
{
    public class WhenControlledByIsCalled : FluentFixture
    {
        [Fact]
        public void WithGenericSingletonLifecycleArgument_LifecycleIsSetToSingleton()
        {
            var registrationItem = new RegistrationItem();
            var fluentRegistration = this.GetRegistration(registrationItem);

            fluentRegistration.ControlledBy<SingletonLifecycle>();

            registrationItem.Lifecycle.Should().BeOfType<SingletonLifecycle>();
        }

        [Fact]
        public void WithGenericTransientLifecycleArgument_LifecycleIsSetToTransient()
        {
            var registrationItem = new RegistrationItem();
            var fluentRegistration = this.GetRegistration(registrationItem);

            fluentRegistration.ControlledBy<TransientLifecycle>();

            registrationItem.Lifecycle.Should().BeOfType<TransientLifecycle>();
        }

        [Fact]
        public void WithSingletonLifecycleTypeArgument_LifecycleIsSetToSingleton()
        {
            var registrationItem = new RegistrationItem();
            var fluentRegistration = this.GetRegistration(registrationItem);

            fluentRegistration.ControlledBy(typeof(SingletonLifecycle));

            registrationItem.Lifecycle.Should().BeOfType<SingletonLifecycle>();
        }

        [Fact]
        public void WithObjectAsLifecycle_ArgumentExceptionThrown()
        {
            var registrationItem = new RegistrationItem();
            var fluentRegistration = this.GetRegistration(registrationItem);

            Action act = () => fluentRegistration.ControlledBy(typeof(object));

            act.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void WithNullAsLifecycle_ArgumentExceptionThrown()
        {
            var registrationItem = new RegistrationItem();
            var fluentRegistration = this.GetRegistration(registrationItem);

            Action act = () => fluentRegistration.ControlledBy(null);
            act.ShouldThrow<ArgumentException>();
        }
    }
}