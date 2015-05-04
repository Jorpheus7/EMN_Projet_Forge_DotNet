// -----------------------------------------------------------------------
// <copyright file="AnimatorMediator.cs" company="So@t">
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
using System.Globalization;

namespace Soat.HappyNet.Silverlight.Library.Controls
{
    public class AnimationMediator : FrameworkElement
    {
        /// <summary>
        /// Gets or sets a reference to the LayoutTransformer to update.
        /// </summary>
        public LayoutTransformer LayoutTransformer { get; set; }

        /// <summary>
        /// Gets or sets the name of the LayoutTransformer to update.
        /// </summary>
        /// <remarks>
        /// This property is used iff the LayoutTransformer property is null.
        /// </remarks>
        public string LayoutTransformerName
        {
            get
            {
                return _layoutTransformerName;
            }
            set
            {
                _layoutTransformerName = value;
                // Force a new name lookup
                LayoutTransformer = null;
            }
        }
        private string _layoutTransformerName;

        /// <summary>
        /// Gets or sets the value being animated.
        /// </summary>
        public double AnimationValue
        {
            get { return (double)GetValue(AnimationValueProperty); }
            set { SetValue(AnimationValueProperty, value); }
        }
        public static readonly DependencyProperty AnimationValueProperty =
            DependencyProperty.Register(
                "AnimationValue",
                typeof(double),
                typeof(AnimationMediator),
                new PropertyMetadata(AnimationValuePropertyChanged));
        private static void AnimationValuePropertyChanged(
            DependencyObject o,
            DependencyPropertyChangedEventArgs e)
        {
            ((AnimationMediator)o).AnimationValuePropertyChanged();
        }
        private void AnimationValuePropertyChanged()
        {
            if (null == LayoutTransformer)
            {
                // No LayoutTransformer set; try to find it by LayoutTransformerName
                LayoutTransformer = FindName(LayoutTransformerName) as LayoutTransformer;
                if (null == LayoutTransformer)
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture,
                        "AnimationMediator was unable to find a LayoutTransformer named \"{0}\".",
                        LayoutTransformerName));
                }
            }
            // The Transform hasn't been updated yet; schedule an update to run after it has
            Dispatcher.BeginInvoke(() => LayoutTransformer.ApplyLayoutTransform());
        }

        public AnimationMediator()
        {
            //this.Visibility = Visibility.Collapsed;
        }
    }
}
