using System.Collections.Generic;

namespace LightCore.TestTypes
{
    public class FooService : IFooService
    {
        private readonly ILogger _logger;
        private readonly IFooRepository _fooRepository;

        public FooService(ILogger logger, IFooRepository fooRepository)
        {
            this._logger = logger;
            this._fooRepository = fooRepository;
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
            return this._fooRepository.GetFoos();
        }
    }
}