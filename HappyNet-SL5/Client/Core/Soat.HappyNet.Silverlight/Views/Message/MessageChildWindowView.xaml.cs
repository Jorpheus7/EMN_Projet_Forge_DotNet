﻿// -----------------------------------------------------------------------
// <copyright file="MessageChildWindowView.xaml.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Soat.HappyNet.Silverlight.Views
{
    /// <summary>
    /// MessageWindow View
    /// </summary>
    public partial class MessageChildWindowView : ChildWindow, IMessageChildWindowView
    {
        #region Model property

        /// <summary>
        /// ViewModel attached to the View
        /// </summary>
        /// 
        public IMessageChildWindowViewModel Model
        {
            get { return this.DataContext as IMessageChildWindowViewModel; }
            set { this.DataContext = value; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MessageChildWindowView()
        {
            InitializeComponent();
        }

        #endregion

    }
}
