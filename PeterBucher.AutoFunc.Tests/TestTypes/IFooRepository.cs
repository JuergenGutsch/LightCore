using System.Collections.Generic;

namespace PeterBucher.AutoFunc.Tests.TestTypes
{
    public interface IFooRepository
    {
        ILogger Logger { get; }
        IEnumerable<string> GetFoos();
    }
}