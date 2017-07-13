using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;

namespace LightCore.Integration.WebApi
{
	public class LightCoreWebApiDependencyResolver : IDependencyResolver
	{
		private readonly IContainer _container;

		public LightCoreWebApiDependencyResolver(IContainer container)
		{
			if (container == null) throw new ArgumentNullException("container");
			_container = container;
		}

		public object GetService(Type serviceType)
		{
			try
			{
				return _container.Resolve(serviceType);
			}
			// IDependencyResolver implementations must not throw an exception 
			// but return null if type is not registered
			catch (RegistrationNotFoundException registrationNotFoundException)
			{
				return null;
			}
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			try {
				return _container.ResolveAll(serviceType);
			}
			// IDependencyResolver implementations must not throw an exception 
			// but return an empty object collection if type is not registered
			catch (RegistrationNotFoundException registrationNotFoundException)
			{
				return new object[] {};
			}
		}

		public IDependencyScope BeginScope()
		{
			return this;
		}

		public void Dispose()
		{
			// no-op as BeginScope == this
		}
	}
}