// -----------------------------------------------------------------------
// <copyright file="TrackEventAction.cs" company="So@t">
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
using System.Windows.Interactivity;
using System.Windows.Browser;
using Soat.HappyNet.Silverlight.Library.Behaviors.Interactivity;
using System.Windows.Data;

namespace Soat.HappyNet.Silverlight.Library.Behaviors.Actions
{
    /// <summary>
    /// Tracking d'événement via Google Analytics
    /// Attention, requiert le code javascript suivant sur la page pour fonctionner 
    /// <code>
    /// var gaJsHost = (("https:" == document.location.protocol) ? "https://ssl." : "http://www.");
    ///document.write(unescape("%3Cscript src='" + gaJsHost + "google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E"));
    ///
    ///try {
    ///    var pageTracker = _gat._getTracker("UA-12732583-1");
    ///    pageTracker._trackPageview();
    ///} catch (err) { }
    ///
    ///function trackEvent(category, action, label) {
    ///    pageTracker._trackEvent(category, action, label);
    ///}</code>
    /// Exemple d'utilisation :
    /// <code>
    /// <Button>
    ///     <Interactivity:Interaction.Triggers>
    ///         <Interactivity:EventTrigger EventName="Click">
    ///             <GoogleAnalytics:TrackEventAction Category="MeXperience.CloudFilter" Action="{Binding Name, StringFormat='Filter.Remove[\{0\}]'}" Label="{Binding Name, StringFormat='Remove filter for \{0\}'}" />
    ///         </Interactivity:EventTrigger>
    ///     </Interactivity:Interaction.Triggers>
    /// </Button>
    /// </code>
    /// </summary>
    public class TrackEventAction : TriggerAction<FrameworkElement>
    {
        public static readonly DependencyProperty CategoryProperty =
        DependencyProperty.Register("Category", typeof(string),
        typeof(TrackEventAction),
        new PropertyMetadata("Silverlight.Event"));

        public static readonly DependencyProperty ActionProperty =
        DependencyProperty.Register("Action", typeof(string),
        typeof(TrackEventAction),
        new PropertyMetadata("Unknown Action"));

        public static readonly DependencyProperty LabelProperty =
        DependencyProperty.Register("Label", typeof(string),
        typeof(TrackEventAction),
        new PropertyMetadata("Unknown Action fired"));

        public string Category
        {
            get { return (string)GetValue(CategoryProperty); }
            set { SetValue(CategoryProperty, value); }
        }

        public string Action
        {
            get { return (string)GetValue(ActionProperty); }
            set { SetValue(ActionProperty, value); }
        }

        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        protected override void Invoke(object parameter)
        {
            try
            {
                HtmlPage.Window.Invoke("trackEvent", new object[] { Category, Action, Label });
            }
            catch
            {
            }
        }
    }
}
