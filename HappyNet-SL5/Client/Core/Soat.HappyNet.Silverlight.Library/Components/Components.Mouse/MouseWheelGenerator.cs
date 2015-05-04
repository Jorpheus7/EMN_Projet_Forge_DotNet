// -----------------------------------------------------------------------
// <copyright file="MouseWheelGenerator.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

// Code ported from Javascript version at http://adomas.org/javascript-mouse-wheel/

using System.Windows;
using System.Windows.Browser;
using System.Windows.Media;

namespace Soat.HappyNet.Silverlight.Library.Components.Mouse
{
    /// <summary>
    /// Evénement du scroll de la souris
    /// </summary>
	public class MouseWheelEventArgs : BubblingEventArgs {

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="source">Source</param>
        /// <param name="delta">Déplacement de la molette de la souris</param>
		public MouseWheelEventArgs(FrameworkElement source, double delta): base(source) {
			this.Delta = delta;
		}

        /// <summary>
        /// Déplacement de la molette de la souris
        /// </summary>
		public double Delta { get; private set; }
	}

    /// <summary>
    /// Gestion de la molette souris
    /// </summary>
	public class MouseWheelGenerator {

        /// <summary>
        /// Evénement de la molette souris
        /// </summary>
		public static readonly BubblingEvent<MouseWheelEventArgs> MouseWheelEvent = new BubblingEvent<MouseWheelEventArgs>("MouseWheel", RoutingStrategy.Bubble);

        /// <summary>
        /// Instance de la classe
        /// </summary>
		private static MouseWheelGenerator helper = new MouseWheelGenerator();

        /// <summary>
        /// Default constructor
        /// </summary>
		private MouseWheelGenerator() {

			if (HtmlPage.IsEnabled) {
				HtmlPage.Window.AttachEvent("DOMMouseScroll", this.HandleMouseWheel);
				HtmlPage.Window.AttachEvent("onmousewheel", this.HandleMouseWheel);
				HtmlPage.Document.AttachEvent("onmousewheel", this.HandleMouseWheel);
			}

		}

		private void HandleMouseWheel(object sender, HtmlEventArgs args) {
			double delta = 0;

			ScriptObject eventObj = args.EventObject;

			if (eventObj.GetProperty("wheelDelta") != null) {
				delta = ((double)eventObj.GetProperty("wheelDelta")) /120;


				if (HtmlPage.Window.GetProperty("opera") != null)
					delta = -delta;
			}
			else if (eventObj.GetProperty("detail") != null) {
				delta = -((double)eventObj.GetProperty("detail"))/3;

				if (HtmlPage.BrowserInformation.UserAgent.IndexOf("Macintosh") != -1)
					delta = delta * 3;
			}

			if (delta != 0) {
				if (this.OnMouseWheel(delta, args))
					args.PreventDefault();
			}
		}

		private bool OnMouseWheel(double delta, HtmlEventArgs e) {

			Point mousePosition = new Point(e.ClientX, e.ClientY);
			if (HtmlPage.BrowserInformation.Name.Contains("Netscape"))
				mousePosition = new Point(e.ScreenX, e.ScreenY);
			
			UIElement rootVisual = (UIElement)Application.Current.RootVisual;

			UIElement firstElement = null;
            foreach (UIElement element in VisualTreeHelper.FindElementsInHostCoordinates(mousePosition, rootVisual))
            {
				firstElement = element;
				break;
			}

			if (firstElement != null) {
                FrameworkElement source = firstElement as FrameworkElement;

                if (source != null)
                {
                    MouseWheelEventArgs wheelArgs = new MouseWheelEventArgs(source, delta);
                    MouseWheelGenerator.MouseWheelEvent.RaiseEvent(wheelArgs, source);

                    return wheelArgs.Handled;
                }
			}
			return false;
		}
	}
}
