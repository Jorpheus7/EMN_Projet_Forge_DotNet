// -----------------------------------------------------------------------
// <copyright file="ScrollableViews.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

//-------------------------------------------------------------------------
// Basé sur la version de Peter Blois
//-------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Media;
using Soat.HappyNet.Silverlight.Library.Extensions;

namespace Soat.HappyNet.Silverlight.Library.Components.Mouse
{

	/// <summary>
	/// Rend tous les scrollviewers scrollable à la souris
	/// </summary>
	public static class ScrollableViews {
		private static bool initialized = false;

        /// <summary>
        /// Initialise tous les scrollviewers pour les rendre scrollable à la souris
        /// </summary>
		public static void Initialize() {

			if (!ScrollableViews.initialized) {
                MouseWheelGenerator.MouseWheelEvent.RegisterClassHandler(typeof(DependencyObject), ScrollableViews.HandleMouseWheel, false);

				ScrollableViews.initialized = true;
			}
		}

        private const int SCROLLAMOUNT = 3;

        private static void HandleMouseWheel(object sender, MouseWheelEventArgs e)
        {
            DependencyObject el = sender as DependencyObject;

            List<Type> typeList = new List<Type>() 
            { 
                typeof(TextBox),
                typeof(DataGrid),
                typeof(ScrollViewer), 
                typeof(ListBox)
            };

            DependencyObject obj = el.GetFirstParentOfType(typeList);

            if (obj == null)
                return;

            while (obj != null)
            {
                if (obj is TextBox || obj is DataGrid || obj is ScrollViewer || obj is ListBox)
                {
                    if (ScrollableViews.ScrollableHandleMouseWheel(obj, e))
                        return;
                }
                else if (obj is Slider)
                {
                    if (ScrollableViews.SliderHandleMouseWheel(obj, e))
                        return;
                }

                obj = obj.GetFirstParentOfType(typeList);
            }
        }


        private static bool ScrollableHandleMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Handled)
                return true;

            UIElement el = sender as UIElement;

            if (el is ScrollViewer)
            {
                ScrollHandleMouseWheel((ScrollViewer)el, e);
            }
            else
            {
                AutomationPeer autopeer = FrameworkElementAutomationPeer.CreatePeerForElement(el);
                if (autopeer != null)
                {
                    IScrollProvider scroller = autopeer.GetPattern(PatternInterface.Scroll) as IScrollProvider;
                    if (scroller != null)
                    {
                        if (scroller.VerticallyScrollable)
                        {
                            if (e.Delta > 0 && scroller.VerticalScrollPercent > 0.00)
                            {
                                for (int i = 0; i < SCROLLAMOUNT; i++)
                                {
                                    if (scroller.VerticalScrollPercent <= 0.00)
                                        break;

                                    scroller.Scroll(System.Windows.Automation.ScrollAmount.NoAmount,
                                        System.Windows.Automation.ScrollAmount.LargeDecrement);
                                }

                                return (e.Handled = true);
                            }
                            else if (e.Delta < 0 && scroller.VerticalScrollPercent < 100.00)
                            {
                                for (int i = 0; i < SCROLLAMOUNT; i++)
                                {
                                    if (scroller.VerticalScrollPercent >= 100.00)
                                        break;

                                    scroller.Scroll(System.Windows.Automation.ScrollAmount.NoAmount,
                                        System.Windows.Automation.ScrollAmount.LargeIncrement);
                                }

                                return (e.Handled = true);
                            }
                        }
                        else if (scroller.HorizontallyScrollable)
                        {
                            if (e.Delta > 0 && scroller.HorizontalScrollPercent > 0.00)
                            {
                                for (int i = 0; i < SCROLLAMOUNT; i++)
                                {
                                    if (scroller.HorizontalScrollPercent <= 0.00)
                                        break;

                                    scroller.Scroll(System.Windows.Automation.ScrollAmount.LargeDecrement,
                                        System.Windows.Automation.ScrollAmount.NoAmount);
                                }

                                return (e.Handled = true);
                            }
                            else if (e.Delta < 0 && scroller.HorizontalScrollPercent < 100.00)
                            {
                                for (int i = 0; i < SCROLLAMOUNT; i++)
                                {
                                    if (scroller.HorizontalScrollPercent >= 100.00)
                                        break;

                                    scroller.Scroll(System.Windows.Automation.ScrollAmount.LargeIncrement,
                                        System.Windows.Automation.ScrollAmount.NoAmount);
                                }

                                return (e.Handled = true);
                            }
                        }
                    }
                }
            }

            return e.Handled;
        }

		private static bool ScrollHandleMouseWheel(object sender, MouseWheelEventArgs e) 
        {
            if (e.Handled)
                return true;

            ScrollViewer sv = sender as ScrollViewer;

            double verticalOffset = sv.VerticalOffset;
            if (e.Delta > 0 && verticalOffset > 0)
            {
                sv.ScrollToVerticalOffset(verticalOffset - e.Delta * 50);
                e.Handled = true;
            }
            else if (e.Delta < 0 && verticalOffset < sv.ScrollableHeight)
            {
                sv.ScrollToVerticalOffset(verticalOffset - e.Delta * 50);
                e.Handled = true;
            }

            if (!e.Handled && sv.ScrollableHeight == 0)
            {
                double horizontalOffset = sv.HorizontalOffset;
                if (e.Delta > 0 && horizontalOffset > 0)
                {
                    sv.ScrollToHorizontalOffset(horizontalOffset - e.Delta * 50);
                    e.Handled = true;
                }
                else if (e.Delta < 0 && horizontalOffset < sv.ScrollableWidth)
                {
                    sv.ScrollToHorizontalOffset(horizontalOffset - e.Delta * 50);
                    e.Handled = true;
                }
            }

            return e.Handled;
		}

        private static bool SliderHandleMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Slider sl = sender as Slider;

            if (e.Delta < 0 && sl.Value < sl.Maximum)
            {
                sl.Value += sl.LargeChange;
                e.Handled = true;
            }
            else if (e.Delta > 0 && sl.Value > sl.Minimum)
            {
                sl.Value -= sl.LargeChange;
                e.Handled = true;
            }

            return e.Handled;
        }

        private static void ListBoxHandleMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ListBox lb = sender as ListBox;

            int max = VisualTreeHelper.GetChildrenCount(lb);
            for (int i = 0; i < max; i++)
            {
                ScrollViewer scroll = VisualTreeHelper.GetChild(lb, i) as ScrollViewer;
                if (scroll != null)
                {
                    ScrollHandleMouseWheel(scroll, e);
                    break;
                }
            }
        }
	}
}
