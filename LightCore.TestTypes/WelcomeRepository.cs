using System.Collections.Generic;

namespace LightCore.TestTypes
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