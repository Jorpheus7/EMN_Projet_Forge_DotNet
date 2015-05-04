// -----------------------------------------------------------------------
// <copyright file="PrismExtensions.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Presentation.Events;
using Microsoft.Practices.Composite.Regions;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Soat.HappyNet.Silverlight.Library.Extensions
{
    public static class PrismExtensions
    {
        #region EventAggregator

        public static void Publish<TMessageType>(
            this IEventAggregator aggregator, TMessageType message)
              where TMessageType : CompositePresentationEvent<TMessageType> 
        {
            aggregator.GetEvent<TMessageType>().Publish(message);
        }

        public static void Subscribe<TMessageType>(
          this IEventAggregator aggregator, Action<TMessageType> action)
            where TMessageType : CompositePresentationEvent<TMessageType>
        {
            //aggregator.GetEvent<TMessageType>().Subscribe(action, ThreadOption.PublisherThread, false, Dummy);
            aggregator.GetEvent<TMessageType>().Subscribe(action);
        }

        //public static bool Dummy(object dummy) 
        //{
        //    return true;
        //}

        public static void Unsubscribe<TMessageType>(
          this IEventAggregator aggregator, Action<TMessageType> action)
            where TMessageType : CompositePresentationEvent<TMessageType>
        {
            aggregator.GetEvent<TMessageType>().Unsubscribe(action);
        }
        #endregion

        #region RegionManager

        public static IRegionManager RegisterViewWithCleanedRegion(this IRegionManager regionManager, 
            string regionName, 
            Func<object> getContentDelegate)
        {
            return RegisterViewWithCleanedRegion(regionManager, regionName, null, getContentDelegate);
        }

        public static IRegionManager RegisterViewWithCleanedRegion(this IRegionManager regionManager,
            string regionName,
            object oldContent,
            Func<object> getContentDelegate)
        {
            if (oldContent != null)
            {
                oldContent.RemoveViewFromRegion(regionManager, regionName);
                if (oldContent is IDisposable)
                    ((IDisposable)oldContent).Dispose();
            }

            regionManager.RegisterViewWithRegion(regionName, () =>
            {
                var view = getContentDelegate();
                return view.RemoveViewFromRegion(regionManager, regionName);
            });

            return regionManager;
        }

        public static IRegionManager RegisterViewWithCleanedRegion(this IRegionManager regionManager, string regionName, Type viewType)
        {
            return regionManager;
        }

        /// <summary>
        /// Cette méthode supprime la vue de son parent graphique (la région)
        /// </summary>
        /// <param name="view">Element voulant se séparer de son parent</param>
        /// <returns>view</returns>
        public static object RemoveViewFromRegion(this object view)
        {
            var removeObject = view as DependencyObject;
            if (removeObject != null)
            {
                removeObject.RemoveFromParentPanel();
            }

            return view;
        }

        /// <summary>
        /// Cette méthode supprime la vue de son parent graphique (la région)
        /// </summary>
        /// <param name="view">Element voulant se séparer de son parent</param>
        /// <param name="region">Région dont on veut supprimer toutes les vues</param>
        /// <returns>view</returns>
        public static object RemoveViewFromRegion(this object view, IRegion region)
        {
            region.ClearViews();
            RemoveViewFromRegion(view);

            return view;
        }

        /// <summary>
        /// Cette méthode supprime la vue de son parent graphique (la région)
        /// </summary>
        /// <param name="view">Element voulant se séparer de son parent</param>
        /// <param name="regionManager">RegionManager</param>
        /// <param name="regionName">Nom de région</param>
        /// <returns>view</returns>
        public static object RemoveViewFromRegion(this object view, IRegionManager regionManager, string regionName)
        {
            if (regionManager.Regions.ContainsRegionWithName(regionName))
            {
                var region = regionManager.Regions[regionName];
                region.ClearViews();
                regionManager.Regions.Remove(regionName);
            }
            
            RemoveViewFromRegion(view);

            return view;
        }

        public static void ClearViews(this IRegion region)
        {
            List<object> listeDelete = new List<object>();
            listeDelete.AddRange(region.Views);
            foreach (var view in listeDelete)
            {
                region.Remove(view);
            }
        }

        public static void ClearViews(this IRegionManager regionManager, string regionName)
        {
            if (regionManager.Regions.ContainsRegionWithName(regionName))
            {
                regionManager.Regions[regionName].ClearViews();
            }
        }

        /// <summary>
        /// Ajoute une vue à une région et l'active
        /// </summary>
        /// <typeparam name="T">Type de la vue</typeparam>
        /// <param name="region">Région</param>
        /// <param name="view">Vue à ajouter</param>
        public static void AddAndActivate<T>(this IRegion region, T view)
        {
            region.AddAndActivate(view, true);
        }

        /// <summary>
        /// Ajoute une vue à une région et l'active
        /// </summary>
        /// <typeparam name="T">Type de la vue</typeparam>
        /// <param name="region">Région</param>
        /// <param name="view">Vue à ajouter</param>
        /// <param name="clearViewBefore">Nettoie la vue avant l'ajout</param>
        public static void AddAndActivate<T>(this IRegion region, T view, bool clearViewBefore)
        {
            if (view == null || region == null)
                return;

            if (clearViewBefore)
            {
                region.ClearViews();
                region.Add(view);
                region.Activate(view);
            }
            else
            {
                if (!region.ActiveViews.Contains(view))
                {
                    if (!region.Views.Contains(view))
                    {
                        region.Add(view);
                    }
                    region.Activate(view);
                }
            }
        }

        /// <summary>
        /// Ajoute une vue à une région et l'active
        /// Si la région contient déjà une instance du même type que la vue, 
        /// on active cette instance déjà présente
        /// </summary>
        /// <typeparam name="T">Type de la vue</typeparam>
        /// <param name="region">Région</param>
        /// <param name="view">Méthode instanciant la vue à ajouter</param>
        public static void AddOrActivateIfTypeFound<T>(this IRegion region, Func<T> instanciateView)
        {
            if (region == null)
                return;

            foreach (var cachedView in region.Views)
            {
                if (cachedView is T)
                {
                    region.Activate(cachedView);
                    return;
                }
            }

            region.AddAndActivate<T>(instanciateView());
        }

        public static bool ActivateIfTypeFound<T>(this IRegion region)
        {
            if (region == null)
                return false;

            foreach (var cachedView in region.Views)
            {
                if (cachedView is T)
                {
                    region.Activate(cachedView);
                    return true;
                }
            }
            return false;
        }

        #endregion
    }
}
