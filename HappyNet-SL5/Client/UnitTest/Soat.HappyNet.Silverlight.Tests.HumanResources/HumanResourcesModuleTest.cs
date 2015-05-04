// -----------------------------------------------------------------------
// <copyright file="HumanResourcesModuleTest.cs" company="So@t">
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
using Microsoft.Silverlight.Testing;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.ObjectModel;
using System.Threading;
using Soat.HappyNet.Silverlight.Infrastructure;
using Soat.HappyNet.Silverlight.Modules.HumanResources.Services;

namespace Soat.HappyNet.Silverlight.Tests.HumanResources
{
    [TestClass]
    public class HumanResourcesModuleTest : SilverlightTest
    {
        #region Container Property

        /// <summary>
        /// Unity container
        /// </summary>
        private IUnityContainer container;
        /// <summary>
        /// Unity container
        /// </summary>
        public IUnityContainer Container
        {
            get
            {
                if (this.container == null)
                {
                    this.container = new UnityContainer();
                }
                return this.container;
            }
        } 

        #endregion

        #region Constructor

        /// <summary>
        /// Constructaur par défaut
        /// </summary>
        public HumanResourcesModuleTest()
        {
            this.Container.RegisterType<IHumanResourcesService, HumanResourcesService>();
        }

        [TestInitialize]
        public void PreparePage()
        {
        }      

        #endregion  

        #region PersonServiceTestMethod Method

        [TestMethod]
        public void PersonServiceTestMethod()
        {
            //Soat.HappyNet.Silverlight.Modules.HumanResources.Services.IHumanResourcesService personService = this.container.Resolve<Soat.HappyNet.Silverlight.Modules.HumanResources.Services.IHumanResourcesService>();
            //personService.AllConstactCompletedEvent += new EventHandler<CompletedEventArgs<ObservableCollection<Contact>>>(OnAllConstactCompletedEvent);
            //personService.BeginGetAllContact(0,50);
        }

        //void OnAllConstactCompletedEvent(object sender, CompletedEventArgs<ObservableCollection<Contact>> e)
        //{
        //    Assert.IsTrue(e.Argument.Count > 0);
        //    this.TestPanel.Children.Add(new TextBlock() { Text = string.Format("Count : {0}", e.Argument.Count.ToString()) });
        //} 

        #endregion


    }
}
