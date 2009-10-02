using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

using PeterBucher.AutoFunc.Builder;
using PeterBucher.AutoFunc.ExtensionMethods;
using PeterBucher.AutoFunc.Integration.Web.Reuse;
using PeterBucher.AutoFunc.Reuse;

namespace PeterBucher.AutoFunc.Integration.Web.Mvc
{
    /// <summary>
    /// Represents a <see cref="RegistrationModule" /> for ASP.NET MVC controllers.
    /// </summary>
    public class AutoFuncControllerRegistrationModule : RegistrationModule
    {
        /// <summary>
        /// The assembly from the controller implementation types.
        /// </summary>
        private readonly List<Assembly> _controllerAssembly;

        /// <summary>
        /// The <see cref="IReuseStrategy" /> for the registrations.
        /// TODO: One reuse strategy instance per registration should there!
        /// </summary>
        private IReuseStrategy _reuseStrategy = new HttpRequestReuseStrategy();

        /// <summary>
        /// Gets or sets the reuse strategy to use for all controllers.
        /// </summary>
        public IReuseStrategy ReuseStrategy
        {
            get
            {
                return this._reuseStrategy;
            }

            set
            {
                this._reuseStrategy = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of <see cref="AutoFuncControllerRegistrationModule" />.
        /// </summary>
        protected AutoFuncControllerRegistrationModule()
        {
            this._controllerAssembly = new List<Assembly>();
        }

        /// <summary>
        /// Initializes a new instance of <see cref="AutoFuncControllerRegistrationModule" />.
        /// </summary>
        /// <param name="controllerAssemblies">The controller assemblies</param>
        public AutoFuncControllerRegistrationModule(params Assembly[] controllerAssemblies)
            : this()
        {
            this._controllerAssembly.AddRange(controllerAssemblies);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="AutoFuncControllerRegistrationModule" />.
        /// </summary>
        /// <param name="controllerAssemblies">The controller assemblies</param>
        public AutoFuncControllerRegistrationModule(IEnumerable<Assembly> controllerAssemblies)
            : this()
        {
            this._controllerAssembly.AddRange(controllerAssemblies);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="AutoFuncControllerRegistrationModule" />.
        /// </summary>
        /// <param name="assemblyNames">The names where controller types lives in.</param>
        public AutoFuncControllerRegistrationModule(params string[] assemblyNames)
            : this()
        {
            this._controllerAssembly.AddRange(assemblyNames.Convert(n => Assembly.Load(n)));
        }

        /// <summary>
        /// Initializes a new instance of <see cref="AutoFuncControllerRegistrationModule" />.
        /// </summary>
        /// <param name="assemblyNames">The names where controller types lives in.</param>
        public AutoFuncControllerRegistrationModule(IEnumerable<string> assemblyNames)
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
            Type typeOfIController = typeof(IController);
            Type[] allPublicTypes = controllerAssembly.GetExportedTypes();

            var controllerTypes = allPublicTypes.Where(t => typeOfIController.IsAssignableFrom(t) && !t.IsAbstract);

            controllerTypes.ForEach(t => containerBuilder
                                             .Register(typeOfIController, t)
                                             .ScopedTo(this._reuseStrategy)
                                             .WithName(this.GetControllerName(t.Name)));
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