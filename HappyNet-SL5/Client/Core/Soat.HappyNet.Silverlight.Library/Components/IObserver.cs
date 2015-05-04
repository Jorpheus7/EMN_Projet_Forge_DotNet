﻿// -----------------------------------------------------------------------
// <copyright file="IObserver.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

// http://nroute.codeplex.com/

using System;

namespace Soat.HappyNet.Silverlight.Library.Components
{
    public interface IObserver<T>
    {
        void OnCompleted();

        void OnError(Exception exception);

        void OnNext(T value);
    }
}
