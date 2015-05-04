// -----------------------------------------------------------------------
// <copyright file="EventOfT.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

// http://nroute.codeplex.com/

using System;

namespace Soat.HappyNet.Silverlight.Library.Components
{
    public class Event<T>
        where T : EventArgs
    {
        readonly Object _sender;
        readonly T _eventArgs;

        public Event(Object sender, T eventArgs)
        {
            _sender = sender;
            _eventArgs = eventArgs;
        }

        public Object Sender
        {
            get { return _sender; }
        }

        public T EventArgs
        {
            get { return _eventArgs; }
        }
    }
}
