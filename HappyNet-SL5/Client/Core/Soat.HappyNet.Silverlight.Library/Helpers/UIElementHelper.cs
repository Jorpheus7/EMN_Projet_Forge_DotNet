// -----------------------------------------------------------------------
// <copyright file="UIElementHelper.cs" company="So@t">
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
using System.Globalization;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using Soat.HappyNet.Silverlight.Library.Extensions;

namespace Soat.HappyNet.Silverlight.Library.Helpers
{
    /// <summary>
    /// Cette classe contient l'ensemble des Méthodes utilitaire qui s'applique sur des Elements Graphiques
    /// </summary>
    public static class UIElementHelper
    {
        #region RemoveElementFromParent Method

        /// <summary>
        /// Cette méthode supprime le parent de l'Element
        /// </summary>
        /// <param name="removeObject">Element voulant se séparer de son parent</param>
        public static void RemoveElementFromParent(FrameworkElement removeObject)
        {
            if (removeObject != null)
            {
                if (removeObject.Parent is Panel)
                {
                    ((Panel)removeObject.Parent).Children.Remove(removeObject);
                }
                else if (removeObject.Parent is ItemsControl)
                {
                    ((ItemsControl)removeObject.Parent).Items.Remove(removeObject);
                }
                else if (removeObject.Parent is ContentControl && ((ContentControl)removeObject.Parent).Content is Panel)
                {
                    ((ContentControl)removeObject.Parent).Content = null;
                }
            }
        }

        #endregion

        #region AddElementToParent Method

        /// <summary>
        /// Cette méthode permet l'ajout d'un Element à un autre élément Parent
        /// </summary>
        /// <param name="addObject">Element a Ajouter</param>
        /// <param name="parent">Parent</param>
        internal static void AddElementToParent(FrameworkElement addObject, FrameworkElement parent)
        {
            if (parent is Panel)
            {
                ((Panel)parent).Children.Add(addObject);
            }
            else if (parent is ItemsControl)
            {
                ((ItemsControl)parent).Items.Add(addObject);
            }
            else if (parent is ContentControl)
            {
                ((ContentControl)parent).Content = addObject;
            }
        }

        #endregion

        #region GetZIndexAndSetMaxZIndex Method

        /// <summary>
        /// Cette méthode permet de récupérer le ZIndex maximal d'un Element
        /// </summary>
        /// <param name="element">Element Graphique </param>
        /// <returns>ZIndex Maximal de l'Element</returns>
        internal static int GetZIndexAndSetMaxZIndex(UIElement element)
        {
            var initialZIndex = Convert.ToInt32(element.GetValue(Canvas.ZIndexProperty), CultureInfo.CurrentCulture);
            Canvas.SetZIndex(element, short.MaxValue - 1);

            return initialZIndex;
        }

        #endregion

        #region SelfOrParentVisualsImplementInterface<T> Method

        /// <summary>
        /// Cette méthode générique permeet de vérifier si un des parents d'un Objet implémente une Interface
        /// </summary>
        /// <typeparam name="T">Type d'interface</typeparam>
        /// <param name="objectToCheck">Objet à vérifier</param>
        /// <param name="typeOfInterface">Type d'interface</param>
        /// <param name="implementedInterface">Class qui implément le Type d'interface</param>
        /// <returns></returns>
        internal static bool SelfOrParentVisualsImplementInterface<T>(DependencyObject objectToCheck, Type typeOfInterface, out T implementedInterface) where T : class
        {
            var p = objectToCheck;
            while (p != null)
            {
                if (p.GetType().GetInterfaces().Contains(typeOfInterface))
                {
                    implementedInterface = p as T;
                    return true;
                }

                p = VisualTreeHelper.GetParent(p);
            }

            implementedInterface = null;
            return false;
        }

        #endregion

        #region GetSortedSurfacesByAbsoluteMousePos Method

        /// <summary>
        /// Récupère la liste des Surfaces à partir d'une position absolue de la souris , triée par ordre de ZIndex Décroissant
        /// </summary>
        /// <param name="absolutePosition">Position absoulue de la souris</param>
        /// <param name="possibleTargets">Liste des panels possibles</param>
        /// <returns>Liste des Panels trouvés aux coordonnées absolues de la souris</returns>
        internal static Collection<Panel> GetSortedSurfacesByAbsoluteMousePos(Point absolutePosition, IEnumerable<Panel> possibleTargets)
        {
            if (possibleTargets == null) return null;

            var hitPos = absolutePosition;
            var allSurfaces = (List<UIElement>)VisualTreeHelper.FindElementsInHostCoordinates(hitPos, Application.Current.RootVisual);

            var targetList = from t in possibleTargets
                             where allSurfaces.Contains(t)
                             orderby t.GetValue(Canvas.ZIndexProperty) descending
                             select t;

            return new Collection<Panel>(targetList.ToList());
        }

        #endregion

        #region GetHighestSurfaceTarget Method

        /// <summary>
        /// Récupère la Surfaces Panel le plus haut (ZIndex) à partir d'une position absolue de la souris
        /// </summary>
        /// <param name="absolutePosition">Position absoulue de la souris</param>
        /// <param name="possibleTargets">Liste des panels possibles</param>
        /// <returns>Panel le plus Haut (ZIndex)</returns>
        public static Panel GetHighestSurfaceTarget(Point absolutePosition, IEnumerable<Panel> possibleTargets)
        {
            var dropTargets = GetSortedSurfacesByAbsoluteMousePos(absolutePosition, possibleTargets);

            Panel dropTarget = null;
            if (dropTargets != null && dropTargets.Count > 0)
            {
                dropTarget = dropTargets[0];
            }

            return dropTarget;
        }

        #endregion

        #region GetChild<T> Methods

        /// <summary>
        /// Cette méthode permet de récupérer un type d'élément par son type et son nom
        /// </summary>
        /// <typeparam name="T">Type d'élément graphique</typeparam>
        /// <param name="obj">Elément graphique contenant l'enfant recherché</param>
        /// <param name="name">Nom de l'enfant</param>
        /// <returns>Enfant trouvé</returns>
        public static T GetChild<T>(FrameworkElement obj, string name) where T : FrameworkElement
        {
            FrameworkElement child = null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                child = VisualTreeHelper.GetChild(obj, i) as FrameworkElement;
                if (child != null && (child.GetType() == typeof(T) || child.GetType().InheritsType(typeof(T))) && child.Name.Equals(name))
                {

                    break;
                }
                else if (child != null)
                {
                    child = GetChild<T>(child, name);
                    if (child != null && (child.GetType() == typeof(T) || child.GetType().InheritsType(typeof(T))) && child.Name.Equals(name))
                    {
                        break;
                    }
                }
            }
            return child as T;
        }

        /// <summary>
        /// Cette méthode permet de récupérer un type d'élément par son type 
        /// </summary>
        /// <typeparam name="T">Type d'élément graphique</typeparam>
        /// <param name="obj">Elément graphique contenant l'enfant recherché</param>
        /// <returns>Enfant trouvé</returns>
        public static T GetChild<T>(FrameworkElement obj) where T : FrameworkElement
        {
            FrameworkElement child = null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                child = VisualTreeHelper.GetChild(obj, i) as FrameworkElement;
                if (child != null && child is T)
                {

                    break;
                }
                else if (child != null)
                {
                    child = GetChild<T>(child);
                    if (child != null && (child.GetType() == typeof(T) || child.GetType().InheritsType(typeof(T))))
                    {
                        break;
                    }
                }
            }
            return child as T;
        }

        #endregion

        #region GetChildsRecursive Method

        /// <summary>
        /// Cette méthode permet de récupérer la liste de tous les enfants d'un objet
        /// </summary>
        /// <param name="root">Root de la recherche</param>
        /// <returns>Liste des enfants</returns>
        public static IEnumerable<DependencyObject> GetChildsRecursive(DependencyObject root)
        {
            List<DependencyObject> elts = new List<DependencyObject>();
            elts.Add(root);
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(root); i++)
            {
                elts.AddRange(GetChildsRecursive(VisualTreeHelper.GetChild(root, i)));
            }
            return elts;
        }

        #endregion

        #region GetColorFromHex Method
		/// <summary>
        /// Convertit un Hexadécimal en SolidColorBrush (couleur)
        /// </summary>
        /// <param name="myColor"></param>
        /// <returns></returns>
        public static SolidColorBrush GetColorFromHex(string myColor)
        {
            return new SolidColorBrush(
                Color.FromArgb(
                    Convert.ToByte(myColor.Substring(1, 2), 16),
                    Convert.ToByte(myColor.Substring(3, 2), 16),
                    Convert.ToByte(myColor.Substring(5, 2), 16),
                    Convert.ToByte(myColor.Substring(7, 2), 16)
                )
            );
        } 

	    #endregion
    }
}
