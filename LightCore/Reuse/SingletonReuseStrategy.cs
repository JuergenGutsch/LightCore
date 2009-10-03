using System;

namespace LightCore.Reuse
{
    /// <summary>
    /// Represents a singleton per registration strategy.
    /// </summary>
    public class SingletonReuseStrategy : IReuseStrategy
    {
        /// <summary>
        /// The instance.
        /// </summary>
        private object _instance;

        /// <summary>
        /// Initializes a new instance of <see cref="SingletonReuseStrategy" />.
        /// </summary>
        public SingletonReuseStrategy()
        {

        }

        /// <summary>
        /// Handle the reuse of instances.
        /// One instance per registration.
        /// </summary>
        /// <param name="newInstanceResolver">The resolve function for a new instance.</param>
        public object HandleReuse(Func<object> newInstanceResolver)
        {
            if (this._instance == null)
            {
                this._instance = newInstanceResolver();
            }

            return this._instance;
        }
    }
}