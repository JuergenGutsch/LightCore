using System;

namespace LightCore
{
    /// <summary>
    /// Represents a registration key.
    /// </summary>
    public class RegistrationKey
    {
        /// <summary>
        /// The name for the registration.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The contract type.
        /// </summary>
        public Type ContractType
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates a new instance of <see cref="Registration" />.
        /// </summary>
        public RegistrationKey()
        {

        }

        /// <summary>
        /// Creates a new instance of <see cref="Registration" />.
        /// </summary>
        /// <param name="contractType">The contract type as <see cref="Type"  />.</param>
        public RegistrationKey(Type contractType)
        {
            this.ContractType = contractType;
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>. </param><exception cref="T:System.NullReferenceException">The <paramref name="obj"/> parameter is null.</exception><filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            var other = (RegistrationKey)obj;

            return other.ContractType == this.ContractType && other.Name == this.Name;
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
            int hashCode = this.ContractType.GetHashCode();

            if (this.Name != null)
            {
                hashCode ^= this.Name.GetHashCode();
            }

            return hashCode;
        }
    }
}