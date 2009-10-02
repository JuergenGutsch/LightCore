using System.Collections.Generic;

namespace PeterBucher.AutoFunc.WebIntegrationSample.Models
{
    public interface IWelcomeRepository
    {
        IEnumerable<string> GetWelcomeText();
    }
}