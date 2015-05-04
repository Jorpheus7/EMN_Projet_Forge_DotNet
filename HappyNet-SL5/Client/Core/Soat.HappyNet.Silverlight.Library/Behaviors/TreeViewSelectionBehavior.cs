// -----------------------------------------------------------------------
// <copyright file="TreeViewSelectionBehavior.cs" company="So@t">
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
using System.Windows.Data;
using Microsoft.Practices.Composite.Presentation.Commands;
using Soat.HappyNet.Silverlight.Library.Behaviors.Interactivity;

namespace Soat.HappyNet.Silverlight.Library.Behaviors
{
    public class TreeViewSelectionBehavior : BindableBehavior<TreeView>
    {
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(TreeViewSelectionBehavior),
            new PropertyMetadata(null, TreeViewSelectionBehavior.HandleCommandChanged));

        #region Properties

        public ICommand CommandValue
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public Binding Command
        {
            get { return GetBinding(CommandProperty); }
            set { SetBinding<ICommand>(CommandProperty, value); }
        }

        private static void HandleCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        #endregion

        public TreeViewSelectionBehavior()
        {
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.SelectedItemChanged += new RoutedPropertyChangedEventHandler<object>(OnSelectedItemChanged);
        }

        void OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (CommandValue != null && CommandValue.CanExecute(e.NewValue))
                CommandValue.Execute(e.NewValue);
        }

        private void AssociatedObjectOnTextChanged(object sender, TextChangedEventArgs args)
        {
            var bindingExpr = AssociatedObject.GetBindingExpression(TextBox.TextProperty);
            bindingExpr.UpdateSource();
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.SelectedItemChanged -= new RoutedPropertyChangedEventHandler<object>(OnSelectedItemChanged);
            Command = null;
            CommandValue = null;
        }
    }
}