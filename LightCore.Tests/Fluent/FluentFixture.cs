using LightCore.Fluent;
using LightCore.Registration;

namespace LightCore.Tests.Fluent
{
    public class FluentFixture
    {
        internal IFluentRegistration GetRegistration(RegistrationItem registrationItem)
        {
            return new LightCore.Fluent.FluentRegistration(registrationItem);
        }
    }
}