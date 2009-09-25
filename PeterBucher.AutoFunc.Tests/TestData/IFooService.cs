using System.Collections.Generic;

namespace PeterBucher.AutoFunc.Tests.TestData
{
    public interface IFooService
    {
        ILogger Logger { get; }
        IEnumerable<string> GetFoos();
    }
}