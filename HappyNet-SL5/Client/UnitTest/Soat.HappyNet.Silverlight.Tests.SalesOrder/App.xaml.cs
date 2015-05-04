// -----------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="So@t">
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
using Microsoft.Silverlight.Testing;

namespace Soat.HappyNet.Silverlight.Tests.SalesOrder
{
    public partial class App : Application
    {
        public App()
        {
            this.Startup += this.Application_Startup;
            InitializeComponent();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            UnitTestSettings settings = UnitTestSystem.CreateDefaultSettings();
            this.RootVisual = UnitTestSystem.CreateTestPage();
        }
    }
}