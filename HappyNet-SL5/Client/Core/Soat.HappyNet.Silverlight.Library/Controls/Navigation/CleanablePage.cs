// -----------------------------------------------------------------------
// <copyright file="CleanablePage.cs" company="So@t">
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
using System.Collections.Generic;
using Microsoft.Practices.Composite.Regions;

namespace Soat.HappyNet.Silverlight.Library.Controls
{
    public class CleanablePage : Page, IDisposable, ICleanable
    {
        private List<IDisposable> ToClean = new List<IDisposable>();
        private List<string> RegionsToClean = new List<string>();

        protected IRegionManager RegionManager { get; set; }

        protected void AddRegion(string regionName)
        {
            if (!RegionsToClean.Contains(regionName))
                RegionsToClean.Add(regionName);
        }

        protected void RemoveRegion(string regionName)
        {
            if (RegionsToClean.Contains(regionName))
                RegionsToClean.Remove(regionName);
        }

        protected void AddToCleaning(IDisposable obj)
        {
            if (!ToClean.Contains(obj))
                ToClean.Add(obj);
        }

        protected void RemoveFromCleaning(IDisposable obj)
        {
            if (ToClean.Contains(obj))
                ToClean.Remove(obj);
        }

        protected override void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);

            Clean();
        }

        public virtual void Clean()
        {
            foreach (var dispose in ToClean)
            {
                dispose.Dispose();
            }

            ToClean.Clear();

            foreach (var regionName in RegionsToClean)
            {
                if (RegionManager.Regions.ContainsRegionWithName(regionName))
                {
                    RegionManager.Regions.Remove(regionName);
                }
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            this.Clean();
        }

        #endregion
    }
}
