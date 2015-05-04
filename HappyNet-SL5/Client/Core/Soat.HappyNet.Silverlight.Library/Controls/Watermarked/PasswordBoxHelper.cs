// -----------------------------------------------------------------------
// <copyright file="PasswordBoxHelper.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System.Windows;
using System.Windows.Controls;

namespace Soat.HappyNet.Silverlight.Library.Controls
{
    /// <summary>
    /// Provide attached property to enable DataBinding on PasswordBox
    /// </summary>
    public class PasswordHelper : DependencyObject
    {

        /// <summary>
        /// Indicates if a PasswordBox is DataBound
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool GetIsBound(PasswordBox obj)
        {
            return (bool)obj.GetValue(IsBoundProperty);
        }

        private static void SetIsBound(PasswordBox obj, bool value)
        {
            obj.SetValue(IsBoundProperty, value);
        }

        public static readonly DependencyProperty IsBoundProperty =
            DependencyProperty.RegisterAttached("IsBound", typeof(bool),
            typeof(PasswordHelper),
            new PropertyMetadata(false));


        /// <summary>
        /// Text of the PasswordBox (bindable vision of Password property)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetText(PasswordBox obj)
        {
            return (string)obj.GetValue(TextProperty);
        }

        public static void SetText(PasswordBox obj, string value)
        {
            obj.SetValue(TextProperty, value);
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.RegisterAttached("Text", typeof(string),
            typeof(PasswordHelper),
            new PropertyMetadata(string.Empty,
                (sender, args) =>
                {
                    var pwdBox = sender as PasswordBox;
                    if (pwdBox == null)
                        return;
                    var nval = (args.NewValue as string) ?? string.Empty;
                    if (pwdBox.Password != nval)
                        pwdBox.Password = nval;
                    if (!GetIsBound(pwdBox))
                    {
                        pwdBox.PasswordChanged += delegate
                        {
                            SetText(pwdBox, pwdBox.Password);
                        };
                        SetIsBound(pwdBox, true);
                    }
                }));
    }

}
