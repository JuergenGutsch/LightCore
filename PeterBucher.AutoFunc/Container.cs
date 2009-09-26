﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using PeterBucher.AutoFunc.Exceptions;

namespace PeterBucher.AutoFunc
{
    /// <summary>
    /// Represents the implementation for a inversion of control container.
    /// </summary>
    public class Container : IContainer
    {
        /// <summary>
        /// Selector for non dependency parameters.
        /// </summary>
        private readonly Func<ParameterInfo, bool> _nonDependencyParameterSelector;

        /// <summary>
        /// Selector for dependency parameters.
        /// </summary>
        private readonly Func<ParameterInfo, bool> _dependencyParameterSelector;

        /// <summary>
        /// Holds a dictionary with registered registration keys and their corresponding registrations.
        /// </summary>
        private readonly IDictionary<RegistrationKey, Registration> _registrations;

        /// <summary>
        /// Initializes a new instance of <see cref="Container" />.
        /// </summary>
        internal Container(IDictionary<RegistrationKey, Registration> registrations)
        {
            // Save registrations.
            this._registrations = registrations;

            // Setup selectors.
            this._nonDependencyParameterSelector = p => !(p.ParameterType.IsInterface || p.ParameterType.IsAbstract);
            this._dependencyParameterSelector = F.Not(this._nonDependencyParameterSelector);
        }

        /// <summary>
        /// Resolves a contract (include subcontracts).
        /// </summary>
        /// <typeparam name="TContract">The type of the contract.</typeparam>
        /// <returns>The resolved instance as <see cref="TContract" />.</returns>
        public TContract Resolve<TContract>()
        {
            return (TContract)this.Resolve(typeof(TContract), null);
        }

        /// <summary>
        /// Resolves a contract by name (include subcontracts).
        /// </summary>
        /// <typeparam name="TContract">The type of the contract.</typeparam>
        /// <param name="name">The name given in the registration.</param>
        /// <returns>The resolved instance as <see cref="TContract" />.</returns>
        public TContract ResolveNamed<TContract>(string name)
        {
            return (TContract)this.Resolve(typeof(TContract), name);
        }

        /// <summary>
        /// Resolves a contract (include subcontracts).
        /// </summary>
        /// <param name="typeOfContract">The type of the contract.</param>
        /// <param name="name">The name given in the registration.</param>
        /// <returns>The resolved instance as <see cref="object" />.</returns>
        private object Resolve(Type typeOfContract, string name)
        {
            Func<Type, bool> typeSelector = t => t.Equals(typeOfContract);
            Func<string, bool> nameSelector = n => n == name;

            Func<KeyValuePair<RegistrationKey, Registration>, bool> registrationSelector =
                r => typeSelector(r.Key.ContractType) && nameSelector(r.Key.Name);

            if (!this._registrations.Any(registrationSelector))
            {
                string exceptionMessage = string.Format("registration for contract '{0}' and name '{1}' not found",
                                                        typeOfContract.Name, name);

                throw new RegistrationNotFoundException(exceptionMessage);
            }

            // Select registration.
            Registration registration = this._registrations.Where(registrationSelector).Single().Value;

            // Handle registration life time and creates an instance on these rules.
            switch (registration.LifeTime)
            {
                case LifeTime.Singleton:
                    if (registration.Instance == null)
                    {
                        registration.Instance = this.CreateInstanceFromRegistration(registration);
                    }

                    return registration.Instance;
            }

            // Implicitly use transient lifetime.
            return this.CreateInstanceFromRegistration(registration);
        }

        /// <summary>
        /// Creates an instance of given registration.
        /// </summary>
        /// <param name="registration">The registration for creating an instance.</param>
        /// <returns>The created instance.</returns>
        private object CreateInstanceFromRegistration(Registration registration)
        {
            Type implementationType = registration.ImplementationType;
            object[] arguments = registration.Arguments;
            ConstructorInfo[] constructors = implementationType.GetConstructors();

            bool onlyDefaultConstructorAvailable =
                constructors.Length == 1 && constructors[0].GetParameters().Length == 0;

            // Use the default constructor.
            if (onlyDefaultConstructorAvailable || registration.UseDefaultConstructor)
            {
                return Activator.CreateInstance(implementationType);
            }

            // Select constructor that matches the given arguments.
            if (arguments != null)
            {
                var constructorCandidates = constructors.Where(
                    delegate(ConstructorInfo c)
                    {
                        var parameters = c.GetParameters();
                        return parameters != null && parameters.Where(this._nonDependencyParameterSelector).Count() == arguments.Count();
                    });

                // Only one constructor with same parameter count.
                if (constructorCandidates.Count() == 1)
                {
                    return this.InvokeConstructor(constructorCandidates.First(), arguments);
                }

                // Find the right constructor according the parameter types.
                foreach (var constructor in constructorCandidates.OrderByDescending(c => c.GetParameters().Count()))
                {
                    if (ConstructorParameterTypesMatch(constructor.GetParameters().Where(this._nonDependencyParameterSelector).ToArray(), arguments))
                    {
                        return this.InvokeConstructor(constructor, arguments);
                    }
                }

                // No constructor found.
                string exceptionMessage = string.Format("constructor for type '{0}' not found", implementationType.Name);
                throw new ResolvingFailedException(exceptionMessage);
            }

            // Select the constructor with most parameters (dependencies).
            ConstructorInfo constructorWithMostParameters = constructors.OrderByDescending(
                delegate(ConstructorInfo c)
                {
                    var parameters = c.GetParameters();
                    return parameters != null && parameters.Count() > 0;
                }).First();

            // Invoke constructor with arguments and return it to the caller.
            return this.InvokeConstructor(constructorWithMostParameters, null);
        }

        /// <summary>
        /// Invokes a constructor with optional given arguments.
        /// Automatically detects the right resolved arguments and arguments,
        /// and injects these into the constructor invocation.
        /// </summary>
        /// <param name="constructor">The constructor to invoke.</param>
        /// <param name="arguments">The optional arguments.</param>
        /// <returns>The resolved object.</returns>
        private object InvokeConstructor(ConstructorInfo constructor, IEnumerable<object> arguments)
        {
            ParameterInfo[] parameters = constructor.GetParameters();
            var finalArguments = new List<object>();

            // If there are depdendency parameters, resolve these.
            if (parameters.Any(_dependencyParameterSelector))
            {
                var dependencyParameters = parameters.Where(_dependencyParameterSelector);
                finalArguments.AddRange(this.ResolveDependendyParameters(dependencyParameters));

                if (arguments != null)
                {
                    finalArguments.AddRange(arguments);
                }

                return constructor.Invoke(finalArguments.ToArray());
            }

            // There are only non depdency arguments.
            return constructor.Invoke(arguments.ToArray());
        }

        private IEnumerable<object> ResolveDependendyParameters(IEnumerable<ParameterInfo> parameters)
        {
            // Resolve up all dependencies (recursive).
            foreach (var parameter in parameters)
            {
                yield return this.Resolve(parameter.ParameterType, null);
            }
        }

        /// <summary>
        /// Checks whether the types of parameter infos and arguments matches.
        /// Ignores depdency parameters at the beginning (increment the index from begining on).
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns><value>true</value> if the parameter and argument types match, otherwise <value>false</value>.</returns>
        private bool ConstructorParameterTypesMatch(ParameterInfo[] parameters, object[] arguments)
        {
            int startIndex = 0;
            var depdendencyParameters = parameters.Where(_dependencyParameterSelector);

            if (depdendencyParameters.Any())
            {
                startIndex = depdendencyParameters.Count();
            }

            for (int i = startIndex; i < arguments.Length; i++)
            {
                if (parameters[i].ParameterType != arguments[i].GetType())
                {
                    return false;
                }
            }

            return true;
        }
    }
}