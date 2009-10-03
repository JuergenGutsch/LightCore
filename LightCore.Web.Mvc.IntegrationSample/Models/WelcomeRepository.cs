using System.Collections.Generic;

namespace LightCore.Web.Mvc.IntegrationSample.Models
{
    public class WelcomeRepository : IWelcomeRepository
    {
        public IEnumerable<string> GetWelcomeText()
        {
            yield return "Hello ";
            yield return "Wold, it works!";
        }
    }
}