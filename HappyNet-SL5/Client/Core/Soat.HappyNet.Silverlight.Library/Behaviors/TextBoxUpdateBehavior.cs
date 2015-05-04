// -----------------------------------------------------------------------
// <copyright file="TextBoxUpdateBehavior.cs" company="So@t">
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
using System.Windows.Interactivity;

namespace Soat.HappyNet.Silverlight.Library.Behaviors
{
    public class TextBoxUpdateBehavior : Behavior<TextBox>
    {
        public TextBoxUpdateBehavior()
        {
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.TextChanged += AssociatedObjectOnTextChanged;
        }

        private void AssociatedObjectOnTextChanged(object sender, TextChangedEventArgs args)
        {
            var bindingExpr = AssociatedObject.GetBindingExpression(TextBox.TextProperty);
            bindingExpr.UpdateSource();

        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.TextChanged -= AssociatedObjectOnTextChanged;
        }
    }
}
