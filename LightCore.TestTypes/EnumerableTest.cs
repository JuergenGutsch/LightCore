using System.Collections.Generic;

namespace LightCore.TestTypes
{
    public class EnumerableTest
    {
        private readonly IEnumerable<IBar> _bars;
        
        public EnumerableTest()
        {
            
        }

        public EnumerableTest(IEnumerable<IBar> bars)
        {
            this._bars = bars;
        }

        public IEnumerable<IBar> Bars
        {
            get
            {
                return this._bars;
            }
        }
    }
}