// -----------------------------------------------------------------------
// <copyright file="MoveableControl.cs" company="So@t">
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
using Soat.HappyNet.Silverlight.Library.Helpers;

namespace Soat.HappyNet.Silverlight.Library.Controls
{
    /// <summary>
    /// Cet Control représente un control permettant de faire bouger son contenu sur une surface
    /// </summary>
    [TemplatePart(Name = MoveableControl.LayoutRoot, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = MoveableControl.ElementHeaderContent, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = MoveableControl.ElementContent, Type = typeof(ContentControl))]
    [TemplatePart(Name = MoveableControl.ElementFooterContent, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = MoveableControl.LayoutRootTranslateTransform, Type = typeof(TranslateTransform))]
    public class MoveableControl : ContentControl
    {
        #region Consts

        private const string LayoutRoot = "LayoutRoot";
        private const string ElementHeaderContent = "HeaderContent";
        private const string ElementContent = "ElementContent";
        private const string ElementFooterContent = "FooterContent";
        private const string LayoutRootTranslateTransform = "LayoutRootTranslateTransform";

        #endregion

        #region Private Members

        /// <summary>
        /// Panel contenant l'ensemble du control
        /// </summary>
        private FrameworkElement layout = null;

        /// <summary>
        /// Transformation de déplacement du control
        /// </summary>
        private TranslateTransform rootTranslateTransform = null;

        /// <summary>
        /// Element permettant le drag and drop (Header du control)
        /// </summary>
        private ContentPresenter header = null;

        /// <summary>
        /// Element de pied du contenu
        /// </summary>
        private ContentPresenter footer = null;

        /// <summary>
        /// Element contenu
        /// </summary>
        private ContentPresenter content = null;

        /// <summary>
        /// Element graphique sur lequel la propriété moveable est possible
        /// </summary>
        private FrameworkElement moveabledElement;

        /// <summary>
        /// Gestion de la capture du clic de souris
        /// </summary>
        private Boolean isLayoutRootMouseCapture = false;

        /// <summary>
        /// Positionnement ancienne
        /// </summary>
        private Point oldPoint;

        #endregion
        
        #region ClickPosition Dependency Property

        /// <summary>
        /// Positionnement du Click
        /// </summary>
        public Point ClickPosition
        {
            get { return (Point)GetValue(ClickPositionProperty); }
            set { SetValue(ClickPositionProperty, value); }
        }

        /// <summary>
        /// Positionnement du Click
        /// // Using a DependencyProperty as the backing store for ClickPosition.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty ClickPositionProperty = DependencyProperty.Register("ClickPosition", typeof(Point), typeof(MoveableControl), new PropertyMetadata(new Point(0.0, 0.0)));

        #endregion

        #region OriginePosition Dependency Property

        /// <summary>
        /// Position origine du controle
        /// </summary>
        public Point OriginePosition
        {
            get { return (Point)GetValue(OriginePositionProperty); }
            set { SetValue(OriginePositionProperty, value); }
        }

        /// <summary>
        /// Signifie que le control est dragable ou non 
        /// // Using a DependencyProperty as the backing store for IsDraggable.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty OriginePositionProperty = DependencyProperty.Register("OriginePosition", typeof(Point), typeof(MoveableControl), new PropertyMetadata(new Point(0.0, 0.0), OnOriginePositionPropertyChanged));

        /// <summary>
        /// Gestion du Changement de position d'origine
        /// </summary>
        /// <param name="sender">Control</param>
        /// <param name="e">Argument contenant la nouvelle Position</param>
        static void OnOriginePositionPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            MoveableControl control = sender as MoveableControl;
            Point newPosition = (Point)e.NewValue;
            if (sender != null)
            {
                control.oldPoint = new Point() { X = newPosition.X, Y = newPosition.Y };
            }
        }

        #endregion

        #region Position Dependency Property

        /// <summary>
        /// Position courant du control
        /// </summary>
        public Point Position
        {
            get { return (Point)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        /// <summary>
        /// Position courant du control
        /// // Using a DependencyProperty as the backing store for Position.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty PositionProperty = DependencyProperty.Register("Position", typeof(Point), typeof(MoveableControl), new PropertyMetadata(new Point(0, 0)));

        #endregion
       
        #region Header Dependency Property

        /// <summary>
        /// Entête du Contenu draggable
        /// </summary>
        public UIElement HeaderContent
        {
            get { return (UIElement)GetValue(HeaderContentProperty); }
            set { SetValue(HeaderContentProperty, value); }
        }

        /// <summary>
        /// Entête du Contenu draggable
        /// // Using a DependencyProperty as the backing store for HeaderContent.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty HeaderContentProperty = DependencyProperty.Register("HeaderContent", typeof(UIElement), typeof(UIElement), null);


        #endregion

        #region Footer Dependency Property

        /// <summary>
        /// Pied du Contenu draggable
        /// </summary>
        public UIElement FooterContent
        {
            get { return (UIElement)GetValue(FooterContentProperty); }
            set { SetValue(FooterContentProperty, value); }
        }

        /// <summary>
        /// Pied du Contenu draggable
        /// // Using a DependencyProperty as the backing store for HeaderContent.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty FooterContentProperty = DependencyProperty.Register("FooterContent", typeof(UIElement), typeof(UIElement), null);


        #endregion

        #region IsDraggable Dependency Property

        /// <summary>
        /// Signifie que le control est dragable ou non 
        /// </summary>
        public bool IsDraggable
        {
            get { return (bool)GetValue(IsDraggableProperty); }
            set { SetValue(IsDraggableProperty, value); }
        }

        /// <summary>
        /// Signifie que le control est dragable ou non 
        /// // Using a DependencyProperty as the backing store for IsDraggable.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty IsDraggableProperty = DependencyProperty.Register("IsDraggable", typeof(bool), typeof(MoveableControl), new PropertyMetadata(true, OnIsDraggablePropertyChanged));

        /// <summary>
        /// Gestion du Changement d'état d'affichage
        /// </summary>
        /// <param name="sender">ToolBox</param>
        /// <param name="e">Argument contenant le nouvel état</param>
        static void OnIsDraggablePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            MoveableControl control = sender as MoveableControl;
            bool newIsDraggable = (bool)e.NewValue;
            if (sender != null)
            {
                if (newIsDraggable && control.oldPoint != null)
                {
                    control.rootTranslateTransform.X = control.oldPoint.X;
                    control.rootTranslateTransform.Y = control.oldPoint.Y;
                }
                else
                {
                    if (control.rootTranslateTransform != null)
                    {
                        control.oldPoint = new Point() { X = control.rootTranslateTransform.X, Y = control.rootTranslateTransform.Y };
                        control.rootTranslateTransform.X = 0.0;
                        control.rootTranslateTransform.Y = 0.0;
                    }
                }
                control.isLayoutRootMouseCapture = false;
            }
        }

        #endregion
        
        #region ControlSize Dependency Property

        /// <summary>
        /// Taille du Control
        /// </summary>
        public Size ControlSize
        {
            get { return (Size)GetValue(ControlSizeProperty); }
            set { SetValue(ControlSizeProperty, value); }
        }

        /// <summary>
        /// Taille du Control
        /// // Using a DependencyProperty as the backing store for ControlSize.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty ControlSizeProperty = DependencyProperty.Register("ControlSize", typeof(Size), typeof(MoveableControl), new PropertyMetadata(new Size(0.0, 0.0)));

        #endregion


        #region public SelectEvent Event

        /// <summary>
        /// Evènement SelectEvent
        /// </summary>
        public event EventHandler<MoveableContentEventArgs> SelectEvent;

        /// <summary>
        /// Evènement SelectEvent
        /// </summary>
        protected virtual void OnSelectEvent(MoveableContentEventArgs e)
        {
            EventHandler<MoveableContentEventArgs> handler = SelectEvent;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion

        #region public UnSelectEvent Event

        /// <summary>
        /// Evènement UnSelectEvent
        /// </summary>
        public event EventHandler<MoveableContentEventArgs> UnSelectEvent;

        /// <summary>
        /// Evènement UnSelectEvent
        /// </summary>
        protected virtual void OnUnSelectEvent(MoveableContentEventArgs e)
        {
            EventHandler<MoveableContentEventArgs> handler = UnSelectEvent;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion

        #region MeasureOverride Method

        /// <summary>
        /// Surcharge du Changement de Taille du Component
        /// </summary>
        /// <param name="availableSize">Taille</param>
        /// <returns>Taille</returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            Size size = base.MeasureOverride(availableSize);
            this.ControlSize = size;
            return size;
        } 

        #endregion
        

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MoveableControl()
        {
            this.DefaultStyleKey = typeof(MoveableControl);
        }

        #endregion

        #region OnApplyTemplate Method

        /// <summary>
        /// Cette méthode permet d'appliquer le template sur le control
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.layout = this.GetTemplateChild(MoveableControl.LayoutRoot) as FrameworkElement;
            this.rootTranslateTransform = this.GetTemplateChild(MoveableControl.LayoutRootTranslateTransform) as TranslateTransform;
            this.header = this.GetTemplateChild(MoveableControl.ElementHeaderContent) as ContentPresenter;
            this.footer = this.GetTemplateChild(MoveableControl.ElementFooterContent) as ContentPresenter;
            this.content = this.GetTemplateChild(MoveableControl.ElementContent) as ContentPresenter;
            if (this.header != null && ((ContentPresenter)header).Content !=null)
            {
                this.moveabledElement = this.header;
            }
            else
            {
                if (this.footer != null && ((ContentPresenter)footer).Content != null)
                {
                    this.moveabledElement = this.footer;
                }
                else
                {
                    this.moveabledElement = this.layout;
                }
            }

            this.moveabledElement.MouseLeftButtonDown += new MouseButtonEventHandler(OnMouseLeftButtonDown);

            // Repositionnement de l'item
            if (this.OriginePosition.X != 0.0 && this.OriginePosition.Y != 0.0)
            {
                if (this.rootTranslateTransform != null)
                {
                    this.rootTranslateTransform.X = this.OriginePosition.X;
                    this.rootTranslateTransform.Y = this.OriginePosition.Y;

                    if (this.IsDraggable)
                    {
                        this.OnMouseLeftButtonDown(this, null);
                    }
                }
            }
        }

        #endregion

        #region Dragging Methods

        /// <summary>
        /// Gestion du déplacement
        /// </summary>
        /// <param name="sender">Element</param>
        /// <param name="e">Arguement de la souris</param>
        void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (this.IsDraggable)
            {
                if ((isLayoutRootMouseCapture) && (rootTranslateTransform != null))
                {
                    rootTranslateTransform.X = e.GetPosition(this).X - this.ClickPosition.X;
                    rootTranslateTransform.Y = e.GetPosition(this).Y - this.ClickPosition.Y;

                    this.Position = new Point(rootTranslateTransform.X, rootTranslateTransform.Y);
                }
            }
        }

        /// <summary>
        /// Gestion de Bouton Up
        /// </summary>
        /// <param name="sender">Element</param>
        /// <param name="e">Arguement de la souris</param>
        void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (this.IsDraggable)
            {
                this.moveabledElement.ReleaseMouseCapture();
                isLayoutRootMouseCapture = false;
                this.Position = new Point(rootTranslateTransform.X, rootTranslateTransform.Y);

                this.OnUnSelectEvent(new MoveableContentEventArgs());
            }
        }

        /// <summary>
        /// Gestion de Bouton Down
        /// </summary>
        /// <param name="sender">Element</param>
        /// <param name="e">Arguement de la souris</param>
        void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (e != null)
                {
                    this.ClickPosition = e.GetPosition(sender as UIElement);
                }
                if (this.IsDraggable)
                {
                    this.moveabledElement.CaptureMouse();
                    isLayoutRootMouseCapture = true;
                    this.moveabledElement.MouseLeftButtonUp += new MouseButtonEventHandler(this.OnMouseLeftButtonUp);
                    this.moveabledElement.MouseMove += new MouseEventHandler(this.OnMouseMove);
                }
                if (e != null)
                {
                    this.OnSelectEvent(new MoveableContentEventArgs());
                }
            }
            catch (ArgumentException)
            {
                // Traitement effectué à la suppression de l'objet
                // Erreur retournée par e.GetPosition(sender as UIElement);
            }
           
        } 

        #endregion
    }
}
