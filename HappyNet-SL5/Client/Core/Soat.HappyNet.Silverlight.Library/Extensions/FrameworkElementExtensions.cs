// -----------------------------------------------------------------------
// <copyright file="FrameworkElementExtensions.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Soat.HappyNet.Silverlight.Library.Extensions
{
    /// <summary>
    /// <see cref="FrameworkElement"/> Extension des méthodes du FrameworkElement
    /// </summary>
    public static class FrameworkElementExtensions
    {
        /// <summary>
        /// Renvoie si un contrôle est visible ou non
        /// </summary>
        public static bool IsVisible(this UIElement element)
        {
            if (element == null)
                return false;

            return element.Visibility == Visibility.Visible;
        }

        public static void SetVisibility(this UIElement element, bool visibility)
        {
            if (element != null)
            {
                element.Visibility = visibility ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Récupère les limites de l'élément
        /// </summary>
        /// <param name="e">Elément</param>
        /// <returns>Rectangle représentant les limites de l'élément</returns>
        public static Rect GetBounds(this FrameworkElement e)
        {
            GeneralTransform tx = e.TransformToVisual(Application.Current.RootVisual);
            Point loc = tx.Transform(new Point(0, 0));
            return new Rect(loc.X, loc.Y, e.ActualWidth, e.ActualHeight);
        }

        /// <summary>
        /// Teste si le curseur de la souris se situe au-dessus d'un élément
        /// </summary>
        /// <param name="mousePos">Position de la souris</param>
        /// <param name="element">Elément à tester</param>
        /// <returns>Vrai si la souris se trouve au-dessus de l'élement, faux sinon</returns>
        /// <remarks>La détection se fait par rapport au référentiel du ParentCanvas</remarks>
        public static bool IsMouseOverRectangle(this FrameworkElement element, Point mousePos)
        {
            return element.IsMouseOverRectangle(mousePos, new Thickness(0));
        }

        /// <summary>
        /// Teste si le curseur de la souris se situe au-dessus d'un élément
        /// </summary>
        /// <param name="mousePos">Position de la souris</param>
        /// <param name="element">Elément à tester</param>
        /// <param name="offset">Décalage à prévoir sur les bords</param>
        /// <returns>Vrai si la souris se trouve au-dessus de l'élement, faux sinon</returns>
        /// <remarks>La détection se fait par rapport au référentiel du ParentCanvas</remarks>
        public static bool IsMouseOverRectangle(this FrameworkElement element, Point mousePos, Thickness offset)
        {
            if (element != null)
            {
                GeneralTransform transform = Application.Current.RootVisual.TransformToVisual(element);
                Point relativePos = transform.Transform(mousePos);

                if (relativePos.X < offset.Left || relativePos.Y < offset.Top || relativePos.X > element.ActualWidth - offset.Right || relativePos.Y > element.ActualHeight - offset.Bottom)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        #region Transforms
        /// <summary>
        /// Cherche un Transform, s'il ne le trouve pas, il le créé
        /// </summary>
        /// <param name="target"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static T FindTransform<T>(this UIElement target) where T : Transform
        {
            TransformGroup tg = target.RenderTransform as TransformGroup;

            if (tg != null)
            {
                if (tg.Children.Count > 0)
                {
                    for (int i = 0; i < tg.Children.Count; i++)
                    {
                        if (tg.Children[i].GetType() is T)
                        {
                            return (T)tg.Children[i];
                        }
                    }
                }
            }
            else if (target.RenderTransform.GetType() is T)
            {
                return (T)target.RenderTransform;
            }
            else
            {
                tg = new TransformGroup();
            }

            Transform trans = null;

            if (typeof(T) == typeof(TranslateTransform))
            {
                trans = new TranslateTransform();
            }
            else if (typeof(T) == typeof(ScaleTransform))
            {
                trans = new ScaleTransform();
            }
            else if (typeof(T) == typeof(RotateTransform))
            {
                trans = new RotateTransform();
            }
            else if (typeof(T) == typeof(SkewTransform))
            {
                trans = new SkewTransform();
            }

            if (trans != null)
            {
                tg.Children.Add(trans);
                target.RenderTransform = tg;
            }

            return (T)trans;
        }

        /// <summary>
        /// Cherche un TransformGroup, s'il ne le trouve pas, il le créé
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static TransformGroup FindTransformGroup(this UIElement target)
        {
            if (target.RenderTransform as TransformGroup != null)
            {
                return (TransformGroup)target.RenderTransform;
            }
            // if TransformGroup doesn't exist then return a new one
            return new TransformGroup();
        }
        #endregion

        public static object FindNameRecursive(this FrameworkElement element, string name)
        {
            FrameworkElement rootVisual = element.GetRootVisual();
            if (rootVisual == null)
            {
                return null;
            }

            return rootVisual.FindName(name);
        }

        public static object FindRecursive(this FrameworkElement element, Type type)
        {
            while (element != null)
            {
                element = element.GetParentVisual();

                if ((element != null) && type.IsAssignableFrom(element.GetType()))
                {
                    return element;
                }
            }

            return null;
        }

        public static object FindResource(this FrameworkElement element, string key)
        {
            while (element != null)
            {
                object value = element.Resources[key];
                if (value != null)
                {
                    return value;
                }

                element = element.GetParentVisual();
            }

            return null;
        }

        public static FrameworkElement GetParentVisual(this FrameworkElement element)
        {
            return VisualTreeHelper.GetParent(element) as FrameworkElement;
        }

        public static FrameworkElement GetRootVisual(this FrameworkElement element)
        {
            FrameworkElement parent = null;
            while (element != null)
            {
                parent = element;
                if (parent is UserControl)
                {
                    // HACK: A UserControl parented to another UserControl has a non-null
                    //       parent; however we want to consider the UserControl as the
                    //       root visual for its contents...
                    break;
                }

                element = element.Parent as FrameworkElement;
            }

            return parent;
        }

        /// <summary>
        /// Helper to attempt to detect if an element is loaded or not.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static bool IsLoaded(this FrameworkElement element)
        {
#if SILVERLIGHT
            UIElement rootVisual = Application.Current.RootVisual;
            DependencyObject parent = element.Parent;
            if (parent == null)
            {
                parent = VisualTreeHelper.GetParent(element);
            }
            return ((parent != null) || ((rootVisual != null) && (element == rootVisual)));
#else
			return element.IsLoaded;
#endif
        }
    }
}