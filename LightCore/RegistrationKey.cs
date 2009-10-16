using System;

namespace LightCore
{
    /// <summary>
    /// Represents a registration key.
    /// </summary>
    public class RegistrationKey : IEquatable<RegistrationKey>
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
        /// Creates a new instance of <see cref="Registration" />.
        /// </summary>
        /// <param name="contractType">The contract type as <see cref="Type"  />.</param>
        /// <param name="name">The name.</param>
        public RegistrationKey(Type contractType, string name) : this(contractType)
        {
            this.Name = name;
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
            var otherKey = obj as RegistrationKey;

            if (otherKey == null)
            {
                return false;
            }

            return Equals(otherKey);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="otherKey"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="otherKey">An object to compare with this object.</param>
        public bool Equals(RegistrationKey otherKey)
        {
            return otherKey.ContractType == this.ContractType
                   && otherKey.Name == this.Name;
        }

        /// <summary>
        /// Represents the behaviour for the equals operator.
        /// </summary>
        /// <param name="leftKey">The left key.</param>
        /// <param name="rightKey">The right key.</param>
        /// <returns><value>true</value> if the registration keys equals.</returns>
        public static bool operator ==(RegistrationKey leftKey, RegistrationKey rightKey)
        {
            return leftKey.Equals(rightKey);
        }

        /// <summary>
        /// Represents the behaviour for the not equals operator.
        /// </summary>
        /// <param name="leftKey">The left key.</param>
        /// <param name="rightKey">The right key.</param>
        /// <returns><value>true</value> if the registration keys equals.</returns>
        public static bool operator !=(RegistrationKey leftKey, RegistrationKey rightKey)
        {
            return !(leftKey == rightKey);
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