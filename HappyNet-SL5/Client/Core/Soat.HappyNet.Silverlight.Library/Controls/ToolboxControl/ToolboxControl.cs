// -----------------------------------------------------------------------
// <copyright file="ToolboxControl.cs" company="So@t">
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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Soat.HappyNet.Silverlight.Library.Controls
{
    /// <summary>
    /// Ce controle représente une boite à outils pouvant être déplacée, agrandie, cachée
    /// </summary>
    [TemplateVisualState(Name = NormalState, GroupName = CommonStateGroup)]
    [TemplateVisualState(Name = HiddenState, GroupName = CommonStateGroup)]
    [TemplateVisualState(Name = AutoState, GroupName = CommonStateGroup)]
    [TemplatePart(Name = ToolboxControl.HideButtonElement, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = ToolboxControl.AutoButtonElement, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = ToolboxControl.NormalButtonElement, Type = typeof(FrameworkElement))]
    public class ToolboxControl : ContentControl
    {
        #region Consts

        /// <summary>
        /// Nom du Groupe contenant les Etats
        /// </summary>
        public const string CommonStateGroup = "CommonGroup";
        /// <summary>
        /// Etat normal, draggable
        /// </summary>
        public const string NormalState = "Normal";

        /// <summary>
        /// Etat caché
        /// </summary>
        public const string HiddenState = "Hidden";

        /// <summary>
        /// Etat hauteur automatique
        /// </summary>
        public const string AutoState = "Auto";

        private const string HideButtonElement = "HideButton";
        private const string AutoButtonElement = "AutoButton";
        private const string NormalButtonElement = "NormalButton";

        #endregion

        #region Private Members

        private FrameworkElement hideButton = null;
        private FrameworkElement autoButton = null;
        private FrameworkElement normalButton = null;

        private ToolboxState oldState = ToolboxState.Normal;

        #endregion

        #region Title Dependency Property

        /// <summary>
        /// Titre de la toolbox
        /// </summary>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        /// <summary>
        /// Titre de la toolbox
        /// // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(ToolboxControl), new PropertyMetadata(""));


        #endregion

        //#region Image Dependency Property

        ///// <summary>
        ///// Titre de la toolbox
        ///// </summary>
        //public ImageSource Image
        //{
        //    get { return (ImageSource)GetValue(ImageProperty); }
        //    set { SetValue(ImageProperty, value); }
        //}

        ///// <summary>
        ///// Titre de la toolbox
        ///// // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        ///// </summary>
        //public static readonly DependencyProperty ImageProperty = DependencyProperty.Register("Image", typeof(ImageSource), typeof(ToolboxControl), new PropertyMetadata(""));


        //#endregion

        #region ToolBoxState Dependency Property

        /// <summary>
        /// Etat d'affichage du Control
        /// </summary>
        public ToolboxState State
        {
            get { return (ToolboxState)GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }

        /// <summary>
        /// Etat d'affichage du Control
        /// Using a DependencyProperty as the backing store for State.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty StateProperty = DependencyProperty.Register("State", typeof(ToolboxState), typeof(ToolboxControl), new PropertyMetadata(ToolboxState.Normal, new PropertyChangedCallback(OnStatePropertyChanged)));

        /// <summary>
        /// Gestion du Changement d'état d'affichage
        /// </summary>
        /// <param name="sender">ToolBox</param>
        /// <param name="e">Argument contenant le nouvel état</param>
        static void OnStatePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ToolboxControl control = sender as ToolboxControl;
            if (control != null && e.NewValue is ToolboxState)
            {
                control.ChangeState((ToolboxState)e.NewValue);
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Contrusteur par défaut
        /// </summary>
        public ToolboxControl()
        {
            this.DefaultStyleKey = typeof(ToolboxControl);
        } 

        #endregion

        #region OnApplyTemplate Method

        /// <summary>
        /// Cette méthode permet d'appliquer le template sur le control
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.hideButton = this.GetTemplateChild(ToolboxControl.HideButtonElement) as FrameworkElement;
            this.autoButton = this.GetTemplateChild(ToolboxControl.AutoButtonElement) as FrameworkElement;
            this.normalButton = this.GetTemplateChild(ToolboxControl.NormalButtonElement) as FrameworkElement;

            this.hideButton.MouseLeftButtonDown += new MouseButtonEventHandler(OnHideMouseLeftButtonDown);
            this.autoButton.MouseLeftButtonDown += new MouseButtonEventHandler(OnAutoMouseLeftButtonDown);
            this.normalButton.MouseLeftButtonDown += new MouseButtonEventHandler(OnNormalMouseLeftButtonDown);

            this.ChangeState(this.State);
        }

        #endregion

        private void ChangeState(ToolboxState state)
        {
            switch (state)
            {
                case ToolboxState.Normal:
                    this.oldState = state;
                    VisualStateManager.GoToState(this, NormalState, true);
                    break;
                case ToolboxState.Auto:
                    this.oldState = state;
                    VisualStateManager.GoToState(this, AutoState, true);
                    break;
                case ToolboxState.Hidden:
                    VisualStateManager.GoToState(this, HiddenState, true);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Gestion de Bouton hideButton Down
        /// </summary>
        /// <param name="sender">Element</param>
        /// <param name="e">Arguement de la souris</param>
        void OnHideMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.State = ToolboxState.Hidden;
        }

        /// <summary>
        /// Gestion de Bouton autoButton Down
        /// </summary>
        /// <param name="sender">Element</param>
        /// <param name="e">Arguement de la souris</param>
        void OnAutoMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.State.Equals(ToolboxState.Auto))
            {
                this.State = ToolboxState.Normal;
            }
            else
            {
                this.State = ToolboxState.Auto;
            }
        }

        /// <summary>
        /// Gestion de Bouton normalButton Down
        /// </summary>
        /// <param name="sender">Element</param>
        /// <param name="e">Arguement de la souris</param>
        void OnNormalMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.State = this.oldState;
        }
    }
}
