// -----------------------------------------------------------------------
// <copyright file="DoubleClickManager.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using System.Windows.Controls.Primitives;

namespace Soat.HappyNet.Silverlight.Library.Components.Mouse
{
    /// <summary>
    /// Gestion du double clic
    /// </summary>
    public class DoubleClickManager
    {
        /// <summary>
        /// Evénement double clic
        /// </summary>
        public event EventHandler DoubleClick;

        private DispatcherTimer _timer;
 
        /// <summary>
        /// Contrôle destinataire du double clic
        /// </summary>
        public UIElement Control { get; set; }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="control">Contrôle à gérer</param>
        public DoubleClickManager(UIElement control)
            : this(control, TimeSpan.FromMilliseconds(300))
        {
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="control">Contrôle à gérer</param>
        /// <param name="timeout">Timer du double clic</param>
        public DoubleClickManager(UIElement control, TimeSpan timeout)
        {
            this.Control = control;

            _timer = new DispatcherTimer();
            _timer.Interval = timeout;
            _timer.Tick += new EventHandler(_timer_Tick);

            if (control is ButtonBase)
            {
                ((ButtonBase)control).Click += new RoutedEventHandler(DoubleClickManager_Click);
            }
            else
            {
                control.MouseLeftButtonDown += new MouseButtonEventHandler(control_MouseLeftButtonDown);
            }
        }

        void DoubleClickManager_Click(object sender, RoutedEventArgs e)
        {
            HandleClick(sender);
        }

        void control_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            HandleClick(sender);
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            _timer.Stop();
        }

        /// <summary>
        /// Gère le clic
        /// </summary>
        public void HandleClick(object sender)
        {
            // Si le second clic intervient avant la fin du timer, c'est un double clic
            if (_timer.IsEnabled)
            {
                _timer.Stop();

                OnDoubleClick(sender);
            }
            // Sinon c'est le premier clic : on lance le timer
            else
            {
                _timer.Start();
            }
        }

        /// <summary>
        /// Appelé lors du double clic
        /// </summary>
        private void OnDoubleClick(object sender)
        {
            if (DoubleClick != null)
                DoubleClick(sender, EventArgs.Empty);
        }
    }
}
