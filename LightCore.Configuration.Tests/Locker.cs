using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LightCore.Configuration.Tests
{
    public class Locker
    {
        public static readonly object Lock = new object();
    }
}
