using System.Collections.Generic;

namespace PeterBucher.AutoFunc.Tests.TestData
{
    public class FooRepository : IFooRepository
    {
        public IEnumerable<string> GetFoos()
        {
            yield return "Foo";
            yield return "Bar";
        }
    }
}