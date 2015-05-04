// -----------------------------------------------------------------------
// <copyright file="GeneralHelper.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Shapes;
using System.Windows.Markup;

namespace Soat.HappyNet.Silverlight.Library.Helpers
{
    /// <summary>
    /// Classe générale d'aide au développement
    /// </summary>
    public static class GeneralHelper
    {
        #region Keyboard
        public static bool IsAltDown()
        {
            return ((Keyboard.Modifiers & ModifierKeys.Alt) == ModifierKeys.Alt);
        }

        public static bool IsShiftDown()
        {
            return ((Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift);
        }

        public static bool IsControlDown()
        {
            return ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control);
        }
        #endregion

        #region Numeric
        static int count;
        /// <summary>
        /// Renvoie un ID unique préfixé
        /// </summary>
        /// <param name="prefix">Préfixe de l'ID</param>
        /// <returns></returns>
        public static string GetUnique(string prefix)
        {
            return prefix + count++;
        }
        #endregion

        #region Double
        public static bool AreClose(double value1, double value2)
        {
            if (value1 == value2)
            {
                return true;
            }
            double num = ((Math.Abs(value1) + Math.Abs(value2)) + 10.0) * 2.2204460492503131E-16;
            double num2 = value1 - value2;
            return ((-num < num2) && (num > num2));
        }

        public static bool AreClose(Point point1, Point point2)
        {
            return (AreClose(point1.X, point2.X) && AreClose(point1.Y, point2.Y));
        }

        public static bool AreClose(Rect rect1, Rect rect2)
        {
            if (rect1.IsEmpty)
            {
                return rect2.IsEmpty;
            }
            return (((!rect2.IsEmpty && AreClose(rect1.X, rect2.X)) && (AreClose(rect1.Y, rect2.Y) && AreClose(rect1.Height, rect2.Height))) && AreClose(rect1.Width, rect2.Width));
        }

        public static bool AreClose(Size size1, Size size2)
        {
            return (AreClose(size1.Width, size2.Width) && AreClose(size1.Height, size2.Height));
        }

        public static int DoubleToInt(double val)
        {
            if (0.0 >= val)
            {
                return (int)(val - 0.5);
            }
            return (int)(val + 0.5);
        }

        public static bool GreaterThan(double value1, double value2)
        {
            return ((value1 > value2) && !AreClose(value1, value2));
        }

        public static bool GreaterThanOrClose(double value1, double value2)
        {
            if (value1 <= value2)
            {
                return AreClose(value1, value2);
            }
            return true;
        }

        public static bool LessThan(double value1, double value2)
        {
            return ((value1 < value2) && !AreClose(value1, value2));
        }

        public static bool LessThanOrClose(double value1, double value2)
        {
            if (value1 >= value2)
            {
                return AreClose(value1, value2);
            }
            return true;
        }
        #endregion

        #region Types & Propriétés
        /// <summary>
        /// Récupère la valeur d'un objet d'après le nom (string) de sa propriété
        /// </summary>
        /// <param name="o">Objet à traiter</param>
        /// <param name="name">Nom de la propriété</param>
        /// <returns>La valeur de la propriété de l'objet, null si la propriété n'est pas trouvée</returns>
        public static object GetObjectProperty(object o, string name)
        {
            Type type = o.GetType();
            if (!string.IsNullOrEmpty(name))
            {
                PropertyInfo pinfo = type.GetProperty(name);
                if (pinfo != null)
                {
                    return pinfo.GetValue(o, null);
                }
            }

            return null;
        }

        /// <summary>
        /// Assigne une valeur à un objet d'après le nom de la propriété de l'objet à assigner
        /// </summary>
        /// <param name="o">Objet à mettre à jour</param>
        /// <param name="name">Nom de la propriété</param>
        /// <param name="value">Valeur à assigner</param>
        /// <returns>Vrai si l'opération s'est bien déroulée, faux sinon</returns>
        public static bool SetObjectProperty(object o, string name, object value)
        {
            Type type = o.GetType();
            if (!string.IsNullOrEmpty(name))
            {
                PropertyInfo pinfo = type.GetProperty(name);
                if (pinfo != null)
                {
                    pinfo.SetValue(o, value, null);
                    return true;
                }
            }

            return false;
        }

        public static object GetObjectMethod(object o, string name, object value)
        {
            object result = null;
            Type type = o.GetType();
            if (!string.IsNullOrEmpty(name))
            {
                PropertyInfo pinfo = type.GetProperty(name);
                if (pinfo != null)
                {
                    MethodInfo mi = pinfo.GetGetMethod();
                    if (mi != null)
                    {
                        result = mi.Invoke(o, new object[0]);
                        return result;
                    }
                }
            }

            return result;
        }
        #endregion

        #region Couleurs
        public static Color GetColorFromString(string colorString)
        {
            Type colorType = (typeof(Colors));
            if (colorType.GetProperty(colorString) != null)
            {
                object color = colorType.InvokeMember(colorString, BindingFlags.GetProperty, null, null, null);
                if (color != null)
                {
                    return (Color)color;
                }
            }
            else
            {
                try
                {
                    Line lne = (Line)XamlReader.Load("<Line xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\" Fill=\"" + colorString + "\" />");
                    return (Color)lne.Fill.GetValue(SolidColorBrush.ColorProperty);
                }
                catch { }
            }
            throw new InvalidCastException("Color not defined");
        }

        public static Color GetColorFromHex(string s)
        {
            if (s.StartsWith("#"))
            {
                s = s.Substring(1);
            }
            byte a = System.Convert.ToByte(s.Substring(0, 2), 16);
            byte r = System.Convert.ToByte(s.Substring(2, 2), 16);
            byte g = System.Convert.ToByte(s.Substring(4, 2), 16);
            byte b = System.Convert.ToByte(s.Substring(6, 2), 16);
            return Color.FromArgb(a, r, g, b);
        }

        public static string GetHexFromColor(Color c)
        {
            return string.Format("#{0}{1}{2}{3}",
                    c.A.ToString("X2"),
                    c.R.ToString("X2"),
                    c.G.ToString("X2"),
                    c.B.ToString("X2"));
        } 
        #endregion

        #region Images
        static public ImageSource StringToImageSource(string s)
        {
            return new BitmapImage(new Uri(s, UriKind.RelativeOrAbsolute));
        }

        public static BitmapImage BytesToBitmapImage(byte[] byteArray)
        {
            MemoryStream ms = new MemoryStream((byte[])byteArray);
            BitmapImage image = new BitmapImage();
            try
            {
                image.SetSource(ms);
            }
            catch
            {
                image = null;
            }
            return image;
        }
        #endregion

        #region Timers
        /// <summary>
        /// Initialise un timer appelant la fonction à intervalles réguliers
        /// </summary>
        /// <param name="callback">Fonction à exécuter à intervalles réguliers</param>
        /// <param name="time">Intervalle de temps du timer</param>
        /// <returns>Renvoie un storyboard</returns>
        public static Storyboard SetTimer(Action<double> callback, TimeSpan time)
        {
            double t = 0;
            Storyboard timer = new Storyboard();
            timer.Duration = time;
            timer.Completed += delegate (Object sender, EventArgs args) { 
                t += time.TotalMilliseconds; 
                callback(t);
                Storyboard thisTimer = (Storyboard)sender;
                thisTimer.Begin();
            };
            timer.Begin();

            return timer;
        }

        /// <summary>
        /// Initialise un timer appelant la fonction à intervalles réguliers
        /// </summary>
        /// <param name="callback">Fonction à exécuter à intervalles réguliers</param>
        /// <param name="time">Intervalle de temps du timer</param>
        /// <returns>Renvoie un storyboard</returns>
        public static Storyboard SetTimer(Action<double, Storyboard> callback, TimeSpan time)
        {
            double t = 0;
            Storyboard timer = new Storyboard();
            timer.Duration = time;
            timer.Completed += delegate (Object sender, EventArgs args) { 
                t += time.TotalMilliseconds; 
                callback(t, timer);
                Storyboard thisTimer = (Storyboard)sender;
                thisTimer.Begin();
            };
            timer.Begin();

            return timer;
        }

        /// <summary>
        /// Initialise un timer appelant la fonction une seule fois
        /// </summary>
        /// <param name="callback">Fonction à exécuter une seule fois</param>
        /// <param name="interval">Intervalle de temps au bout duquel la fonction sera exécutée</param>
        /// <returns>Renvoie un timer</returns>
        public static Storyboard SetTimerOnce(Action callback, TimeSpan time)
        {
            Storyboard timer = new Storyboard();
            timer.Duration = time;
            timer.Completed += delegate (Object sender, EventArgs args) {
                callback();
            };
            timer.Begin();

            return timer;
        }
        #endregion

        #region Collections
        /// <summary>
        /// Convertie une ObservableCollection en Array
        /// </summary>
        public static Array ObservableCollectionToArray<T>(ObservableCollection<T> obs)
        {
            int i = 0;
            int count = obs.Count;
            T[] array = new T[count];

            foreach (T t in obs)
            {
                array[i++] = t;
            }

            return array;
        }
        #endregion

        #region Strings
        /// <summary>
        /// Cherche si un motif se trouve dans une chaîne de caractères
        /// </summary>
        /// <param name="search">Chaîne de caractère dans laquelle chercher</param>
        /// <param name="pattern">Motif à rechercher</param>
        /// <returns>Vrai si le motif est trouvé, faux sinon</returns>
        public static bool SearchPattern (string search, string pattern)
        {
            bool result = false;

            if (pattern != null && search != null)
            {
                if (pattern.Length > 1 && pattern[pattern.Length - 1] == '*')
                {
                    pattern = pattern.Substring(0, pattern.Length - 1);
                    result = search.ToUpper().StartsWith(pattern.ToUpper());
                }
                else
                {
                    result = (search.ToUpper().IndexOf(pattern.ToUpper()) >= 0);
                }
            }

            return result;
        }

        /// <summary>
        /// Cherche si tous les mots du motif (séparés par des espaces) sont contenus dans une string
        /// </summary>
        /// <param name="search">Chaîne de caractère dans laquelle on cherche le motif</param>
        /// <param name="pattern">Motif à rechercher</param>
        /// <returns></returns>
        public static bool SearchSpacedPattern(string search, string pattern)
        {
            string[] patternArray = pattern.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            bool patternFound = true;
            foreach (string s in patternArray)
            {
                patternFound = patternFound && GeneralHelper.SearchPattern(search, s);
            }

            return patternFound;
        }

        /// <summary>
        /// Formatte l'url pour supprimer le "/" final
        /// </summary>
        /// <param name="url">Url à formatter</param>
        /// <returns>Retourne l'url sans "/" final</returns>
        public static string FormatUrl(string url)
        {
            if (url.EndsWith("/"))
                url.Remove(url.Length - 1, 1);

            return url;
        }

        /// <summary>
        /// Met la première lettre en majuscule, et le reste en minuscule
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UcFirst(string str)
        {
            return str.Substring(0, 1).ToUpper() + str.Substring(1);
        }
        #endregion

        #region Events
        public static Delegate AddHandler(object source, string eventName, Delegate handler)
        {
            EventInfo ei = GetEventInfo(source, eventName);

            Delegate eh = Delegate.CreateDelegate(
              ei.EventHandlerType, handler.Target, handler.Method);

            ei.AddEventHandler(source, eh);

            return (eh);
        }

        public static void RemoveHandler(object source, string eventName, Delegate handler)
        {
            EventInfo ei = GetEventInfo(source, eventName);

            Delegate eh = Delegate.CreateDelegate(
              ei.EventHandlerType, handler.Target, handler.Method);

            ei.RemoveEventHandler(source, eh);
        }

        private static EventInfo GetEventInfo(object source, string eventName)
        {
            return (source.GetType().GetEvent(eventName));
        }
        
        #endregion
    }
}
