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
            return new LightCore.Registration.RegistrationSource.EnumerableRegistrationSource
                       {
                           RegistrationContainer = new RegistrationContainer()
                       };
        }

        internal IRegistrationSource GetEnumerableRegistrationSource(Type typeToRegister)
        {
            var registrationKey = new RegistrationKey(typeToRegister);

            return new LightCore.Registration.RegistrationSource.EnumerableRegistrationSource
                       {
                           RegistrationContainer = new RegistrationContainer
                                                       {
                                                           Registrations =
                                                               new Dictionary<RegistrationKey, RegistrationItem>
                                                                   {
                                                                       {
                                                                           registrationKey,
                                                                           new RegistrationItem(registrationKey)
                                                                           }
                                                                   }
                                                       }
                       };
        }

        internal IRegistrationSource GetFactoryRegistrationSource(Type typeToRegister)
        {
            var registrationKey = new RegistrationKey(typeToRegister);

            return new LightCore.Registration.RegistrationSource.FactoryRegistrationSource
            {
                RegistrationContainer = new RegistrationContainer
                {
                    Registrations = new Dictionary<RegistrationKey, RegistrationItem>
                                                                                          {
                                                                                              {
                                                                                                  registrationKey,
                                                                                                  new RegistrationItem(
                                                                                                  registrationKey)
                                                                                                  }
                                                                                          }
                }
            };
        }

        internal IRegistrationSource GetOpenGenericRegistrationSource(Type contractType, Type implementationType)
        {
            var registrationKey = new RegistrationKey(contractType);

            return new LightCore.Registration.RegistrationSource.OpenGenericRegistrationSource
                       {
                           RegistrationContainer = new RegistrationContainer
                                                       {
                                                           Registrations =
                                                               new Dictionary<RegistrationKey, RegistrationItem>
                                                                   {
                                                                       {
                                                                           registrationKey,
                                                                           new RegistrationItem(registrationKey)
                                                                               {ImplementationType = implementationType}
                                                                           }
                                                                   }
                                                       }
                       };
        }

        internal IRegistrationSource GetRegistrationSource()
        {
            return new RegistrationSourceMock();
        }

        private class RegistrationSourceMock : LightCore.Registration.RegistrationSource.RegistrationSource
        {
            protected override RegistrationItem GetRegistrationForCore(Type contractType, IContainer container)
            {
                throw new NotImplementedException();
            }
        }
    }
}