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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Soat.HappyNet.Silverlight.Tests.Production.Default1.Interfaces;
using Soat.HappyNet.Silverlight.Tests.Production.Default1.Views;
using Soat.HappyNet.Silverlight.Tests.Production.Default1.ViewModels;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Composite.Presentation.Regions;

namespace Soat.HappyNet.Silverlight.Tests.Production
{
    [TestClass]
    public class TestClassDefault1 : SilverlightTest
    {

        #region Container Property

        /// <summary>
        /// Membre privée Container
        /// </summary>
        private IUnityContainer container;

        /// <summary>
        /// Propriété publique Container
        /// </summary>
        public IUnityContainer Container
        {
            get
            {
                if (container == null)
                {
                    this.container = new UnityContainer();
                    this.container.RegisterType<IEventAggregator, EventAggregator>();
                    this.container.RegisterType<IRegionManager, RegionManager>();
                }
                return container;
            }
        }

        #endregion

        [TestInitialize]
        public void Initialize()
        {
            this.Container.RegisterType<IDefault1View, Default1View>();
            this.Container.RegisterType<IDefault1ViewModel, Default1ViewModel>();
        }

        [TestMethod]
        [Asynchronous]
        public void TestMethod1()
        {
            this.TestPanel.Children.Add(this.Container.Resolve<IDefault1ViewModel>().View as UIElement);

            //Assert.Inconclusive();

            EnqueueTestComplete();
        }


        [TestMethod]
        [Asynchronous]
        public void TestMethod2()
        {
            this.TestPanel.Children.Add(this.Container.Resolve<IDefault1ViewModel>().View as UIElement);


            //Assert.Inconclusive();
        }
    }
}