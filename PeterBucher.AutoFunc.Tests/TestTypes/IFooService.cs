using System.Collections.Generic;

namespace PeterBucher.AutoFunc.Tests.TestTypes
{
    public interface IFooService
    {
        ILogger Logger { get; }
        IEnumerable<string> GetFoos();
    }
}