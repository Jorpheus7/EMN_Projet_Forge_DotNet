// -----------------------------------------------------------------------
// <copyright file="BindingInfo.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

// http://nroute.codeplex.com/

using System;
using System.Windows;
using System.Windows.Data;

namespace Soat.HappyNet.Silverlight.Library.Components.Bindings
{
    public class BindingInfo
    {

        public DependencyProperty BindingProperty { get; set; }

        public Type BindingType { get; set; }

        public Binding Binding { get; set; }

    }
}
