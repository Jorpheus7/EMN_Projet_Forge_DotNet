// -----------------------------------------------------------------------
// <copyright file="Globals.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Regions;
using Soat.HappyNet.Server.DataContract;
using System.Collections.Generic;

namespace Soat.HappyNet.Silverlight.Infrastructure
{
    /// <summary>
    /// Represents the global state of the application
    /// We use a static class so it can be accessed from any page (converters, *.xaml.cs, etc.)
    /// </summary>
    public static class Globals
    {
        /// <summary>
        /// Current username
        /// </summary>
        public static string UserName = "";
        /// <summary>
        /// Current password
        /// </summary>
        public static string Password = "";
        /// <summary>
        /// User full name
        /// </summary>
        public static string FullName = "";

        /// <summary>
        /// Logged in status of the user
        /// </summary>
        public static bool IsLoggedIn = false;

        /// <summary>
        /// User id
        /// </summary>
        public static int UserID = -1;

        /// <summary>
        /// Current navigation uri
        /// </summary>
        public static Uri CurrentUri;

        /// <summary>
        /// Current query string from the last navigation
        /// </summary>
        public static IDictionary<string, string> QueryString { get; set; }

        /// <summary>
        /// Unity container, global to the application
        /// </summary>
        public static IUnityContainer Container { get; set; }

        /// <summary>
        /// Event aggregator, global to the application
        /// </summary>
        public static IEventAggregator EventAggregator { get; set; }

        /// <summary>
        /// Region manager, global to the application
        /// </summary>
        public static IRegionManager RegionManager { get; set; }

        /// <summary>
        /// Boolean to state wether the next navigation is allowed or not 
        /// (used for ChangeFragmentWithoutNavigating)
        /// </summary>
        public static bool IsNavigationAllowed { get; set; }

        /// <summary>
        /// Changed the current navigation anchor without triggering the navigation to the page
        /// </summary>
        /// <param name="fragment"></param>
        public static void ChangeFragmentWithoutNavigating(string fragment)
        {
            IsNavigationAllowed = false;
            Application.Current.Host.NavigationState = fragment;
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        static Globals()
        {
            IsNavigationAllowed = true;
        }
    }
}
