// -----------------------------------------------------------------------
// <copyright file="DynamicPagedCollectionView.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using Soat.HappyNet.Silverlight.Library;

namespace Soat.HappyNet.Silverlight.Modules.BackOffice.Views.OrdersList
{
    public class DynamicPagedCollectionView : ViewModel, IPagedCollectionView
    {
        public DynamicPagedCollectionView(IEnumerable source)
        {
            Debug.Assert(source != null, "La source est nulle");
            this._sourceCollection = source;

            this.PageIndex = 0;
            this.PageSize = 20;
        }

        public DynamicPagedCollectionView(int totalItemCount, IEnumerable source, Func<int, int, IEnumerable> fetchDataFunction)
        {
            Debug.Assert(fetchDataFunction != null, "Le délégué est nul");
            if (totalItemCount > 0)
            {
                this._totalItemCount = totalItemCount;
            }

            if (source == null)
                throw new ArgumentNullException("source");
            else
                this._sourceCollection = source;


            //this.fetchData = new FetchDataDelegate(fetchDataFunction);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="content"></param>
        /// <param name="totalItemCount"></param>
        /// <returns></returns>
        public bool Update(int pageIndex, int pageSize, IEnumerable content, int totalItemCount)
        {
            this.TotalItemCount = totalItemCount;
            this._sourceCollection = content;

            return MoveToPage(pageIndex);
        }

        #region IPagedCollectionView Members

        #region Properties

        private bool _canChangePage;
        public bool CanChangePage
        {
            get { return true; }
        }

        private bool _isPageChanging;
        public bool IsPageChanging
        {
            get { return false; }
        }

        private int _itemCount;
        public int ItemCount
        {
            get { return this._itemCount; }
        }

        private int _pageIndex;
        public int PageIndex
        {
            get { return this._pageIndex; }
            set
            {
                if (_pageIndex != value)
                {
                    _pageIndex = value;
                }
            }
        }

        private int _pageSize;
        public int PageSize
        {
            get
            {
                return this._pageSize;
            }
            set
            {
                // TODO
                this._pageIndex = value;
            }
        }

        private int _totalItemCount;
        public int TotalItemCount
        {
            get { return this._totalItemCount; }
            set
            {
                if (_totalItemCount != value)
                {
                    _totalItemCount = value;
                }
            }
        }

        #endregion

        #region Private members

        private int PageCount
        {
            get { return (this.PageSize > 0) ? Math.Max(1, (int)Math.Ceiling((double)this.ItemCount / this.PageSize)) : 0; }
        }

        #endregion

        #region Events

        public event EventHandler<EventArgs> PageChanged;

        public event EventHandler<PageChangingEventArgs> PageChanging;

        #endregion

        public bool MoveToFirstPage()
        {
            return this.MoveToPage(0);
        }

        public bool MoveToLastPage()
        {
            if (this.TotalItemCount != -1 && this.PageSize > 0)
            {
                return this.MoveToPage(this.PageCount - 1);
            }
            else
            {
                return false;
            }
        }

        public bool MoveToNextPage()
        {
            return this.MoveToPage(this.PageIndex + 1);
        }

        public bool MoveToPreviousPage()
        {
            return this.MoveToPage(this.PageIndex - 1);
        }

        public bool MoveToPage(int pageIndex)
        {
            this._pageIndex = pageIndex;
            return true;
        }

        #endregion

        #region ICollectionView Members

        //public bool CanFilter
        //{
        //    // TODO
        //    get { return false; }
        //}

        //public bool CanGroup
        //{
        //    // TODO
        //    get { return false; }
        //}

        //public bool CanSort
        //{
        //    // TODO
        //    get { return false; }
        //}

        //public bool Contains(object item)
        //{
        //    // TODO
        //    return false;
        //}

        //public System.Globalization.CultureInfo Culture
        //{
        //    get
        //    {
        //        // TODO
        //        return null;
        //    }
        //    set
        //    {
        //        // TODO
        //        return null;
        //    }
        //}

        //public event EventHandler CurrentChanged;

        //public event CurrentChangingEventHandler CurrentChanging;

        //public object CurrentItem
        //{
        //    get { throw new NotImplementedException(); }
        //}

        //public int CurrentPosition
        //{
        //    get { throw new NotImplementedException(); }
        //}

        //public IDisposable DeferRefresh()
        //{
        //    throw new NotImplementedException();
        //}

        //public Predicate<object> Filter
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }
        //    set
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        //public System.Collections.ObjectModel.ObservableCollection<GroupDescription> GroupDescriptions
        //{
        //    get { throw new NotImplementedException(); }
        //}

        //public System.Collections.ObjectModel.ReadOnlyObservableCollection<object> Groups
        //{
        //    get { throw new NotImplementedException(); }
        //}

        //public bool IsCurrentAfterLast
        //{
        //    get { throw new NotImplementedException(); }
        //}

        //public bool IsCurrentBeforeFirst
        //{
        //    get { throw new NotImplementedException(); }
        //}

        //public bool IsEmpty
        //{
        //    get { throw new NotImplementedException(); }
        //}

        //public bool MoveCurrentTo(object item)
        //{
        //    throw new NotImplementedException();
        //}

        //public bool MoveCurrentToFirst()
        //{
        //    throw new NotImplementedException();
        //}

        //public bool MoveCurrentToLast()
        //{
        //    throw new NotImplementedException();
        //}

        //public bool MoveCurrentToNext()
        //{
        //    throw new NotImplementedException();
        //}

        //public bool MoveCurrentToPosition(int position)
        //{
        //    throw new NotImplementedException();
        //}

        //public bool MoveCurrentToPrevious()
        //{
        //    throw new NotImplementedException();
        //}

        //public void Refresh()
        //{
        //    throw new NotImplementedException();
        //}

        //public SortDescriptionCollection SortDescriptions
        //{
        //    get { throw new NotImplementedException(); }
        //}

        private IEnumerable _sourceCollection;
        public IEnumerable SourceCollection
        {
            get { return _sourceCollection; }
        }

        #endregion

        #region IEnumerable Members

        public IEnumerator GetEnumerator()
        {
            return SourceCollection.GetEnumerator();
        }

        #endregion

        #region INotifyCollectionChanged Members

        public event System.Collections.Specialized.NotifyCollectionChangedEventHandler CollectionChanged;

        #endregion
    }
}
