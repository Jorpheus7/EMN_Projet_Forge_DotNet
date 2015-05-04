﻿// -----------------------------------------------------------------------
// <copyright file="IOrderDetailsView.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace Soat.HappyNet.Silverlight.Modules.BackOffice.Views
{
    public interface IOrderDetailsView
    {
        IOrderDetailsViewModel Model { get; set; }
    }
}
