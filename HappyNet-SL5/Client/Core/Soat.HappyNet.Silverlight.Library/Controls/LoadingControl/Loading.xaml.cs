// -----------------------------------------------------------------------
// <copyright file="Loading.xaml.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Soat.HappyNet.Silverlight.Library.Controls
{
    public partial class Loading : UserControl
    {
        static object padlock = new object();
        int nb;
        bool isRunning = false;

        #region IsLoading (DependencyProperty)

        /// <summary>
        /// A description of the property.
        /// </summary>
        public bool IsLoading
        {
            get { return (bool)GetValue(IsLoadingProperty); }
            set { SetValue(IsLoadingProperty, value); }
        }
        public static readonly DependencyProperty IsLoadingProperty =
            DependencyProperty.Register("IsLoading", typeof(bool), typeof(Loading),
            new PropertyMetadata(new PropertyChangedCallback(OnIsLoadingChanged)));

        private static void OnIsLoadingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Loading)d).OnIsLoadingChanged(e);
        }

        protected virtual void OnIsLoadingChanged(DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                Start();
            }
            else
            {
                Stop();
            }
        }
        #endregion

        Storyboard StartLoad;
        Storyboard LoadCycle;
        Storyboard StopLoad;

        public Loading()
        {
            InitializeComponent();

            this.StartLoad = this.Resources["StartLoad"] as Storyboard;
            this.LoadCycle = this.Resources["LoadCycle"] as Storyboard;
            this.StopLoad = this.Resources["StopLoad"] as Storyboard;
        }

        private void Start()
        {
            lock (padlock)
            {
                nb++;

                if (!isRunning)
                {
                    isRunning = true;
                    //VisualStateManager.GoToState(this, "Load", true);
                    StartLoad.Begin();
                    LoadCycle.Begin();
                }
            }
        }

        private void Stop()
        {
            lock (padlock)
            {
                nb--;

                if (nb <= 0)
                {
                    isRunning = false;
                    //VisualStateManager.GoToState(this, "Normal", true);
                    StopLoad.Begin();
                    nb = 0;
                }
            }
        }
    }
}
