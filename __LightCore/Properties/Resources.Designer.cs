﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LightCore.Properties {
    using System;
    using System.Reflection;
    
    
    /// <summary>
    ///    A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        internal Resources() {
        }
        
        /// <summary>
        ///    Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("LightCore.Properties.Resources", typeof(Resources).GetTypeInfo().Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///    Overrides the current thread's CurrentUICulture property for all
        ///    resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Contract &apos;{0}&apos; is not implemented by type &apos;{1}&apos;..
        /// </summary>
        internal static string ContractNotImplementedByTypeFormat {
            get {
                return ResourceManager.GetString("ContractNotImplementedByTypeFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Registration to self failed, type &apos;{0}&apos; seems to be not concrete..
        /// </summary>
        internal static string InvalidRegistrationFormat {
            get {
                return ResourceManager.GetString("InvalidRegistrationFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to No constructor available for type &apos;{0}&apos;. Please add a public constructor..
        /// </summary>
        internal static string NoConstructorAvailableForType {
            get {
                return ResourceManager.GetString("NoConstructorAvailableForType", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to No suitable constructor for implementation &apos;{0}&apos; found. Check registered dependencies and availability of default constructors..
        /// </summary>
        internal static string NoSuitableConstructorFoundFormat {
            get {
                return ResourceManager.GetString("NoSuitableConstructorFoundFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Passed type &apos;{0}&apos; does not implement the interface &quot;ILifecycle&quot;..
        /// </summary>
        internal static string PassedTypeDoesNotImplementILifecycleFormat {
            get {
                return ResourceManager.GetString("PassedTypeDoesNotImplementILifecycleFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Registration for contract &apos;{0}&apos; was found to be duplicate. Either you want to duplicate or not. Duplicate registrations can only resolved all at once through the &apos;ResolveAll&apos; method.
        /// </summary>
        internal static string RegistrationNotFoundBecauseDuplicate {
            get {
                return ResourceManager.GetString("RegistrationNotFoundBecauseDuplicate", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Registration for contract &apos;{0}&apos; not found, please check your registrations..
        /// </summary>
        internal static string RegistrationNotFoundFormat {
            get {
                return ResourceManager.GetString("RegistrationNotFoundFormat", resourceCulture);
            }
        }
    }
}
