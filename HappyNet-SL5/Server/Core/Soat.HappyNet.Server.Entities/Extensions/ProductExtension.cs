// -----------------------------------------------------------------------
// <copyright file="ProductExtension.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Soat.HappyNet.Server.Entities
{
    public partial class Product
    {
        private decimal price;
        [DataMember]
        public decimal Price
        {
            get { return price; }
            set
            {
                if (price != value)
                {
                    price = value;
                    OnPropertyChanged("Price");
                }
            }
        }

        private string currency;
        [DataMember]
        public string Currency
        {
            get { return currency; }
            set
            {
                if (currency != value)
                {
                    currency = value;
                    OnPropertyChanged("Currency");
                }
            }
        }
    }
}
