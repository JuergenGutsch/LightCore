using System.Collections.Generic;

namespace LightCore.Web.Mvc.IntegrationSample.Models
{
    public class WelcomeRepository : IWelcomeRepository
    {
        private readonly IFoo _foo;

        public WelcomeRepository(IFoo foo)
        {
            this._foo = foo;
        }

        public IEnumerable<string> GetWelcomeText()
        {
            yield return "Hello ";
            yield return "Wold, it works!";
            yield return "Foo is not null: " + (this._foo != null);
        }
    }
}