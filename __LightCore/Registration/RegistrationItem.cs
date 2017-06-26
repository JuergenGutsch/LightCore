using System;

using LightCore.Activation;
using LightCore.Activation.Activators;
using LightCore.Lifecycle;

namespace LightCore.Registration
{
    /// <summary>
    /// Represents a registration.
    /// </summary>
    internal class RegistrationItem
    {
        /// <summary>
        /// Gets the key for this registration.
        /// </summary>
        internal Type ContractType
        {
            get;
            private set;
        }
        
        /// <summary>
        /// The group.
        /// </summary>
        internal string Group
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the implementation type for this registration.
        /// </summary>
        internal Type ImplementationType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the activator.
        /// </summary>
        internal IActivator Activator
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the scope that holds the reuse strategy.
        /// </summary>
        internal ILifecycle Lifecycle
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the arguments for object creations.
        /// </summary>
        internal ArgumentContainer Arguments
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the runtime arguments for object creations.
        /// </summary>
        internal ArgumentContainer RuntimeArguments
        {
            get;
            set;
        }

        /// <summary>
        /// Creates a new instance of <see cref="RegistrationItem" />.
        /// </summary>
        internal RegistrationItem()
        {
            this.Arguments = new ArgumentContainer();
            this.RuntimeArguments = new ArgumentContainer();
        }

        /// <summary>
        /// Creates a new instance of <see cref="RegistrationItem" />.
        /// </summary>
        /// <param name="contractType">The contract type as <see cref="Type" />.</param>
        internal RegistrationItem(Type contractType)
            : this()
        {
            this.ContractType = contractType;

            if (this.ImplementationType == null)
            {
                this.ImplementationType = contractType;
            }
        }

        /// <summary>
        /// Activates the current registration.
        /// </summary>
        /// <param name="resolutionContext">The resolution context (e.g. for resolve inner depenencies).</param>
        /// <returns>The activated instance.</returns>
        internal object ActivateInstance(ResolutionContext resolutionContext)
        {
            return this.Lifecycle.ReceiveInstanceInLifecycle(
                () => this.Activator.ActivateInstance(resolutionContext));
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>. </param><filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            return Equals(this, obj as RegistrationItem);
        }

        /// <summary>
        /// Gets whether two keys equals, or not.
        /// </summary>
        /// <param name="obj1">The first key.</param>
        /// <param name="obj2">The second key.</param>
        /// <returns><true /> if the keys equals each other, otherwise false.</returns>
        public static bool Equals(RegistrationItem obj1, RegistrationItem obj2)
        {
            if ((object.Equals(null, obj1) || object.Equals(null, obj2)) || (obj1.GetType() != obj2.GetType()))
            {
                return false;
            }

            return obj1.ContractType == obj2.ContractType
                   && obj1.ImplementationType == obj2.ImplementationType
                   && ReferenceEquals(obj1, obj2);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            return this.ContractType.GetHashCode() ^ this.ImplementationType.GetHashCode();
        }

        public override string ToString()
        {
            return this.ContractType.ToString();
        }
    }
}