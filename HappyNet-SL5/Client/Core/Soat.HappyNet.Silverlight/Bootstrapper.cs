// -----------------------------------------------------------------------
// <copyright file="Bootstrapper.cs" company="So@t">
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
using Microsoft.Practices.Composite.Modularity;
using Soat.HappyNet.Silverlight.Library;
using Soat.HappyNet.Silverlight.Library.Components.Components.Regions;
using Microsoft.Practices.Composite.Presentation.Regions;
using Soat.HappyNet.Silverlight.Infrastructure;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Regions;
using Soat.HappyNet.Silverlight.Views;
using Microsoft.Practices.Composite.UnityExtensions;
using Microsoft.Practices.Unity;

namespace Soat.HappyNet.Silverlight
{
    /// <summary>
    /// Represents the Bootstrapper which instanciate the shell, loads
    /// the module catalog and initializes the configuration
    /// </summary>
    public class Bootstrapper : UnityBootstrapper
    {
        #region CreateShell Method

        /// <summary>
        /// Creates the main Shell
        /// </summary>
        /// <returns>Shell objet</returns>
        protected override DependencyObject CreateShell()
        {
            Shell shell = Container.Resolve<Shell>();
            
            if (Application.Current is INavigationApplication)
            {
                ((INavigationApplication)Application.Current).MainFrame = shell.MainFrame;
            }

            Application.Current.RootVisual = shell;
            
            return shell;
        }

        #endregion

        #region GetModuleCatalog Method

        /// <summary>
        /// Creates the module catalog
        /// </summary>
        /// <returns>Modules catalog</returns>
        protected override IModuleCatalog GetModuleCatalog()
        {
            ModuleCatalog m = new ModuleCatalog();

            m.AddModule(typeof(Soat.HappyNet.Silverlight.Modules.Main.MainModule));
            m.AddModule(typeof(Soat.HappyNet.Silverlight.Modules.HumanResources.HumanResourcesModule));
            m.AddModule(typeof(Soat.HappyNet.Silverlight.Modules.SalesOrder.SalesOrderModule));
            m.AddModule(typeof(Soat.HappyNet.Silverlight.Modules.Production.ProductionModule));
            m.AddModule(typeof(Soat.HappyNet.Silverlight.Modules.Logging.LoggingModule));
            m.AddModule(typeof(Soat.HappyNet.Silverlight.Modules.BackOffice.BackOfficeModule));

            return m;
        }

        #endregion

        #region RegionBehaviors

        /// <summary>
        /// Adds a behavior to the region to handle a bug from the region manager
        /// </summary>
        /// <returns></returns>
        protected override IRegionBehaviorFactory ConfigureDefaultRegionBehaviors()
        {
            var regionBehaviorTypesDictionary = base.ConfigureDefaultRegionBehaviors();
            regionBehaviorTypesDictionary.AddIfMissing
                (ClearChildViewsRegionBehavior.BehaviorKey, typeof(ClearChildViewsRegionBehavior));
            return regionBehaviorTypesDictionary;
        }

        #endregion

        /// <summary>
        /// Configures some global types and initializations
        /// </summary>
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Globals.Container = this.Container;
            Globals.EventAggregator = this.Container.Resolve<IEventAggregator>();
            Globals.RegionManager = this.Container.Resolve<IRegionManager>();

            this.Container.RegisterType<IErrorChildWindowView, ErrorChildWindowView>();
            this.Container.RegisterType<IErrorChildWindowViewModel, ErrorChildWindowViewModel>();

            this.Container.RegisterType<IMessageChildWindowView, MessageChildWindowView>();
            this.Container.RegisterType<IMessageChildWindowViewModel, MessageChildWindowViewModel>();

            this.Container.RegisterType<IConfirmChildWindowView, ConfirmChildWindowView>();
            this.Container.RegisterType<IConfirmChildWindowViewModel, ConfirmChildWindowViewModel>();
        }
    }
}
