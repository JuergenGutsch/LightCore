﻿using System;
using System.Web.Mvc;

namespace LightCore.Integration.Web.Mvc
{
    /// <summary>
    /// Represents a default controller factory that works with a <see cref="IContainer" />.
    /// </summary>
    public class ControllerFactory : DefaultControllerFactory
    {
        private readonly IContainer _container;

        /// <summary>
        /// Initializes a new instance of <see cref="ControllerFactory" />.
        /// </summary>
        /// <param name="container">The container.</param>
        public ControllerFactory(IContainer container)
        {
            this._container = container;
        }

        /// <summary>
        /// Gets the controller instance.
        /// </summary>
        /// <param name="controllerType">Type of the controller.</param>
        /// <returns>
        /// A reference to the controller.
        /// </returns>
        protected override IController GetControllerInstance(Type controllerType)
        {
            return (IController)this._container.Resolve(controllerType);
        }
    }
}