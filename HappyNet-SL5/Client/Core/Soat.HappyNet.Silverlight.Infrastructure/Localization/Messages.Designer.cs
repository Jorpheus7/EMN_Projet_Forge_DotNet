﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Soat.HappyNet.Silverlight.Infrastructure.Localization {
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
    public class Messages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Messages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Soat.HappyNet.Silverlight.Infrastructure.Localization.Messages", typeof(Messages).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Problème avec le serveur distant - merci de contacter un administrateur.
        /// </summary>
        public static string ERR_CouldNotRetrieveData {
            get {
                return ResourceManager.GetString("ERR_CouldNotRetrieveData", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Oops, vous avez dû vous tromper d&apos;email ou de mot de passe :(.
        /// </summary>
        public static string ERR_NotAuthentified {
            get {
                return ResourceManager.GetString("ERR_NotAuthentified", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Oops, vous n&apos;avez pas pu être déconnecté ... :(.
        /// </summary>
        public static string ERR_NotLoggedOut {
            get {
                return ResourceManager.GetString("ERR_NotLoggedOut", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Désolé mais vous devez vous authentifier pour exécuter cette action.
        /// </summary>
        public static string ERR_PleaseAuthenticate {
            get {
                return ResourceManager.GetString("ERR_PleaseAuthenticate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Félicitations ! Vous êtes authentifié !.
        /// </summary>
        public static string OK_Authentified {
            get {
                return ResourceManager.GetString("OK_Authentified", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Emballez, c&apos;est payé !.
        /// </summary>
        public static string OK_CheckedOut {
            get {
                return ResourceManager.GetString("OK_CheckedOut", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Vous êtes déconnecté !.
        /// </summary>
        public static string OK_LoggedOut {
            get {
                return ResourceManager.GetString("OK_LoggedOut", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Produit supprimé du panier.
        /// </summary>
        public static string OK_ProductDeletedFromShoppingCart {
            get {
                return ResourceManager.GetString("OK_ProductDeletedFromShoppingCart", resourceCulture);
            }
        }
    }
}
