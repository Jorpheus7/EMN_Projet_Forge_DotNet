// -----------------------------------------------------------------------
// <copyright file="HoverBehavior.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

// http://nroute.codeplex.com/

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
using System.Windows.Interactivity;
using Soat.HappyNet.Silverlight.Library.Controls;
using Soat.HappyNet.Silverlight.Library.Behaviors.Actions;

namespace Soat.HappyNet.Silverlight.Library.Behaviors
{
    public class HoverBehavior : Behavior<FrameworkElement>
    {
        #region HoverVisualStateName (DependencyProperty)

        /// <summary>
        /// A description of the property.
        /// </summary>
        public string HoverVisualStateName
        {
            get { return (string)GetValue(HoverVisualStateNameProperty); }
            set { SetValue(HoverVisualStateNameProperty, value); }
        }
        public static readonly DependencyProperty HoverVisualStateNameProperty =
            DependencyProperty.Register("HoverVisualStateName", typeof(string), typeof(HoverBehavior),
              new PropertyMetadata(string.Empty));

        #endregion

        #region NormalVisualStateName (DependencyProperty)

        /// <summary>
        /// A description of the property.
        /// </summary>
        public string NormalVisualStateName
        {
            get { return (string)GetValue(NormalVisualStateNameProperty); }
            set { SetValue(NormalVisualStateNameProperty, value); }
        }
        public static readonly DependencyProperty NormalVisualStateNameProperty =
            DependencyProperty.Register("NormalVisualStateName", typeof(string), typeof(HoverBehavior),
              new PropertyMetadata(VisualStateHelper.StateNormal));

        #endregion

        #region HoverStoryboard (DependencyProperty)

        /// <summary>
        /// A description of the property.
        /// </summary>
        public Storyboard HoverStoryboard
        {
            get { return (Storyboard)GetValue(HoverStoryboardProperty); }
            set { SetValue(HoverStoryboardProperty, value); }
        }
        public static readonly DependencyProperty HoverStoryboardProperty =
            DependencyProperty.Register("HoverStoryboard", typeof(Storyboard), typeof(HoverBehavior), null);

        #endregion

        #region NormalStoryboard (DependencyProperty)

        /// <summary>
        /// A description of the property.
        /// </summary>
        public Storyboard NormalStoryboard
        {
            get { return (Storyboard)GetValue(NormalStoryboardProperty); }
            set { SetValue(NormalStoryboardProperty, value); }
        }
        public static readonly DependencyProperty NormalStoryboardProperty =
            DependencyProperty.Register("NormalStoryboard", typeof(Storyboard), typeof(HoverBehavior), null);

        #endregion

        #region Target (DependencyProperty)

        /// <summary>
        /// A description of the property.
        /// </summary>
        FrameworkElement Target
        {
            get;
            set;
        }

        #endregion

        public HoverBehavior()
        {
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.MouseEnter += new MouseEventHandler(AssociatedObject_MouseEnter);
            AssociatedObject.MouseLeave += new MouseEventHandler(AssociatedObject_MouseLeave);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.MouseEnter -= new MouseEventHandler(AssociatedObject_MouseEnter);
            AssociatedObject.MouseLeave -= new MouseEventHandler(AssociatedObject_MouseLeave);
        }

        void AssociatedObject_MouseEnter(object sender, MouseEventArgs e)
        {
            FrameworkElement element = AssociatedObject;
            if (Target != null)
                element = Target;
            else
                Target = GoToStateBase.FindTargetElement(element);

            if (Target != null
                && !string.IsNullOrEmpty(HoverVisualStateName))
            {
                GoToStateBase.GoToState(Target, HoverVisualStateName, true);
            }

            if (NormalStoryboard != null)
                NormalStoryboard.Stop();

            if (HoverStoryboard != null)
                HoverStoryboard.Begin();
        }

        void AssociatedObject_MouseLeave(object sender, MouseEventArgs e)
        {
            FrameworkElement element = AssociatedObject;
            if (Target != null)
                element = Target;
            else
                Target = GoToStateBase.FindTargetElement(element);
            if (Target != null
                && !string.IsNullOrEmpty(NormalVisualStateName))
            {
                GoToStateBase.GoToState(Target, NormalVisualStateName, true);
            }

            if (HoverStoryboard != null)
                HoverStoryboard.Stop();

            if (NormalStoryboard != null)
                NormalStoryboard.Begin();
        }
    }
}
