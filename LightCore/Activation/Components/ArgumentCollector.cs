using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using LightCore.Registration;

namespace LightCore.Activation.Components
{
    internal class ArgumentCollector
    {
        public Func<Type, bool> DependencyTypeSelector
        {
            get;
            set;
        }

        public ParameterInfo[] Parameters
        {
            get;
            set;
        }

        public ArgumentContainer Arguments
        {
            get;
            set;
        }

        public ArgumentContainer RuntimeArguments
        {
            get;
            set;
        }

        public Func<Type, object> DependencyResolver
        {
            get;
            set;
        }

        public ArgumentCollector()
        {
            this.Arguments = new ArgumentContainer();
            this.RuntimeArguments = new ArgumentContainer();
        }

        public object[] CollectArguments()
        {
            var finalArguments = new List<object>();

            var dependencyParameters = this.Parameters.Where(p => this.DependencyTypeSelector(p.ParameterType));

            foreach (ParameterInfo parameter in this.Parameters)
            {
                if(this.RuntimeArguments.NamedArguments != null && this.RuntimeArguments.NamedArguments.ContainsKey(parameter.Name))
                {
                    finalArguments.Add(this.RuntimeArguments.NamedArguments[parameter.Name]);
                    continue;
                }

                if(this.RuntimeArguments.AnonymousArguments != null && this.RuntimeArguments.AnonymousArguments.Any(a => a.GetType() == parameter.ParameterType))
                {
                    finalArguments.Add(this.RuntimeArguments.AnonymousArguments.FirstOrDefault(a => a.GetType() == parameter.ParameterType));
                    continue;
                }

                if (this.Arguments.NamedArguments != null && this.Arguments.NamedArguments.ContainsKey(parameter.Name))
                {
                    finalArguments.Add(this.Arguments.NamedArguments[parameter.Name]);
                    continue;
                }

                if (this.Arguments.AnonymousArguments != null && this.Arguments.AnonymousArguments.Any(a => a.GetType() == parameter.ParameterType))
                {
                    finalArguments.Add(this.Arguments.AnonymousArguments.FirstOrDefault(a => a.GetType() == parameter.ParameterType));
                    continue;
                }

                if (dependencyParameters.Contains(parameter))
                {
                    finalArguments.Add(this.DependencyResolver(parameter.ParameterType));
                    continue;
                }
            }

            return finalArguments.ToArray();
        }
    }
}