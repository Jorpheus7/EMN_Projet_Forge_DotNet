// -----------------------------------------------------------------------
// <copyright file="DependencyObjectExtensions.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Soat.HappyNet.Silverlight.Library.Extensions
{
    public static class DependencyObjectExtensions
    {
        /// <summary>
        /// Récupère le premier objet parent du type demandé
        /// </summary>
        /// <param name="element">Elément dont on souhaite regarder les parents</param>
        /// <param name="type">Type à rechercher</param>
        /// <returns>Renvoie l'objet trouvé, null sinon</returns>
        public static T GetFirstParentOfType<T>(this DependencyObject element) where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(element) as DependencyObject;
            while (parent != null)
            {
                T result = parent as T;
                if (result != null)
                {
                    return result as T;
                }
                parent = VisualTreeHelper.GetParent(parent) as DependencyObject;
            }

            return null;
        }

        /// Récupère le premier objet parent du type demandé
        /// </summary>
        /// <param name="element">Elément dont on souhaite regarder les parents</param>
        /// <param name="type">Liste de types à rechercher</param>
        /// <returns>Renvoie l'objet trouvé, null sinon</returns>
        public static DependencyObject GetFirstParentOfType(this DependencyObject element, List<Type> type)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(element) as DependencyObject;
            while (parent != null)
            {
                if (type.Contains(parent.GetType()))
                {
                    return parent;
                }
                parent = VisualTreeHelper.GetParent(parent) as DependencyObject;
            }

            return null;
        }

        /// <summary>
        /// Récupère le premier enfant du type spécifié
        /// </summary>
        /// <typeparam name="T">Type à rechercher</typeparam>
        /// <param name="dpob">Objet à parcourir</param>
        /// <returns></returns>
        /// <remarks>Attention : la méthode parcourt tous les enfants de l'arbre visuel, méthode coûteuse donc</remarks>
        public static T GetObjectOfTypeInVisualTree<T>(this DependencyObject dpob) where T : DependencyObject
        {
            int count = VisualTreeHelper.GetChildrenCount(dpob);
            for (int i = 0; i < count; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(dpob, i);
                T childAsT = child as T;
                if (childAsT != null)
                {
                    return childAsT;
                }
                childAsT = GetObjectOfTypeInVisualTree<T>(child);
                if (childAsT != null)
                {
                    return childAsT;
                }
            }
            return null;
        }

        /// <summary>
        /// Savoir si un élément est parent
        /// </summary>
        /// <param name="target"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        public static bool IsInVisualTreeOf(this DependencyObject target, DependencyObject element)
        {
            if (target == element)
            {
                return true;
            }
            DependencyObject parent = VisualTreeHelper.GetParent(element) as DependencyObject;

            while (parent != null)
            {
                if (parent == target)
                {
                    return true;
                }
                parent = VisualTreeHelper.GetParent(parent) as DependencyObject;
            }
            return false;
        }

        /// <summary>
        /// Détermine si un élément est dans l'arbre visuel de l'application courante
        /// </summary>
        /// <param name="element">Element</param>
        /// <returns>
        /// 	<c>vrai</c> si l'élément fait parti de l'arbre visuel, <c>faux</c> sinon.
        /// </returns>
        public static bool IsInVisualTree(this DependencyObject element)
        {
            return IsInVisualTreeOf(Application.Current.RootVisual as DependencyObject, element);
        }

        /// <summary>
        /// Retire l'élément 
        /// </summary>
        /// <param name="element"></param>
        public static bool RemoveFromParentPanel(this DependencyObject element)
        {
            UIElement removeObject = element as UIElement;

            if (removeObject != null)
            {
                DependencyObject parent = VisualTreeHelper.GetParent(removeObject) as Panel;
                if (parent == null && removeObject is FrameworkElement)
                    parent = ((FrameworkElement)removeObject).Parent;

                if (parent is Panel)
                {
                    ((Panel)parent).Children.Remove(removeObject);
                    return true;
                }
                else if (parent is ItemsControl)
                {
                    ((ItemsControl)parent).Items.Remove(removeObject);
                    return true;
                }
                else if (parent is ContentControl) // && ((ContentControl)removeObject.Parent).Content is Panel)
                {
                    ((ContentControl)parent).Content = null;
                    return true;
                }
            }

            return false;
        }
    }
}
