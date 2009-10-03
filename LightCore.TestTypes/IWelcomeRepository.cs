using System.Collections.Generic;

namespace LightCore.TestTypes
{
    public interface IWelcomeRepository
    {
        IEnumerable<string> GetWelcomeText();
    }
}