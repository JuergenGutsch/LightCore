using System.Collections.Generic;

namespace PeterBucher.AutoFunc.TestTypes
{
    public interface IFooRepository
    {
        ILogger Logger { get; }
        IEnumerable<string> GetFoos();
    }
}