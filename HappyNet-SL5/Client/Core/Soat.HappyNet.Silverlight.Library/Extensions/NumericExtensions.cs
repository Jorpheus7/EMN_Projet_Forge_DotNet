// -----------------------------------------------------------------------
// <copyright file="NumericExtensions.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------


namespace Soat.HappyNet.Silverlight.Library.Extensions
{
    public static class NumericExtensions
    {
        /// <summary>
        /// Teste si un double n'a pas de valeur non définie (infini ou NaN)
        /// </summary>
        /// <param name="d">Double à tester</param>
        /// <returns>Retourne vrai si l'argument n'est ni infini, ni NaN et faux sinon</returns>
        public static bool IsDouble(this double d)
        {
            return (!double.IsNaN(d) && !double.IsInfinity(d));
        }

        public static bool IsInt(string s)
        {
            int i;
            return int.TryParse(s, out i);
        }
    }
}
