// -----------------------------------------------------------------------
// <copyright file="VisualStateExtensions.cs" company="So@t">
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

namespace Soat.HappyNet.Silverlight.Library.Extensions
{
    public static class VisualStateExtensions
    {
        public static void Activate(this VisualState state, Control control, bool useTransitions)
        {
            VisualStateManager.GoToState(control, state.Name, useTransitions);
        }

        public static void Activate(this VisualState state, Control control)
        {
            state.Activate(control, true);
        }
    }
}
