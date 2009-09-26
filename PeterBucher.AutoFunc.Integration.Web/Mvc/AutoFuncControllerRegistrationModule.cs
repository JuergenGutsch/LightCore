using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

using PeterBucher.AutoFunc.Build;
using PeterBucher.AutoFunc.ExtensionMethods;

namespace PeterBucher.AutoFunc.Integrations.Web.Mvc
{
    /// <summary>
    /// Represents a <see cref="RegistrationModule" /> for ASP.NET MVC controllers.
    /// </summary>
    public class AutoFuncControllerRegistrationModule : RegistrationModule
    {
        /// <summary>
        /// The assembly from the controller implementation types.
        /// </summary>
        private readonly Assembly _controllerAssembly;

        /// <summary>
        /// Initializes a new instance of <see cref="AutoFuncControllerRegistrationModule" />.
        /// </summary>
        /// <param name="controllerAssembly">The controller assembly</param>
        public AutoFuncControllerRegistrationModule(Assembly controllerAssembly)
        {
            this._controllerAssembly = controllerAssembly;
        }

        /// <summary>
        /// Registers all controllers by its name.
        /// </summary>
        /// <param name="containerBuilder">The container builder.</param>
        public override void Register(IContainerBuilder containerBuilder)
        {
            Type typeOfIController = typeof(IController);
            Type[] allPublicTypes = this._controllerAssembly.GetExportedTypes();

            var controllerTypes = allPublicTypes
                .Where(t => typeof(IController).IsAssignableFrom(t) && !t.IsAbstract);

            string controllerIdentifier = "Controller";

            controllerTypes.ForEach(t => containerBuilder
                                             .Register(typeOfIController, t)
                                             .WithName(
                                             t.Name.Replace(controllerIdentifier, String.Empty).ToLowerInvariant())
                );
        }
    }
}