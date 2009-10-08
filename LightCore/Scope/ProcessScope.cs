using System;

namespace LightCore.Scope
{
    /// <summary>
    /// Represents a singleton per registration strategy.
    /// </summary>
    public class ProcessScope : ScopeBase
    {
        /// <summary>
        /// The instance.
        /// </summary>
        private object _instance;

        /// <summary>
        /// Handle the reuse of instances.
        /// </summary>
        /// <param name="newInstanceResolver"></param>
        public override object ReceiveScopedInstance(Func<object> newInstanceResolver)
        {
            if (this._instance == null)
            {
                this._instance = newInstanceResolver();
            }

            return this._instance;
        }
    }
}