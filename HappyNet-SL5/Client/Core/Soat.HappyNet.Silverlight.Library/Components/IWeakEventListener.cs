// -----------------------------------------------------------------------
// <copyright file="IWeakEventListener.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

// http://nroute.codeplex.com/

using System;

namespace Soat.HappyNet.Silverlight.Library.Components
{

#if SILVERLIGHT

    public interface IWeakEventListener
    {
        /// <remarks> The managerType returns the event (handler) type to identify the event.
        /// and returing false would remove the listener.
        bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e);
    }

#endif

    

}
