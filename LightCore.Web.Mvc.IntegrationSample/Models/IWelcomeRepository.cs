using System.Collections.Generic;

namespace LightCore.Web.Mvc.IntegrationSample.Models
{
    public interface IWelcomeRepository
    {
        IEnumerable<string> GetWelcomeText();
    }
}