using System;

namespace LightCore.Lifecycle
{
    /// <summary>
    /// Represents a singleton per registration strategy on process scope (??)
    /// </summary>
    public class SingletonLifecycle : ILifecycle
    {
        /// <summary>
        /// The instance.
        /// </summary>
        private object _instance;

        /// <summary>
        /// Handle the reuse of instances.
        /// </summary>
        /// <param name="newInstanceResolver"></param>
        public object ReceiveInstanceInLifecycle(Func<object> newInstanceResolver)
        {
            if (this._instance == null)
            {
                this._instance = newInstanceResolver();
            }

            return this._instance;
        }
    }
}