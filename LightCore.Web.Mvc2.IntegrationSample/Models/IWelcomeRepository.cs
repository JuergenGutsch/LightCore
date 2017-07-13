using System.Collections.Generic;

namespace LightCore.Web.Mvc2.IntegrationSample.Models
{
    public interface IWelcomeRepository
    {
        IEnumerable<string> GetWelcomeText();
    }
}