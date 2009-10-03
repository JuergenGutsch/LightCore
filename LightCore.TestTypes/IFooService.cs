using System.Collections.Generic;

namespace LightCore.TestTypes
{
    public interface IFooService
    {
        ILogger Logger { get; }
        IEnumerable<string> GetFoos();
    }
}