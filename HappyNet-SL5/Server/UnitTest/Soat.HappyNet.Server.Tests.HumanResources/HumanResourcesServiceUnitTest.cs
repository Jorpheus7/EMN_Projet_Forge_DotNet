// -----------------------------------------------------------------------
// <copyright file="HumanResourcesServiceUnitTest.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Soat.HappyNet.Server.Services.HumanResources;
using Soat.HappyNet.Server.DataContract;

namespace Soat.HappyNet.Server.Tests.HumanResources
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class HumanResourcesServiceUnitTest
    {
        [TestMethod]
        public void FindAllEmployeesTestMethod()
        {
            IHumanResourcesService service = new HumanResourcesService();


			var contacts = service.FindAllEmployees(0, 50, "BusinessEntityId", SortDirection.Ascending);
            Assert.IsTrue(contacts.Count() > 0);
        }
    }
}
