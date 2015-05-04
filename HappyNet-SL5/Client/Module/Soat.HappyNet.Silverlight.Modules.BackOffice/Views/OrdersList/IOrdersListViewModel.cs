// -----------------------------------------------------------------------
// <copyright file="IOrdersListViewModel.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.ObjectModel;
using Soat.HappyNet.Server.DataContract;
using Soat.HappyNet.Silverlight.Library;
using Soat.HappyNet.Silverlight.Modules.BackOffice.OrdersServiceReference;
using System.Windows.Data;
using System.Windows.Input;
using Soat.HappyNet.Silverlight.Modules.BackOffice.Views.OrdersList;
using Soat.HappyNet.Silverlight.Library.Components.CollectionView;

namespace Soat.HappyNet.Silverlight.Modules.BackOffice.Views
{
    public interface IOrdersListViewModel
    {
        IOrdersListView View { get; }

        PagedSortableCollectionView<SalesOrderHeader> OrdersPagedCollection { get; set; }
        SalesOrderHeader SelectedOrder { get; }

        void LoadOrdersCollection();
        void GetOrdersCompleted(ServiceArgs<Orders> args);
    }
}
