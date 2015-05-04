// -----------------------------------------------------------------------
// <copyright file="EmployeeDetailsPage.xaml.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;
using Soat.HappyNet.Silverlight.Infrastructure.Localization;
using System.ComponentModel;
using Soat.HappyNet.Silverlight.Library.Controls;
using Soat.HappyNet.Silverlight.Infrastructure;
using Soat.HappyNet.Silverlight.Infrastructure.Components;

namespace Soat.HappyNet.Silverlight.Modules.HumanResources.ViewPages
{
    /// <summary>
    /// EmployeeDetails Page
    /// </summary>
    public partial class EmployeeDetailsPage : LocalizedPage
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmployeeDetailsPage()
            : base()
        {
            InitializeComponent();
        }

        #endregion

        /// <summary>
        /// Sets the current title of the page
        /// </summary>
        protected override void SetTitle()
        {
            this.Title = StringLibrary.PAGE_EmployeeDetails;
        }
    }
}
