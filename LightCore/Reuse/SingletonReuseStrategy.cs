﻿using System;

namespace LightCore.Reuse
{
    /// <summary>
    /// Represents a singleton per registration strategy.
    /// </summary>
    public class SingletonReuseStrategy : IReuseStrategy
    {
        /// <summary>
        /// The registration, the strategy belongs to.
        /// </summary>
        private readonly Registration _registration;

        /// <summary>
        /// Initializes a new instance of <see cref="SingletonReuseStrategy" />.
        /// </summary>
        /// <param name="registration">The registration, the strategy belongs to.</param>
        public SingletonReuseStrategy(Registration registration)
        {
            this._registration = registration;
        }

        /// <summary>
        /// Handle the reuse of instances.
        /// One instance per registration.
        /// </summary>
        /// <param name="newInstanceResolver">The resolve function for a new instance.</param>
        public object HandleReuse(Func<object> newInstanceResolver)
        {
            if (this._registration.Instance == null)
            {
                this._registration.Instance = newInstanceResolver();
            }

            return this._registration.Instance;
        }
    }
}