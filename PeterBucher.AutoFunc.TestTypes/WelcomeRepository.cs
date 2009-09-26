using System.Collections.Generic;

namespace PeterBucher.AutoFunc.TestTypes
{
    public class WelcomeRepository : IWelcomeRepository
    {
        public IEnumerable<string> GetWelcomeText()
        {
            yield return "Hello ";
            yield return "World, it works!";
        }
    }
}