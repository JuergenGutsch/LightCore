using System.Collections.Generic;

namespace PeterBucher.AutoFunc.Tests.TestData
{
    public interface IFooRepository
    {
        IEnumerable<string> GetFoos();
    }
}