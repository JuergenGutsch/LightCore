using System.Collections.Generic;

namespace PeterBucher.AutoFunc.TestTypes
{
    public class FooRepository : IFooRepository
    {
        private ILogger _logger;

        public FooRepository(ILogger logger)
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