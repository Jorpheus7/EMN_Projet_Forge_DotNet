// -----------------------------------------------------------------------
// <copyright file="VisualStateHelper.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

///////////////////////////////////////////////////////////////////////////////
//
//  VisualStateHelper.cs
//
// This file is licensed with the Microsoft Public License (Ms-PL), for details look here: 
// http://www.opensource.org/licenses/ms-pl.html
//
///////////////////////////////////////////////////////////////////////////////

namespace Soat.HappyNet.Silverlight.Library.Controls
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    internal static class VisualStateHelper
    {
        public const string GroupCommon = "CommonStates";
        public const string GroupFocus = "FocusStates";
        public const string GroupSelection = "SelectionStates";
        public const string GroupWatermark = "WatermarkStates";
        public const string StateDisabled = "Disabled";
        public const string StateFocused = "Focused";
        public const string StateMouseOver = "MouseOver";
        public const string StateNormal = "Normal";
        public const string StateUnfocused = "Unfocused";
        public const string StateUnwatermarked = "Unwatermarked";
        public const string StateWatermarked = "Watermarked";

        public static void GoToState(Control control, bool useTransitions, params string[] stateNames)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }
            if (stateNames != null)
            {
                foreach (string str in stateNames)
                {
                    if (VisualStateManager.GoToState(control, str, useTransitions))
                    {
                        return;
                    }
                }
            }
        }
    }
}