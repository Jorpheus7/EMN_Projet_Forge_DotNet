// -----------------------------------------------------------------------
// <copyright file="ILanguageSwitchViewModel.cs" company="So@t">
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
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Presentation.Commands;
using Soat.HappyNet.Silverlight.Infrastructure.Models;
using System.Collections.ObjectModel;

namespace Soat.HappyNet.Silverlight.Modules.Main.Views
{
    /// <summary>
    /// Interface defining the ViewModel for the LanguageSwitch View
    /// </summary>
    public interface ILanguageSwitchViewModel
    {
        /// <summary>
        /// View
        /// </summary>
        ILanguageSwitchView View { get; }

        /// <summary>
        /// Selected language
        /// </summary>
        Language CurrentLanguage { get; }

        /// <summary>
        /// List of available languages
        /// </summary>
        ObservableCollection<Language> Languages { get; }
    }
}
