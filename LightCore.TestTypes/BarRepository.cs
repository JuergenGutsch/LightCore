using System.Collections.Generic;

namespace LightCore.TestTypes
{
    public class BarRepository : IFooRepository
    {
        private ILogger _logger;

        public BarRepository(ILogger logger)
        {
            this._logger = logger;
        }

        public ILogger Logger
        {
            get
            {
                return this._logger;
            }
        }

        public IEnumerable<string> GetFoos()
        {
            yield return "Foo";
            yield return "Bar";
        }
    }
}