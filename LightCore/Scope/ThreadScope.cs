using System;
using System.Collections.Generic;

namespace LightCore.Scope
{
    /// <summary>
    /// TODO: Comment.... :)
    /// </summary>
    public class ThreadScope : ScopeBase
    {
        /// <summary>
        /// The instance.
        /// </summary>
        private object _instance;

        [ThreadStatic]
        private static Dictionary<RegistrationKey, object> _instanceMap;

        public override object ReceiveScopedInstance(Func<object> newInstanceResolver)
        {
            throw new NotImplementedException();
        }
    }
}