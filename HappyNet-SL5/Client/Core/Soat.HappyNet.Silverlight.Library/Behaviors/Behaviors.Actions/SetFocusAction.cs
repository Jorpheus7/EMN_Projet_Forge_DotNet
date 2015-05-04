// -----------------------------------------------------------------------
// <copyright file="SetFocusAction.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

// http://nroute.codeplex.com/

using System.Windows.Controls;
using System.Windows.Interactivity;

namespace Soat.HappyNet.Silverlight.Library.Behaviors.Actions
{
    public class SetFocusAction : TargetedTriggerAction<Control>
    {
        protected override void Invoke(object parameter)
        {
            if (Target != null) Target.Focus();
        }
    }
}
