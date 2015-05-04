// -----------------------------------------------------------------------
// <copyright file="IOrderDetailsViewModel.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using Soat.HappyNet.Silverlight.Modules.BackOffice.OrdersServiceReference;

namespace Soat.HappyNet.Silverlight.Modules.BackOffice.Views
{
    public interface IOrderDetailsViewModel
    {
        IOrderDetailsView View { get; }
        SalesOrderHeader CurrentOrder { get; set; }
    }
}
