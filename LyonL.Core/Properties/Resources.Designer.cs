﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LyonL.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("LyonL.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
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
        ///   Looks up a localized string similar to Input string was not in the correct format..
        /// </summary>
        internal static string BaseConverter_FormatExceptionMessage {
            get {
                return ResourceManager.GetString("BaseConverter_FormatExceptionMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Expected string with at least two digits.
        /// </summary>
        internal static string BaseConverter_OutOfRangeMessage {
            get {
                return ResourceManager.GetString("BaseConverter_OutOfRangeMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DataBinder_Invalid_Indexed_Expr.
        /// </summary>
        internal static string DataBinder_Invalid_Indexed_Expr {
            get {
                return ResourceManager.GetString("DataBinder_Invalid_Indexed_Expr", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DataBinder_No_Indexed_Accessor.
        /// </summary>
        internal static string DataBinder_No_Indexed_Accessor {
            get {
                return ResourceManager.GetString("DataBinder_No_Indexed_Accessor", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Clock moved backwards.  Refusing to generate id for {0} milliseconds.
        /// </summary>
        internal static string InvalidSystemClock {
            get {
                return ResourceManager.GetString("InvalidSystemClock", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to input cannot be negative.
        /// </summary>
        internal static string LongExtensions_ToBase62_RangeExceptionMessage {
            get {
                return ResourceManager.GetString("LongExtensions_ToBase62_RangeExceptionMessage", resourceCulture);
            }
        }
    }
}
