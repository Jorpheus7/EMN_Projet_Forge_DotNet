// -----------------------------------------------------------------------
// <copyright file="ProductModelExtension.cs" company="So@t">
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
    public partial class ProductModel
    {
        public ProductModel()
        {
        }

        private ProductDescription description;
        [DataMember]
        public ProductDescription ProductDescription
        {
            get { return description; }
            set
            {
                if (description != value)
                {
                    description = value;
                    OnPropertyChanged("ProductDescription");
                }
            }
        }

        private decimal lowerPrice;
        [DataMember]
        public decimal LowerPrice
        {
            get { return lowerPrice; }
            set
            {
                if (lowerPrice != value)
                {
                    lowerPrice = value;
                    OnPropertyChanged("LowerPrice");
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
