using System.Collections.Generic;

namespace PeterBucher.AutoFunc.TestTypes
{
    public interface IWelcomeRepository
    {
        IEnumerable<string> GetWelcomeText();
    }
}