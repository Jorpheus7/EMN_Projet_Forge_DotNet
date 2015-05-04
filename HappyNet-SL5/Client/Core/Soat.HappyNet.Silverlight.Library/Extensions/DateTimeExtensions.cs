// -----------------------------------------------------------------------
// <copyright file="DateTimeExtensions.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;

namespace Soat.HappyNet.Silverlight.Library.Extensions
{
    /// <summary>
    /// Extension des DateTime
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Vérifie si une date est sur un intervalle
        /// </summary>
        /// <param name="date">Date à vérifier</param>
        /// <param name="start">Début de l'intervalle</param>
        /// <param name="end">Fin de l'intervalle</param>
        /// <returns>Vrai si la date est comprise dans l'intervalle, faux sinon</returns>
        public static bool IsBetween(this DateTime date, DateTime start, DateTime end)
        {
            return date.CompareTo(start) >= 0 && date.CompareTo(end) <= 0;
        }

        /// <summary>
        /// Formatte le TimeSpan
        /// </summary>
        /// <param name="time">TimeSpan à formatter</param>
        /// <returns>Texte formatté</returns>
        public static string ToLongString(this TimeSpan time)
        {
            return ToLongString(time, null, null);
        }

        /// <summary>
        /// Formatte le TimeSpan
        /// </summary>
        /// <param name="time">TimeSpan à formatter</param>
        /// <param name="separator">Séparateur pour chaque donnée temporelle</param>
        /// <returns>Texte formatté</returns>
        public static string ToLongString(this TimeSpan time, string separator)
        {
            return ToLongString(time, null, separator);
        }

        /// <summary>
        /// Formatte le TimeSpan
        /// </summary>
        /// <param name="time">TimeSpan à formatter</param>
        /// <param name="hideparts">Parties que l'on ne souhaite pas afficher ("d" : jours, "h" : heures, "m" : minutes, "s" : secondes)</param>
        /// <returns>Texte formatté</returns>
        public static string ToLongString(this TimeSpan time, IList<string> hideparts, string separator)
        {
            StringBuilder sb = new StringBuilder();
            if (hideparts == null)
                hideparts = new List<string>();

            if (!hideparts.Contains("d")
                && time.Days != 0)
            {
                sb.Append(Math.Abs(time.Days).ToString("00"));
                if (separator == null)
                    sb.Append("j");
                else
                    sb.Append(separator);
            }

            if (!hideparts.Contains("h")
                && (time.Days != 0 || time.Hours != 0))
            {
                sb.Append(Math.Abs(time.Hours).ToString("00"));
                if (separator == null)
                    sb.Append("h");
                else
                    sb.Append(separator);
            }

            if (!hideparts.Contains("m"))
            //&& (time.Days != 0 || time.Hours != 0 || time.Minutes != 0))
            {
                sb.Append(Math.Abs(time.Minutes).ToString("00"));
                if (separator == null)
                    sb.Append("min");
                else
                    sb.Append(separator);
            }

            if (!hideparts.Contains("s"))
            {
                sb.Append(Math.Abs(time.Seconds).ToString("00"));
                if (separator == null)
                    sb.Append("sec");
                else
                    sb.Append(separator);
            }

            string result = sb.ToString().Trim(); ;
            if (separator != null)
                result = result.Substring(0, sb.ToString().LastIndexOf(separator));

            return string.Format("{0}{1}", time.Ticks < 0 ? "-" : "", result);
        }
    }
}