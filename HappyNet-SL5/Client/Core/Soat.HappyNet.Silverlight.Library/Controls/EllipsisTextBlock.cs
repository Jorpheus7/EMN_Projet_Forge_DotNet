// -----------------------------------------------------------------------
// <copyright file="EllipsisTextBlock.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Soat.HappyNet.Silverlight.Library.Controls
{
    public enum EllipsisMode
    {
        Ellipsis,
        Fade,
        Hide
    }

    public class EllipsisTextBlock : ContentControl
    {
        #region Text (DependencyProperty)

        /// <summary>
        /// A description of the property.
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(EllipsisTextBlock),
            new PropertyMetadata(new PropertyChangedCallback(OnTextChanged)));

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((EllipsisTextBlock)d).OnTextChanged(e);
        }

        protected virtual void OnTextChanged(DependencyPropertyChangedEventArgs e)
        {
            base.Content = e.NewValue;
            UpdateText();
        }

        #endregion


        #region EllipsisMode (DependencyProperty)

        /// <summary>
        /// A description of the property.
        /// </summary>
        public EllipsisMode EllipsisMode
        {
            get { return (EllipsisMode)GetValue(EllipsisModeProperty); }
            set { SetValue(EllipsisModeProperty, value); }
        }
        public static readonly DependencyProperty EllipsisModeProperty =
            DependencyProperty.Register("EllipsisMode", typeof(EllipsisMode), typeof(EllipsisTextBlock),
            new PropertyMetadata(new PropertyChangedCallback(OnEllipsisModeChanged)));

        private static void OnEllipsisModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((EllipsisTextBlock)d).OnEllipsisModeChanged(e);
        }

        protected virtual void OnEllipsisModeChanged(DependencyPropertyChangedEventArgs e)
        {
        }

        #endregion

        FrameworkElement rootElement;
        static LinearGradientBrush fadeMask;
        private Popup toolTipPopup;

        public EllipsisTextBlock()
        {
            DefaultStyleKey = typeof(EllipsisTextBlock);
            EllipsisMode = EllipsisMode.Ellipsis;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            rootElement = (FrameworkElement)base.GetTemplateChild("RootElement");
            if (rootElement != null)
            {
                rootElement.SizeChanged += new SizeChangedEventHandler(this.RootElement_SizeChanged);
            }

            this.toolTipPopup = (Popup)base.GetTemplateChild("ToolTipPopup");
            if ((this.toolTipPopup != null) && (this.toolTipPopup.Child is FrameworkElement))
            {
                (this.toolTipPopup.Child as FrameworkElement).MouseLeave += new MouseEventHandler(this.ToolTip_MouseLeave);
            }
        }

        internal void OpenToolTip()
        {
            if (this.toolTipPopup != null && this.toolTipPopup.Child is ContentControl)
            {
                try
                {
                    //Point point = this.TransformToVisual(this).Transform(new Point(0.0, 0.0));
                    this.toolTipPopup.VerticalOffset = -4;
                    this.toolTipPopup.HorizontalOffset = -3;
                    ((ContentControl)this.toolTipPopup.Child).Content = this.Text;
                    this.toolTipPopup.IsOpen = true;
                }
                catch
                {
                }
            }
        }

        internal void CloseToolTip()
        {
            if (this.toolTipPopup != null)
            {
                this.toolTipPopup.IsOpen = false;
            }
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);

            double width = (base.ActualWidth - base.Padding.Left) - base.Padding.Right;
            if (MeasureString(this.Text, base.FontFamily, base.FontWeight, base.FontSize) > width)
            {
                this.OpenToolTip();
            }
            else
            {
                this.CloseToolTip();
            }
        }

        private void ToolTip_MouseLeave(object sender, MouseEventArgs e)
        {
            this.CloseToolTip();
        }

        void RootElement_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateText();
        }

        void UpdateText()
        {
            if (rootElement != null)
            {
                if (this.EllipsisMode != EllipsisMode.Hide)
                {
                    double width = (this.ActualWidth - base.Padding.Left) - base.Padding.Right;
                    if (MeasureString(this.Text, base.FontFamily, base.FontWeight, base.FontSize) > width)
                    {
                        if (this.EllipsisMode == EllipsisMode.Ellipsis)
                        {
                            base.Content = this.CreateEllipsisString(this.Text, 0, this.Text.Length - 2, width - 5.0);
                        }
                        else if (this.EllipsisMode == EllipsisMode.Fade)
                        {
                            if (fadeMask == null)
                            {
                                LinearGradientBrush brush = new LinearGradientBrush
                                {
                                    GradientStops = { new GradientStop(), new GradientStop(), new GradientStop() }
                                };
                                brush.GradientStops[0].Color = Color.FromArgb(0xff, 0, 0, 0);
                                brush.GradientStops[1].Color = Color.FromArgb(0xff, 0, 0, 0);
                                brush.GradientStops[2].Color = Color.FromArgb(0, 0, 0, 0);
                                brush.GradientStops[0].Offset = 0.0;
                                brush.GradientStops[1].Offset = 0.85;
                                brush.GradientStops[2].Offset = 1.0;
                                fadeMask = brush;
                            }
                            rootElement.OpacityMask = fadeMask;
                        }
                    }
                    else if (this.EllipsisMode == EllipsisMode.Ellipsis)
                    {
                        base.Content = this.Text;
                    }
                    else if (this.EllipsisMode == EllipsisMode.Fade)
                    {
                        rootElement.OpacityMask = null;
                    }
                }
            }
        }

        private string CreateEllipsisString(string text, int minLength, int maxLength, double maxWidth)
        {
            if (text.Length == 1)
            {
                return ".";
            }
            if ((maxLength - minLength) < 2)
            {
                return (text.Substring(0, minLength) + "...");
            }
            int length = minLength + ((maxLength - minLength) / 2);
            if (MeasureString(text.Substring(0, length) + "...", base.FontFamily, base.FontWeight, base.FontSize) <= maxWidth)
            {
                return this.CreateEllipsisString(text, length, maxLength, maxWidth);
            }
            return this.CreateEllipsisString(text, minLength, length, maxWidth);
        }

        private static double MeasureString(string text, FontFamily fontFamily, FontWeight fontWeight, double fontSize)
        {
            TextBlock block = new TextBlock
            {
                FontFamily = fontFamily,
                FontSize = fontSize,
                FontWeight = fontWeight,
                Text = text
            };
            return block.ActualWidth;
        }
    }
}
