// -----------------------------------------------------------------------
// <copyright file="ClearChildViewsRegionBehavior.cs" company="So@t">
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
using Microsoft.Practices.Composite.Presentation.Regions;

namespace Soat.HappyNet.Silverlight.Library.Components.Components.Regions
{
    public class ClearChildViewsRegionBehavior : Microsoft.Practices.Composite.Presentation.Regions.RegionBehavior
    {
        public const string BehaviorKey = "ClearChildViews";

        protected override void OnAttach()
        {
            this.Region.PropertyChanged +=
                new System.ComponentModel.PropertyChangedEventHandler(Region_PropertyChanged);
        }

        void Region_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "RegionManager")
            {
                if (this.Region.RegionManager == null)
                {
                    foreach (object view in this.Region.Views)
                    {
                        DependencyObject dependencyObject = view as DependencyObject;
                        if (dependencyObject != null)
                        {
                            dependencyObject.ClearValue(RegionManager.RegionManagerProperty);
                        }
                    }
                }
            }
        }
    }
}
