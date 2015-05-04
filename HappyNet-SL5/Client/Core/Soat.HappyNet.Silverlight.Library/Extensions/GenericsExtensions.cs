// -----------------------------------------------------------------------
// <copyright file="GenericsExtensions.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Soat.HappyNet.Silverlight.Library.Extensions
{
    public static class GenericsExtensions
    {
        /// <summary>
        /// Convertie une IEnumerable en ObservableCollection
        /// </summary>
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> liste)
        {
            ObservableCollection<T> obs = new ObservableCollection<T>();

            foreach (T t in liste)
            {
                obs.Add(t);
            }

            return obs;
        }


        #region Copy des propriétés communes à 2 classes différentes

        /// <summary>
        /// Retourner un nouvel objet de type U à partir des propriétés commune au type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="sender"></param>
        /// <returns></returns>
        public static U Copy<T, U>(this T sender) where U : new()
        {
            // Création d'une instance du type de retour
            U retour = new U();

            // Copy les propriétés de l'orinal vers le retour
            sender.Copy<T, U>(retour);

            return retour;
        }

        /// <summary>
        /// Copie les propriétés du type U à partir des propriétés commune au type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="sender"></param>
        /// <param name="retour"></param>
        public static void Copy<T, U>(this T sender, U retour)
        {
            var req = (
                // Selection des properties de l'objet original
                from s in typeof(T).GetProperties()

                // Selection des properties de l'objet de retour
                from r in typeof(U).GetProperties()

                // Jointure des properties
                where r.Name == s.Name && r.PropertyType == s.PropertyType

                select new { r, s })

                .ToArray();

            // Test si on a bien des donnée
            if (req != null
                && req.Length > 0)
            {
                for (Int32 i = 0; i < req.Length; i++)
                {
                    // 
                    req[i].r.SetValue(
                        retour,
                        req[i].s.GetValue(sender, null),
                        null);
                }
            }
        }

        /// <summary>
        /// Retourner un nouveau tableau d'objet de type U à partir des propriétés commune au tableau du type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="sender"></param>
        /// <returns></returns>
        public static U[] Copy<T, U>(this T[] sender) where U : new()
        {
            // Création d'une instance du type de retour
            U[] retour = new U[sender.Length];
            for (Int32 i = 0; i < sender.Length; i++)
            {
                retour[i] = new U();
            }

            // Copy les propriétés de l'orinal vers le retour
            sender.Copy<T, U>(retour);

            return retour;
        }

        /// <summary>
        /// Copie les propriétés du tableau de type U à partir des propriétés commune au tableau du type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="sender"></param>
        /// <param name="retour"></param>
        public static void Copy<T, U>(this T[] sender, U[] retour)
        {
            var req = (
                // Selection des properties de l'objet original
                from s in typeof(T).GetProperties()

                // Selection des properties de l'objet de retour
                from r in typeof(U).GetProperties()

                // Jointure des properties
                where r.Name == s.Name && r.PropertyType == s.PropertyType

                select new { r, s })

                .ToArray();

            // Test si on a bien des donnée
            if (req != null
                && req.Length > 0)
            {
                for (Int32 i = 0; i < sender.Length; i++)
                {
                    for (Int32 j = 0; j < req.Length; j++)
                    {
                        // 
                        req[j].r.SetValue(
                            retour[i],
                            req[j].s.GetValue(sender[i], null),
                            null);
                    }
                }
            }
        }

        #endregion
    }
}