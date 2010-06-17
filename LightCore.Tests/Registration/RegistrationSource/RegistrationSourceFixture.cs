using System;
using System.Collections.Generic;

using LightCore.Registration;
using LightCore.Registration.RegistrationSource;

namespace LightCore.Tests.Registration.RegistrationSource
{
    public class RegistrationSourceFixture
    {
        internal IRegistrationSource GetConcreteRegistrationSource()
        {
            return new LightCore.Registration.RegistrationSource.ConcreteTypeRegistrationSource();
        }

        internal IRegistrationSource GetEnumerableRegistrationSource()
        {
            return
                new LightCore.Registration.RegistrationSource.EnumerableRegistrationSource(new RegistrationContainer());
        }

        internal IRegistrationSource GetEnumerableRegistrationSource(Type typeToRegister) {
            return new LightCore.Registration.RegistrationSource.EnumerableRegistrationSource(
                new RegistrationContainer {
                    Registrations =
                        new Dictionary<Type, RegistrationItem>
                                {
                                    {
                                        typeToRegister,
                                        new RegistrationItem(typeToRegister)
                                        }
                                }
                });
        }

        internal IRegistrationSource GetArrayRegistrationSource()
        {
            return
                new LightCore.Registration.RegistrationSource.ArrayRegistrationSource(new RegistrationContainer());
        }

        internal IRegistrationSource GetArrayRegistrationSource(Type typeToRegister)
        {
            return new LightCore.Registration.RegistrationSource.ArrayRegistrationSource(
                new RegistrationContainer
                    {
                        Registrations =
                            new Dictionary<Type, RegistrationItem>
                                {
                                    {
                                        typeToRegister,
                                        new RegistrationItem(typeToRegister)
                                        }
                                }
                    });
        }

        internal IRegistrationSource GetFactoryRegistrationSource(Type typeToRegister)
        {
            return new LightCore.Registration.RegistrationSource.FactoryRegistrationSource(
                new RegistrationContainer
                    {
                        Registrations = new Dictionary<Type, RegistrationItem>
                                            {
                                                {
                                                    typeToRegister,
                                                    new RegistrationItem(typeToRegister)
                                                    }
                                            }
                    });
        }

        internal IRegistrationSource GetOpenGenericRegistrationSource(Type contractType, Type implementationType)
        {
            return new LightCore.Registration.RegistrationSource.OpenGenericRegistrationSource(
                new RegistrationContainer
                    {
                        Registrations =
                            new Dictionary<Type, RegistrationItem>
                                {
                                    {
                                        contractType,
                                        new RegistrationItem(contractType)
                                            {
                                                ImplementationType = implementationType
                                            }
                                        }
                                }
                    });
        }
    }
}