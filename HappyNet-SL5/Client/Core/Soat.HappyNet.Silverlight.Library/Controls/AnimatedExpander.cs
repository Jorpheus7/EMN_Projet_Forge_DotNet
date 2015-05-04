//-----------------------------------------------------------------------
// <copyright file="AnimatedExpander.cs" company="Microsoft Corporation copyright 2008.">
// (c) 2008 Microsoft Corporation. All rights reserved.
// This source is subject to the Microsoft Public License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// </copyright>
// <date>02-Mar-2009</date>
// <author>Martin Grayson</author>
// <summary>An animated expanding / collapsing control.</summary>
//-----------------------------------------------------------------------
namespace Soat.HappyNet.Silverlight.Library.Controls
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;
    using System.Windows.Media.Animation;

    /// <summary>
    /// An animated expanding / collapsing control.
    /// </summary>
    public class AnimatedExpander : HeaderedContentControl
    {
        /// <summary>
        /// The IsExpanded Dependency Property.
        /// </summary>
        public static readonly DependencyProperty IsExpandedProperty =
           DependencyProperty.Register("IsExpanded", typeof(bool), typeof(AnimatedExpander), new PropertyMetadata(false));


        #region Indentation (DependencyProperty)

        /// <summary>
        /// A description of the property.
        /// </summary>
        public Thickness Indentation
        {
            get { return (Thickness)GetValue(IndentationProperty); }
            set { SetValue(IndentationProperty, value); }
        }
        public static readonly DependencyProperty IndentationProperty =
            DependencyProperty.Register("Indentation", typeof(Thickness), typeof(AnimatedExpander), null);

        #endregion


        #region HeaderBackground (DependencyProperty)

        /// <summary>
        /// A description of the property.
        /// </summary>
        public Brush HeaderBackground
        {
            get { return (Brush)GetValue(HeaderBackgroundProperty); }
            set { SetValue(HeaderBackgroundProperty, value); }
        }
        public static readonly DependencyProperty HeaderBackgroundProperty =
            DependencyProperty.Register("HeaderBackground", typeof(Brush), typeof(AnimatedExpander), null);

        #endregion


        #region HeaderForeground (DependencyProperty)

        /// <summary>
        /// A description of the property.
        /// </summary>
        public Brush HeaderForeground
        {
            get { return (Brush)GetValue(HeaderForegroundProperty); }
            set { SetValue(HeaderForegroundProperty, value); }
        }
        public static readonly DependencyProperty HeaderForegroundProperty =
            DependencyProperty.Register("HeaderForeground", typeof(Brush), typeof(AnimatedExpander), null);

        #endregion

        #region HeaderStyle (DependencyProperty)

        /// <summary>
        /// A description of the property.
        /// </summary>
        public Style HeaderStyle
        {
            get { return (Style)GetValue(HeaderStyleProperty); }
            set { SetValue(HeaderStyleProperty, value); }
        }
        public static readonly DependencyProperty HeaderStyleProperty =
            DependencyProperty.Register("HeaderStyle", typeof(Style), typeof(AnimatedExpander), null);

        #endregion


        /// <summary>
        /// The LayoutRoot Template Part Name.
        /// </summary>
        private const string ElementLayoutRoot = "LayoutRoot";

        /// <summary>
        /// The ContentContentPresenter Template Part Name.
        /// </summary>
        private const string ElementContentContentPresenter = "ContentContentPresenter";

        /// <summary>
        /// The ExpandToggleButton Template Part Name.
        /// </summary>
        private const string ElementExpandToggleButton = "ExpandToggleButton";

        /// <summary>
        /// The ExpandStoryboard Template Part Name.
        /// </summary>
        private const string ElementExpandStoryboard = "ExpandStoryboard";

        /// <summary>
        /// The ExpandKeyFrame Template Part Name.
        /// </summary>
        private const string ElementExpandKeyFrame = "ExpandKeyFrame";

        /// <summary>
        /// The CollapseStoryboard Template Part Name.
        /// </summary>
        private const string ElementCollapseStoryboard = "CollapseStoryboard";

        private const string ElementScrollingView = "ScrollViewer";

        /// <summary>
        /// Store the expand toggle button.
        /// </summary>
        private ToggleButton expandToggleButton;

        private ContentPresenter contentContentPresenter;

        /// <summary>
        /// Stores the expand key frame.
        /// </summary>
        private SplineDoubleKeyFrame expandKeyFrame;
        
        /// <summary>
        /// Stores the expand storyboard.
        /// </summary>
        private Storyboard expandStoryboard;

        /// <summary>
        /// Stores the collapse storyboard.
        /// </summary>
        private Storyboard collapseStoryboard;

        private ScrollViewer scrollingView;

        /// <summary>
        /// AnimatedExpander constructor.
        /// </summary>
        public AnimatedExpander()
        {
            this.DefaultStyleKey = typeof(AnimatedExpander);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the control is expanded on not.
        /// </summary>
        [System.ComponentModel.Category("Layout"), System.ComponentModel.Description("Gets or sets a value indicating whether the control is expanded on not.")]
        public bool IsExpanded
        {
            get
            {
                if (this.expandToggleButton != null)
                {
                    return this.expandToggleButton.IsChecked.Value;
                }

                return (bool)GetValue(IsExpandedProperty);
            }

            set
            {
                SetValue(IsExpandedProperty, value);
            }
        }

        /// <summary>
        /// Gets the template parts from the template.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            FrameworkElement layoutRoot = this.GetTemplateChild(AnimatedExpander.ElementLayoutRoot) as FrameworkElement;
            if (layoutRoot != null)
            {
                if (layoutRoot.Resources.Contains(AnimatedExpander.ElementExpandStoryboard))
                {
                    this.expandStoryboard = layoutRoot.Resources[AnimatedExpander.ElementExpandStoryboard] as Storyboard;
                    this.expandStoryboard.Completed += new EventHandler(expandStoryboard_Completed);
                }

                if (layoutRoot.Resources.Contains(AnimatedExpander.ElementCollapseStoryboard))
                {
                    this.collapseStoryboard = layoutRoot.Resources[AnimatedExpander.ElementCollapseStoryboard] as Storyboard;
                    this.collapseStoryboard.Completed += new EventHandler(collapseStoryboard_Completed);
                }

                this.expandKeyFrame = layoutRoot.FindName(AnimatedExpander.ElementExpandKeyFrame) as SplineDoubleKeyFrame;
            }

            this.expandToggleButton = this.GetTemplateChild(AnimatedExpander.ElementExpandToggleButton) as ToggleButton;
            if (this.expandToggleButton != null)
            {
                this.expandToggleButton.Checked += new RoutedEventHandler(this.ExpandToggleButton_Checked);
                this.expandToggleButton.Unchecked += new RoutedEventHandler(this.ExpandToggleButton_Unchecked);
            }

            this.contentContentPresenter = this.GetTemplateChild(AnimatedExpander.ElementContentContentPresenter) as ContentPresenter;
            if (contentContentPresenter != null)
            {
                contentContentPresenter.SizeChanged += new SizeChangedEventHandler(this.ContentContentPresenter_SizeChanged);
            }

            scrollingView = this.GetTemplateChild(AnimatedExpander.ElementScrollingView) as ScrollViewer;
        }

        void collapseStoryboard_Completed(object sender, EventArgs e)
        {
            
        }

        void expandStoryboard_Completed(object sender, EventArgs e)
        {
            //if (scrollingView != null)
            //{
            //    scrollingView.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
            //}
        }

        /// <summary>
        /// Expands the control.
        /// </summary>
        /// <param name="sender">The expand toggle button.</param>
        /// <param name="e">Routed Event Args.</param>
        private void ExpandToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            if (this.expandStoryboard != null)
            {
                this.expandStoryboard.Begin();
            }
        }

        /// <summary>
        /// Collapses the control.
        /// </summary>
        /// <param name="sender">The expand toggle button.</param>
        /// <param name="e">Routed Event Args.</param>
        private void ExpandToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            if (this.collapseStoryboard != null)
            {
                //scrollingView.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
                this.collapseStoryboard.Begin();
            }
        }

        /// <summary>
        /// Updates the key frame and height of the control.
        /// </summary>
        /// <param name="sender">The content content presenter.</param>
        /// <param name="e">Size Changed Event Args.</param>
        private void ContentContentPresenter_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.expandKeyFrame != null)
            {
                this.expandKeyFrame.Value = e.NewSize.Height;

                if (this.expandToggleButton != null && this.expandToggleButton.IsChecked.Value && this.expandStoryboard != null)
                {
                    this.expandStoryboard.Begin();
                }
            }
        }
    }
}
