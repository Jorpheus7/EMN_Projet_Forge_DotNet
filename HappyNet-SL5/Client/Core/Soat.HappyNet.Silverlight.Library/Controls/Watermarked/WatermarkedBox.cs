// -----------------------------------------------------------------------
// <copyright file="WatermarkedBox.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

///////////////////////////////////////////////////////////////////////////////
//
//  WatermarkedPasswordBox.xaml.cs
//
// This file is licensed with the Microsoft Public License (Ms-PL), for details look here: 
// http://www.opensource.org/licenses/ms-pl.html
//
///////////////////////////////////////////////////////////////////////////////

namespace Soat.HappyNet.Silverlight.Library.Controls
{
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using Soat.HappyNet.Silverlight.Library.Extensions;
    using System;

    /// <summary>
    /// WatermarkedPasswordBox is a specialized form of TextBox which displays custom visuals when its contents are empty
    /// </summary>
    [TemplatePart(Name = WatermarkedBox.ElementContentName, Type = typeof(ContentControl))]
    [TemplatePart(Name = WatermarkedBox.PasswordBoxElementName, Type = typeof(PasswordBox))]
    [TemplatePart(Name = WatermarkedBox.TextBoxElementName, Type = typeof(TextBox))]
    [TemplateVisualState(Name = VisualStateHelper.StateNormal, GroupName = VisualStateHelper.GroupCommon)]
    [TemplateVisualState(Name = VisualStateHelper.StateMouseOver, GroupName = VisualStateHelper.GroupCommon)]
    [TemplateVisualState(Name = VisualStateHelper.StateDisabled, GroupName = VisualStateHelper.GroupCommon)]
    [TemplateVisualState(Name = VisualStateHelper.StateUnfocused, GroupName = VisualStateHelper.GroupFocus)]
    [TemplateVisualState(Name = VisualStateHelper.StateFocused, GroupName = VisualStateHelper.GroupFocus)]
    [TemplateVisualState(Name = VisualStateHelper.StateUnwatermarked, GroupName = VisualStateHelper.GroupWatermark)]
    [TemplateVisualState(Name = VisualStateHelper.StateWatermarked, GroupName = VisualStateHelper.GroupWatermark)]
    public class WatermarkedBox : Control
    {
        #region Constants
        private const string ElementContentName = "Watermark";
        private const string TextBoxElementName = "TextBoxElement";
        private const string PasswordBoxElementName = "PasswordBoxElement";
        private const string TemplateXamlPath = "Microsoft.Windows.Controls.themes.generic.xaml";
        #endregion

        #region IsPasswordModeEnabled (DependencyProperty)

        /// <summary>
        /// A description of the property.
        /// </summary>
        public bool IsPasswordModeEnabled
        {
            get { return (bool)GetValue(IsPasswordModeEnabledProperty); }
            set { SetValue(IsPasswordModeEnabledProperty, value); }
        }
        public static readonly DependencyProperty IsPasswordModeEnabledProperty =
            DependencyProperty.Register("IsPasswordModeEnabled", typeof(bool), typeof(WatermarkedBox),
            new PropertyMetadata(new PropertyChangedCallback(OnIsPasswordModeEnabledChanged)));

        private static void OnIsPasswordModeEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((WatermarkedBox)d).OnIsPasswordModeEnabledChanged();
        }

        protected virtual void OnIsPasswordModeEnabledChanged()
        {
            if (elementTextBox != null && elementPasswordBox != null)
            {
                elementPasswordBox.SetVisibility(IsPasswordModeEnabled);
                elementTextBox.SetVisibility(!IsPasswordModeEnabled);

                elementPasswordBox.IsTabStop = IsPasswordModeEnabled;
                elementTextBox.IsTabStop = !IsPasswordModeEnabled;
            }
        }

        #endregion

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
            DependencyProperty.Register("Text", typeof(string), typeof(WatermarkedBox),
            new PropertyMetadata(string.Empty, new PropertyChangedCallback(OnTextChanged)));

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((WatermarkedBox)d).OnTextChanged(e);
        }

        protected virtual void OnTextChanged(DependencyPropertyChangedEventArgs e)
        {
            //string text = e.NewValue == null ? string.Empty : e.NewValue.ToString();
            //if (elementTextBox != null)
            //{
            //    elementTextBox.Text = text;
            //}
        }

        #endregion

        #region Password (DependencyProperty)

        /// <summary>
        /// A description of the property.
        /// </summary>
        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(WatermarkedBox),
            new PropertyMetadata(string.Empty, new PropertyChangedCallback(OnPasswordChanged)));

        private static void OnPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((WatermarkedBox)d).OnPasswordChanged(e);
        }

        protected virtual void OnPasswordChanged(DependencyPropertyChangedEventArgs e)
        {
            //string text = e.NewValue == null ? string.Empty : e.NewValue.ToString();
            //if (elementPasswordBox != null)
            //{
            //    elementPasswordBox.Password = text;
            //}
        }

        #endregion

        public event EventHandler TextChanged;

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="WatermarkedPasswordBox"/> class.
        /// </summary>
        public WatermarkedBox()
        {
            this.DefaultStyleKey = typeof(WatermarkedBox);
            SetDefaults();

            this.MouseEnter += OnMouseEnter;
            this.MouseLeave += OnMouseLeave;
            this.Loaded += OnLoaded;
            this.LostFocus += OnLostFocus;
            this.GotFocus += OnGotFocus;
        }
        #endregion

        #region Internal

        internal ContentControl elementContent;

        private TextBox elementTextBox;
        public TextBox TextBox
        {
            get { return elementTextBox; }
            set { elementTextBox = value; }
        }

        private PasswordBox elementPasswordBox;
        public PasswordBox PasswordBox
        {
            get { return elementPasswordBox; }
            set { elementPasswordBox = value; }
        }

        internal bool isHovered;
        internal bool hasFocus;

        //this method is made 'internal virtual' so the a TestWatermarkedPasswordBox with custom verification code
        //that executes in OnLoaded could be created
        internal virtual void OnLoaded(object sender, RoutedEventArgs e)
        {
            ApplyTemplate();
            ChangeVisualState(false);
        }

        /// <summary>
        /// Change to the correct visual state for the textbox.
        /// </summary>
        internal void ChangeVisualState()
        {
            ChangeVisualState(true);
        }

        /// <summary>
        /// Change to the correct visual state for the textbox.
        /// </summary>
        /// <param name="useTransitions">
        /// true to use transitions when updating the visual state, false to
        /// snap directly to the new visual state.
        /// </param>
        internal void ChangeVisualState(bool useTransitions)
        {
            // Update the CommonStates group
            if (!IsEnabled)
            {
                VisualStateHelper.GoToState(this, useTransitions, VisualStateHelper.StateDisabled, VisualStateHelper.StateNormal);
            }
            else if (isHovered)
            {
                VisualStateHelper.GoToState(this, useTransitions, VisualStateHelper.StateMouseOver, VisualStateHelper.StateNormal);
            }
            else
            {
                VisualStateHelper.GoToState(this, useTransitions, VisualStateHelper.StateNormal);
            }

            // Update the FocusStates group
            if (hasFocus && IsEnabled)
            {
                VisualStateHelper.GoToState(this, useTransitions, VisualStateHelper.StateFocused, VisualStateHelper.StateUnfocused);
            }
            else
            {
                VisualStateHelper.GoToState(this, useTransitions, VisualStateHelper.StateUnfocused);
            }

            // Update the WatermarkStates group
            if (!hasFocus && this.Watermark != null &&
                (
                    (!IsPasswordModeEnabled && string.IsNullOrEmpty(this.Text))
                 || (IsPasswordModeEnabled && string.IsNullOrEmpty(this.Password))
                ))
            {
                VisualStateHelper.GoToState(this, useTransitions, VisualStateHelper.StateWatermarked, VisualStateHelper.StateUnwatermarked);
            }
            else
            {
                VisualStateHelper.GoToState(this, useTransitions, VisualStateHelper.StateUnwatermarked);
            }
        }
        #endregion

        #region Protected

        /// <summary>
        /// Called when template is applied to the control.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            elementContent = ExtractTemplatePart<ContentControl>(ElementContentName);
            elementPasswordBox = ExtractTemplatePart<PasswordBox>(PasswordBoxElementName);
            elementTextBox = ExtractTemplatePart<TextBox>(TextBoxElementName);

            elementPasswordBox.PasswordChanged += new RoutedEventHandler(elementPasswordBox_PasswordChanged);
            elementTextBox.TextChanged += new TextChangedEventHandler(elementTextBox_TextChanged);

            elementTextBox.GotFocus += new RoutedEventHandler(OnGotFocus);
            elementPasswordBox.GotFocus += new RoutedEventHandler(OnGotFocus);

            elementTextBox.LostFocus += new RoutedEventHandler(OnLostFocus);
            elementPasswordBox.LostFocus += new RoutedEventHandler(OnLostFocus);

            OnIsPasswordModeEnabledChanged();
            OnWatermarkChanged();

            ChangeVisualState(false);
        }
        #endregion

        #region Public


        #region TextBoxStyle (DependencyProperty)

        /// <summary>
        /// A description of the property.
        /// </summary>
        public Style TextBoxStyle
        {
            get { return (Style)GetValue(TextBoxStyleProperty); }
            set { SetValue(TextBoxStyleProperty, value); }
        }
        public static readonly DependencyProperty TextBoxStyleProperty =
            DependencyProperty.Register("TextBoxStyle", typeof(Style), typeof(WatermarkedBox), null);

        #endregion


        #region PasswordBoxStyle (DependencyProperty)

        /// <summary>
        /// A description of the property.
        /// </summary>
        public Style PasswordBoxStyle
        {
            get { return (Style)GetValue(PasswordBoxStyleProperty); }
            set { SetValue(PasswordBoxStyleProperty, value); }
        }
        public static readonly DependencyProperty PasswordBoxStyleProperty =
            DependencyProperty.Register("PasswordBoxStyle", typeof(Style), typeof(WatermarkedBox), null);

        #endregion

        #region Watermark
        /// <summary>
        /// Watermark dependency property
        /// </summary>
        public static readonly DependencyProperty WatermarkProperty = DependencyProperty.Register(
            "Watermark", typeof(object), typeof(WatermarkedBox), new PropertyMetadata(OnWatermarkPropertyChanged));

        /// <summary>
        /// Watermark content
        /// </summary>
        /// <value>The watermark.</value>
        public object Watermark
        {
            get { return (object)GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }

        #endregion

        #endregion

        #region Private

        private T ExtractTemplatePart<T>(string partName) where T : DependencyObject
        {
            DependencyObject obj = GetTemplateChild(partName);
            return ExtractTemplatePart<T>(partName, obj);
        }

        private static T ExtractTemplatePart<T>(string partName, DependencyObject obj) where T : DependencyObject
        {
            return obj as T;
        }

        private void OnGotFocus(object sender, RoutedEventArgs e)
        {
            if (IsEnabled)
            {
                hasFocus = true;

                if (!string.IsNullOrEmpty(this.Text))
                {
                    elementTextBox.SelectAll();
                    elementPasswordBox.SelectAll();
                }

                ChangeVisualState();
            }
        }

        private void OnLostFocus(object sender, RoutedEventArgs e)
        {
            hasFocus = false;
            ChangeVisualState();
        }

        private void OnMouseEnter(object sender, MouseEventArgs e)
        {
            isHovered = true;

            if (!hasFocus)
            {
                ChangeVisualState();
            }
        }

        private void OnMouseLeave(object sender, MouseEventArgs e)
        {
            isHovered = false;

            ChangeVisualState();
        }

        void elementTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (elementTextBox.Text != this.Text)
            {
                this.Text = elementTextBox.Text;
            }

            ChangeVisualState();

            RaiseTextChanged();
        }

        void RaiseTextChanged()
        {
            if (TextChanged != null)
                TextChanged(this, EventArgs.Empty);
        }

        void elementPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (elementPasswordBox.Password != this.Password)
            {
                this.Password = elementPasswordBox.Password;
            }

            ChangeVisualState();

            RaiseTextChanged();
        }

        private void OnWatermarkChanged()
        {
            if (elementContent != null)
            {
                Control watermarkControl = this.Watermark as Control;
                if (watermarkControl != null)
                {
                    watermarkControl.IsTabStop = false;
                    watermarkControl.IsHitTestVisible = false;
                }
            }
        }

        /// <summary>
        /// Called when watermark property is changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnWatermarkPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            WatermarkedBox watermarkTextBox = sender as WatermarkedBox;
            Debug.Assert(watermarkTextBox != null, "The source is not an instance of a WatermarkedPasswordBox!");
            watermarkTextBox.OnWatermarkChanged();
            watermarkTextBox.ChangeVisualState();

        }

        private void SetDefaults()
        {
            IsEnabled = true;
            IsPasswordModeEnabled = false;
            IsTabStop = false;
        }

        #endregion
    }
}