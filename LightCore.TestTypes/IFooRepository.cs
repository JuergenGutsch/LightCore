using System.Collections.Generic;

namespace LightCore.TestTypes
{
    public interface IFooRepository
    {
        ILogger Logger { get; }
        IEnumerable<string> GetFoos();
    }
}