using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

using LightCore.ExtensionMethods.System.Collections.Generic;
using LightCore.Registration;

namespace LightCore.Integration.Web.Mvc
{
    /// <summary>
    /// Represents a <see cref="RegistrationModule" /> for ASP.NET MVC controllers.
    /// </summary>
    public class ControllerRegistrationModule : RegistrationModule
    {
        /// <summary>
        /// The assembly from the controller implementation types.
        /// </summary>
        private readonly List<Assembly> _controllerAssemblies;

        /// <summary>
        /// Initializes a new instance of <see cref="ControllerRegistrationModule" />.
        /// </summary>
        protected ControllerRegistrationModule()
        {
            this._controllerAssemblies = new List<Assembly>();
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ControllerRegistrationModule" />.
        /// </summary>
        /// <param name="controllerAssembly">The controller assembly.</param>
        public ControllerRegistrationModule(Assembly controllerAssembly)
            : this()
        {
            this._controllerAssemblies.Add(controllerAssembly);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ControllerRegistrationModule" />.
        /// </summary>
        /// <param name="controllerAssemblies">The controller assemblies.</param>
        public ControllerRegistrationModule(params Assembly[] controllerAssemblies)
            : this()
        {
            this._controllerAssemblies.AddRange(controllerAssemblies);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ControllerRegistrationModule" />.
        /// </summary>
        /// <param name="controllerAssemblies">The controller assemblies.</param>
        public ControllerRegistrationModule(IEnumerable<Assembly> controllerAssemblies)
            : this()
        {
            this._controllerAssemblies.AddRange(controllerAssemblies);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ControllerRegistrationModule" />.
        /// </summary>
        /// <param name="assemblyNames">The names where controller types lives in.</param>
        public ControllerRegistrationModule(params string[] assemblyNames)
            : this()
        {
            this._controllerAssemblies.AddRange(assemblyNames.Select(assembly => Assembly.Load(assembly)));
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ControllerRegistrationModule" />.
        /// </summary>
        /// <param name="assemblyNames">The names where controller types lives in.</param>
        public ControllerRegistrationModule(IEnumerable<string> assemblyNames)
            : this()
        {
            this._controllerAssemblies.AddRange(assemblyNames.Select(assembly => Assembly.Load(assembly)));
        }

        /// <summary>
        /// Registers all controllers by its name.
        /// </summary>
        /// <param name="containerBuilder">The container builder.</param>
        public override void Register(IContainerBuilder containerBuilder)
        {
            this._controllerAssemblies.ForEach(a => this.RegisterControllers(a, containerBuilder));
        }

        /// <summary>
        /// Registers all controllers within one assembly.
        /// </summary>
        /// <param name="controllerAssembly">The controller assembly.</param>
        /// <param name="containerBuilder">The container builder.</param>
        private void RegisterControllers(Assembly controllerAssembly, IContainerBuilder containerBuilder)
        {
            Type typeOfController = typeof(IController);
            Type[] allPublicTypes = controllerAssembly.GetExportedTypes();

            var controllerTypes = allPublicTypes.Where(t => typeOfController.IsAssignableFrom(t) && !t.IsAbstract);

            controllerTypes.ForEach(t => containerBuilder.Register(t, t));
        }
    }
}