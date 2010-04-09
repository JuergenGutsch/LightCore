using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using LightCore.ExtensionMethods.System.Collections.Generic;

namespace LightCore.Registration
{
    /// <summary>
    /// Represents a container for arguments.
    /// </summary>
    internal class ArgumentContainer
    {
        /// <summary>
        /// Contains the anonymous arguments.
        /// </summary>
        internal object[] AnonymousArguments
        {
            get;
            set;
        }

        /// <summary>
        /// Contains the named arguments.
        /// </summary>
        internal IDictionary<string, object> NamedArguments
        {
            get;
            set;
        }

        /// <summary>
        /// Adds values to the anonymous arguments.
        /// </summary>
        /// <param name="arguments">The anonymous arguments to add.</param>
        internal void AddToAnonymousArguments(IEnumerable<object> arguments)
        {
            if (this.AnonymousArguments == null)
            {
                this.AnonymousArguments = arguments.ToArray();
            }
            else
            {
                this.AnonymousArguments = this.AnonymousArguments.Concat(arguments).ToArray();
            }
        }

        /// <summary>
        /// Adds values to the named arguments.
        /// </summary>
        /// <param name="namedArguments">The named arguments to add.</param>
        internal void AddToNamedArguments(IDictionary<string, object> namedArguments)
        {
            if (this.NamedArguments == null)
            {
                this.NamedArguments = namedArguments;
            }
            else
            {
                this.NamedArguments = this.NamedArguments.Merge(namedArguments);
            }
        }

        /// <summary>
        /// Gets the count of all arguments.
        /// </summary>
        internal int CountOfAllArguments
        {
            get
            {
                int count = 0;

                if (this.AnonymousArguments != null)
                {
                    count += this.AnonymousArguments.Length;
                }

                if (this.NamedArguments != null)
                {
                    count += this.NamedArguments.Count;
                }

                return count;
            }
        }

        /// <summary>
        /// Checks wether the value can be supplied from the arguments or not.
        /// </summary>
        /// <param name="parameter">The parameter info.</param>
        /// <returns>Returns <false /> if the value cannot be supplied from arguments, otherwise <true />.</returns>
        internal bool CanSupplyValue(ParameterInfo parameter)
        {
            if (this.NamedArguments != null
                && this.NamedArguments.Keys.Any(k => k == parameter.Name)
                && this.NamedArguments.Values.Any(a => a.GetType() == parameter.ParameterType))
            {
                return true;
            }

            if (this.AnonymousArguments != null
                && this.AnonymousArguments.Any(a => a.GetType() == parameter.ParameterType))
            {
                return true;
            }

            return false;
        }
    }
}