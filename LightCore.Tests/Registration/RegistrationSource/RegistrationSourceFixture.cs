using System;
using System.Collections.Generic;
using LightCore.Lifecycle;
using LightCore.Registration;
using LightCore.Registration.RegistrationSource;

namespace LightCore.Tests.Registration.RegistrationSource
{
    public class RegistrationSourceFixture
    {
        public RegistrationSourceFixture()
        {
            var builder = new ContainerBuilder();

            BootStrapContainer = builder.Build();
        }

        internal IContainer BootStrapContainer { get; set; }

        internal IRegistrationSource GetConcreteRegistrationSource()
        {
            return new LightCore.Registration.RegistrationSource.ConcreteTypeRegistrationSource();
        }

        internal IRegistrationSource GetEnumerableRegistrationSource()
        {
            return
                new LightCore.Registration.RegistrationSource.EnumerableRegistrationSource(new RegistrationContainer());
        }

        internal IRegistrationSource GetEnumerableRegistrationSource(Type typeToRegister)
        {
            var registrationContainer = new RegistrationContainer();
            registrationContainer.AddRegistration(new RegistrationItem(typeToRegister));

            return new LightCore.Registration.RegistrationSource.EnumerableRegistrationSource(registrationContainer);
        }

        internal IRegistrationSource GetArrayRegistrationSource()
        {
            return
                new LightCore.Registration.RegistrationSource.ArrayRegistrationSource(new RegistrationContainer());
        }

        internal IRegistrationSource GetArrayRegistrationSource(Type typeToRegister)
        {
            var registrationContainer = new RegistrationContainer();
            registrationContainer.AddRegistration(new RegistrationItem(typeToRegister));

            return new LightCore.Registration.RegistrationSource.ArrayRegistrationSource(registrationContainer);
        }

        internal IRegistrationSource GetFactoryRegistrationSource(Type typeToRegister)
        {
            var registrationContainer = new RegistrationContainer();
            registrationContainer.AddRegistration(new RegistrationItem(typeToRegister));

            return new LightCore.Registration.RegistrationSource.FactoryRegistrationSource(registrationContainer);
        }

        internal IRegistrationSource GetOpenGenericRegistrationSource(Type contractType, Type implementationType)
        {
            var registrationContainer = new RegistrationContainer();
            registrationContainer.AddRegistration(new RegistrationItem(contractType)
            {
                ImplementationType = implementationType,
                Lifecycle = new TransientLifecycle()
            });

            return new LightCore.Registration.RegistrationSource.OpenGenericRegistrationSource(registrationContainer);
        }

        internal IRegistrationSource GetLazyRegistrationSource(Type typeToRegister)
        {
            var registrationContainer = new RegistrationContainer();
            registrationContainer.AddRegistration(new RegistrationItem(typeToRegister));

            return new LightCore.Registration.RegistrationSource.LazyRegistrationSource(registrationContainer);
        }
    }
}