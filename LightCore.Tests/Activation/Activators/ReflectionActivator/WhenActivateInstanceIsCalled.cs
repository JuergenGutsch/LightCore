using System;

using LightCore.Activation;
using LightCore.Activation.Activators;
using LightCore.Lifecycle;
using LightCore.Registration;
using LightCore.TestTypes;

using NUnit.Framework;

using System.Collections.Generic;

namespace LightCore.Tests.Activation.Activators.ReflectionActivator
{
    [TestFixture]
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
            var registrationContainer = new RegistrationContainer
                                            {
                                                Registrations =
                                                    new Dictionary<Type, RegistrationItem>
                                                        {
                                                            {
                                                                contractType,
                                                                new RegistrationItem(contractType)
                                                                    {
                                                                        Activator =
                                                                            new LightCore.Activation.Activators.ReflectionActivator
                                                                                (implementationType,
                                                                                new LightCore.Activation.Components.ConstructorSelector(),
                                                                                new LightCore.Activation.Components.ArgumentCollector()
                                                                                ),
                                                                        Lifecycle = new TransientLifecycle()
                                                                    }
                                                                }
                                                        }
                                            };

            return new ResolutionContext(new Container(registrationContainer), registrationContainer);
        }

        [Test]
        public void WithObjectImplementationAndEmptyResolutionContext_NewObjectReturned()
        {
            var reflectionActivator = this.GetActivator(typeof(object));
            var resolutionContext = this.GetContext();

            object result = reflectionActivator.ActivateInstance(resolutionContext);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<object>());
        }

        [Test]
        public void WithFooImplementationAndEmptyResolutionContext_NewFooReturned()
        {
            var reflectionActivator = this.GetActivator(typeof(Foo));
            var resolutionContext = this.GetContext();

            object result = reflectionActivator.ActivateInstance(resolutionContext);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<Foo>());
        }

        [Test]
        public void WithFooImplementationAndIBarAvailable_FooWithBarReturned()
        {
            var reflectionActivator = this.GetActivator(typeof(Foo));
            var resolutionContext = this.GetContext(typeof(IBar), typeof(Bar));

            object result = reflectionActivator.ActivateInstance(resolutionContext);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<Foo>());

            Assert.That(((Foo)result).Bar, Is.Not.Null);
        }
    }
}