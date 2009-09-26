using System.Collections.Generic;

namespace PeterBucher.AutoFunc.TestTypes
{
    public interface IFooService
    {
        ILogger Logger { get; }
        IEnumerable<string> GetFoos();
    }
}