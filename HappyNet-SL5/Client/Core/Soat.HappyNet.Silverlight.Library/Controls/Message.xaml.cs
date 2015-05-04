// -----------------------------------------------------------------------
// <copyright file="Message.xaml.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

//-------------------------------------------------------------------------
// Basé sur le TransientMessage.cs d'Infragistics - FaceOut
//-------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Soat.HappyNet.Silverlight.Library.Controls
{
    /// <summary>
    /// Message d'information
    /// </summary>
    public partial class Message : UserControl
    {
        Storyboard ShowTimeline;
        Storyboard HideTimeline;

        DispatcherTimer timer;

        #region Constructeurs
        /// <summary>
        /// Ce contrôle est utilisé pour afficher un message
        /// </summary>
        public Message()
        {
            InitializeComponent();

            this.ShowTimeline = this.Resources["ShowTimeline"] as Storyboard;
            this.HideTimeline = this.Resources["HideTimeline"] as Storyboard;
            this.HideTimeline.Completed += new EventHandler(HideTimeline_Completed);

            // On cache le message à son initialisation
            this.LayoutRoot.Visibility = Visibility.Collapsed;

            // Valeurs par défaut
            this.ShowDuration = 5d; // 5 secondes

            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);

            InitTimer();
        }

        void InitTimer()
        {
            if (timer != null)
            {
                timer.Stop();
                timer.Interval = TimeSpan.FromSeconds(ShowDuration);
                timer.Start();
            }
        }

        // Initialisation des propriétés XAML
        static Message()
        {
            ShowDurationProperty = DependencyProperty.Register("ShowDurationProperty", typeof(double), typeof(Message), null);
        }
        #endregion Constructeurs

        #region Membres

        /// <summary>
        /// <see cref="ShowDuration"/> backing dependency property.
        /// </summary>
        public static readonly DependencyProperty ShowDurationProperty;
        /// <summary>
        /// Durée d'affichage du message.
        /// </summary>
        public double ShowDuration
        {
            get
            {
                return (double)this.GetValue(ShowDurationProperty);
            }
            set
            {
                this.SetValue(ShowDurationProperty, value);
                InitTimer();
            }
        }

        /// <summary>
        /// Couleur du texte.
        /// </summary>
        public Brush ForeColor
        {
            get
            {
                return this.MessageBlock.Foreground;
            }
            set
            {
                this.MessageBlock.Foreground = value;
            }
        }

        public Brush BackgroundColor
        {
            get
            {
                return this.Container.Background;
            }
            set
            {
                this.Container.Background = value;
            }
        }

        /// <summary>
        /// Message à afficher.
        /// </summary>
        public string Text
        {
            get
            {
                return this.MessageBlock.Text;
            }
            set
            {
                this.MessageBlock.Text = value;
            }
        }

        #endregion Membres

        #region Méthodes

        public void Show()
        {
            this.LayoutRoot.Visibility = Visibility.Visible;
            this.ShowTimeline.Begin();

            this.MouseLeftButtonDown += new MouseButtonEventHandler(LayoutRoot_MouseLeftButtonDown);
        }

        void LayoutRoot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Hide();
            this.MouseLeftButtonDown -= new MouseButtonEventHandler(LayoutRoot_MouseLeftButtonDown);
        }

        /// <summary>
        /// Cache le message sans attendre qu'il le fasse de lui-même.
        /// </summary>
        public void Hide()
        {
            this.HideTimeline.Begin();
        }

        #endregion Méthodes

        #region Evénements

        void HideTimeline_Completed(object sender, EventArgs e)
        {
            // Lorsque l'affichage est terminé, on supprime le message
            if (Parent is Panel)
            {
                Panel parent = (Panel)Parent;
                parent.Children.Remove(this);
            }
        }

        #endregion Evénements

        void timer_Tick(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
