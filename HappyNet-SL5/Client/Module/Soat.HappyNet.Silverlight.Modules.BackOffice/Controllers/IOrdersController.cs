// -----------------------------------------------------------------------
// <copyright file="IOrdersController.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using Soat.HappyNet.Silverlight.Modules.BackOffice.OrdersServiceReference;

namespace Soat.HappyNet.Silverlight.Modules.BackOffice.Controllers
{
    public interface IOrdersController
    {
        void SelectedOrderChanged(SalesOrderHeader salesOrderHeader);

        void Run();
    }
}
