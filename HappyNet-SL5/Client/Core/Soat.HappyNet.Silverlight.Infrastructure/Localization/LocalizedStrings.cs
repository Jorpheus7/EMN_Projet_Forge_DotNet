// -----------------------------------------------------------------------
// <copyright file="LocalizedStrings.cs" company="So@t">
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

namespace Soat.HappyNet.Silverlight.Infrastructure.Localization
{
    /// <summary>
    /// Class which encapsulates a resource file, so it can be binded
    /// and updated whenever we fire the PropertyChanged event
    /// </summary>
    public class LocalizedStrings : INotifyPropertyChanged
    {
        /// <summary>
        /// Default constructor used when instanciated in XAML
        /// </summary>
        public LocalizedStrings()
        {
        }

        private static StringLibrary strings = new StringLibrary();
        /// <summary>
        /// Resource file containing our localization
        /// </summary>
        public StringLibrary Strings
        {
            get
            {
                return strings;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event
        /// </summary>
        public void FirePropertyChanged()
        {
            if (PropertyChanged != null)
            {
                var args = new PropertyChangedEventArgs(null);
                PropertyChanged(this, args);
            }
        }
    }
}
