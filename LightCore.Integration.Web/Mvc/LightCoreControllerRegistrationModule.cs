using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

using LightCore.Builder;
using LightCore.Reuse;

namespace LightCore.Integration.Web.Mvc
{
    /// <summary>
    /// Represents a <see cref="RegistrationModule" /> for ASP.NET MVC controllers.
    /// </summary>
    public class LightCoreControllerRegistrationModule<TReuseStrategy> : RegistrationModule where TReuseStrategy : IReuseStrategy, new()
    {
        /// <summary>
        /// The assembly from the controller implementation types.
        /// </summary>
        private readonly List<Assembly> _controllerAssembly;

        /// <summary>
        /// Initializes a new instance of <see cref="LightCoreControllerRegistrationModule" />.
        /// </summary>
        protected LightCoreControllerRegistrationModule()
        {
            this._controllerAssembly = new List<Assembly>();
        }

        /// <summary>
        /// Initializes a new instance of <see cref="LightCoreControllerRegistrationModule" />.
        /// </summary>
        /// <param name="controllerAssemblies">The controller assemblies</param>
        public LightCoreControllerRegistrationModule(params Assembly[] controllerAssemblies)
            : this()
        {
            this._controllerAssembly.AddRange(controllerAssemblies);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="LightCoreControllerRegistrationModule" />.
        /// </summary>
        /// <param name="controllerAssemblies">The controller assemblies</param>
        public LightCoreControllerRegistrationModule(IEnumerable<Assembly> controllerAssemblies)
            : this()
        {
            this._controllerAssembly.AddRange(controllerAssemblies);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="LightCoreControllerRegistrationModule" />.
        /// </summary>
        /// <param name="assemblyNames">The names where controller types lives in.</param>
        public LightCoreControllerRegistrationModule(params string[] assemblyNames)
            : this()
        {
            this._controllerAssembly.AddRange(assemblyNames.Convert(n => Assembly.Load(n)));
        }

        /// <summary>
        /// Initializes a new instance of <see cref="LightCoreControllerRegistrationModule" />.
        /// </summary>
        /// <param name="assemblyNames">The names where controller types lives in.</param>
        public LightCoreControllerRegistrationModule(IEnumerable<string> assemblyNames)
            : this()
        {
            this._controllerAssembly.AddRange(assemblyNames.Convert(n => Assembly.Load(n)));
        }

        /// <summary>
        /// Registers all controllers by its name.
        /// </summary>
        /// <param name="containerBuilder">The container builder.</param>
        public override void Register(IContainerBuilder containerBuilder)
        {
            this._controllerAssembly.ForEach(a => this.RegisterControllers(a, containerBuilder));
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

            if (this.ReuseStrategy == null)
            {
                controllerTypes.ForEach(t => containerBuilder
                                                 .Register(typeOfController, t)
                                                 .ScopedTo<TReuseStrategy>()
                                                 .WithName(this.GetControllerName(t.Name)));
            }
            else
            {
                // Unit testing
                controllerTypes.ForEach(t => containerBuilder
                                                 .Register(typeOfController, t)
                                                 .ScopedTo(this.ReuseStrategy)
                                                 .WithName(this.GetControllerName(t.Name)));
            }
        }

        /// <summary>
        /// Gets the controllername used for registration from typename.
        /// </summary>
        /// <param name="typeName">The typename.</param>
        /// <returns>The controllername used for registration.</returns>
        private string GetControllerName(string typeName)
        {
            string controllerPostfix = "Controller";
            int indexOfControllerPostfix = typeName.IndexOf(controllerPostfix);

            return typeName
                .Substring(0, indexOfControllerPostfix)
                .ToLowerInvariant();
        }
    }
}