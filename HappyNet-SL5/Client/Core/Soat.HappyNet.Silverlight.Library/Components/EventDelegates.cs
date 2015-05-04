// -----------------------------------------------------------------------
// <copyright file="EventDelegates.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

// http://nroute.codeplex.com/

using System;

namespace Soat.HappyNet.Silverlight.Library.Components
{

    public delegate void EventDelegate<E>(Object sender, E args)
        where E : EventArgs;

    public delegate void EventDelegate<T, E>(T @this, Object sender, E args)
        where E : EventArgs;

}
