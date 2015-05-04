// -----------------------------------------------------------------------
// <copyright file="Type.Extensions.cs" company="So@t">
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
using System.Collections.Generic;

namespace Soat.HappyNet.Silverlight.Library.Extensions
{
    /// <summary>
    /// Cette classe contient les méthodes d'extentions sur les Types Type
    /// </summary>
    public static class TypeExtensions
    {
        #region GetAllBaseTypes Extension Method

        /// <summary>
        /// Récupère l'ensemble des type de base à partir d'un type
        /// </summary>
        /// <param name="type">Type</param>
        /// <returns>Liste des tous les types de base</returns>
        public static IList<Type> GetAllBaseTypes(this Type type)
        {
            var t = type.BaseType;
            var typeList = new List<Type>();
            while (t != null)
            {
                typeList.Add(t);
                t = t.BaseType;
            }

            return typeList;
        } 

        #endregion

        #region InheritsType Extension Method

        /// <summary>
        /// Vérifie si un type a pour type de base un certain Type
        /// </summary>
        /// <param name="type">Type </param>
        /// <param name="baseTypeToCheck">Type de base à véirifer</param>
        /// <returns>Retourne vrai si le type herite du type de base</returns>
        public static bool InheritsType(this Type type, Type baseTypeToCheck)
        {
            if (type.GetAllBaseTypes().Contains(baseTypeToCheck))
            {
                return true;
            }
            return false;
        } 

        #endregion
    }
}
