using System;
using System.Collections.Generic;
using FluentAssertions;
using LightCore.Activation;
using LightCore.Activation.Activators;
using LightCore.Lifecycle;
using LightCore.Registration;
using LightCore.TestTypes;
using Xunit;

namespace LightCore.Tests.Activation.Activators.ReflectionActivator
{
    public class WhenActivateInstanceIsCalled
    {
        private IActivator GetActivator(Type implementationType)
        {
            return new LightCore.Activation.Activators.ReflectionActivator(
                implementationType,
                new LightCore.Activation.Components.ConstructorSelector(),
                new LightCore.Activation.Components.ArgumentCollector()
                );
        }

        private ResolutionContext GetContext()
        {
            var registrationContainer = new RegistrationContainer();

            return new ResolutionContext(new Container(registrationContainer), registrationContainer);
        }

        private ResolutionContext GetContext(Type contractType, Type implementationType)
        {
            var registrationContainer = new RegistrationContainer();

            var registration = new RegistrationItem(contractType)
            {
                Activator =
                    new LightCore.Activation.Activators.ReflectionActivator
                        (
                            implementationType,
                            new LightCore.Activation.Components.ConstructorSelector(),
                            new LightCore.Activation.Components.ArgumentCollector()
                        ),
                Lifecycle = new TransientLifecycle()
            };
            registrationContainer.AddRegistration(registration);
            
            return new ResolutionContext(new Container(registrationContainer), registrationContainer);
        }

        [Fact]
        public void WithObjectImplementationAndEmptyResolutionContext_NewObjectReturned()
        {
            var reflectionActivator = GetActivator(typeof(object));
            var resolutionContext = GetContext();

            var result = reflectionActivator.ActivateInstance(resolutionContext);

            result.Should().NotBeNull();
            result.Should().BeOfType<object>();
        }

        [Fact]
        public void WithFooImplementationAndEmptyResolutionContext_NewFooReturned()
        {
            var reflectionActivator = GetActivator(typeof(Foo));
            var resolutionContext = GetContext();

            var result = reflectionActivator.ActivateInstance(resolutionContext);

            result.Should().NotBeNull();
            result.Should().BeOfType<Foo>();
        }

        [Fact]
        public void WithFooImplementationAndIBarAvailable_FooWithBarReturned()
        {
            var reflectionActivator = GetActivator(typeof(Foo));
            var resolutionContext = GetContext(typeof(IBar), typeof(Bar));

            var result = reflectionActivator.ActivateInstance(resolutionContext);

            result.Should().NotBeNull();
            result.Should().BeOfType<Foo>();

            ((Foo)result).Bar.Should().NotBeNull();
        }
    }
}