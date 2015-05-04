// -----------------------------------------------------------------------
// <copyright file="BindingHelper.cs" company="So@t">
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
using System.ComponentModel;
using System.Windows.Data;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

// http://www.scottlogic.co.uk/blog/colin/2009/02/relativesource-binding-in-silverlight/

namespace Soat.HappyNet.Silverlight.Library.Helpers
{
    public class BindingProperties
    {
        public string SourceProperty { get; set; }
        public string ElementName { get; set; }
        public string TargetProperty { get; set; }
        public IValueConverter Converter { get; set; }
        public object ConverterParameter { get; set; }
        public bool RelativeSourceSelf { get; set; }
        public string RelativeSourceAncestorType { get; set; }
        public int RelativeSourceAncestorLevel { get; set; }

        public BindingProperties()
        {
            RelativeSourceAncestorLevel = 1;
        }
    }

    public static class BindingHelper
    {
        public class ValueObject : INotifyPropertyChanged
        {
            //private object _value;

            WeakReference valueRef;
            public object Value
            {
                get 
                {
                    if (valueRef != null)
                        return valueRef.Target;

                    return null; 
                }
                set
                {
                    //_value = value;
                    valueRef = new WeakReference(value);
                    OnPropertyChanged("Value");
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }
        }

        public static BindingProperties GetBinding(DependencyObject obj)
        {
            return (BindingProperties)obj.GetValue(BindingProperty);
        }

        public static void SetBinding(DependencyObject obj, BindingProperties value)
        {
            obj.SetValue(BindingProperty, value);
        }

        public static readonly DependencyProperty BindingProperty =
            DependencyProperty.RegisterAttached("Binding", typeof(BindingProperties), typeof(BindingHelper),
            new PropertyMetadata(null, OnBinding));


        /// <summary>
        /// property change event handler for BindingProperty
        /// </summary>
        private static void OnBinding(
            DependencyObject depObj, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement targetElement = depObj as FrameworkElement;

            targetElement.Loaded += new RoutedEventHandler(TargetElement_Loaded);
        }

        private static void TargetElement_Loaded(object sender, RoutedEventArgs e)
        {
            FrameworkElement targetElement = sender as FrameworkElement;

            targetElement.Loaded -= new RoutedEventHandler(TargetElement_Loaded);

            // get the value of our attached property
            BindingProperties bindingProperties = GetBinding(targetElement);

            if (bindingProperties.ElementName != null)
            {
                // perform our 'ElementName' lookup
                FrameworkElement sourceElement = targetElement.FindName(bindingProperties.ElementName) as FrameworkElement;

                // bind them
                CreateRelayBinding(targetElement, sourceElement, bindingProperties);
            }
            else if (bindingProperties.RelativeSourceSelf)
            {
                // bind an element to itself.
                CreateRelayBinding(targetElement, targetElement, bindingProperties);
            }
            else if (!string.IsNullOrEmpty(bindingProperties.RelativeSourceAncestorType))
            {
                
                // navigate up the tree to find the type
                DependencyObject currentObject = targetElement;

                int currentLevel = 0;
                while (currentLevel < bindingProperties.RelativeSourceAncestorLevel)
                {
                    do
                    {
                        currentObject = VisualTreeHelper.GetParent(currentObject);
                    }
                    while (currentObject.GetType().Name != bindingProperties.RelativeSourceAncestorType);
                    currentLevel++;
                }

                FrameworkElement sourceElement = currentObject as FrameworkElement;

                // bind them
                CreateRelayBinding(targetElement, sourceElement, bindingProperties);
            }
        }

        private static readonly BindingFlags dpFlags = BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy;

        private struct RelayBindingKey
        {
            public DependencyProperty dependencyObject;

            WeakReference frameworkElementReference;
            public FrameworkElement frameworkElement
            {
                get
                {
                    if (frameworkElementReference != null)
                        return frameworkElementReference.Target as FrameworkElement;

                    return null;
                }
                set
                {
                    frameworkElementReference = new WeakReference(value);
                }
            }
            //public FrameworkElement frameworkElement;
        }

        /// <summary>
        /// A cache of relay bindings, keyed by RelayBindingKey which specifies a property of a specific 
        /// framework element.
        /// </summary>
        private static Dictionary<RelayBindingKey, ValueObject> relayBindings = new Dictionary<RelayBindingKey, ValueObject>();

        /// <summary>
        /// Creates a relay binding between the two given elements using the properties and converters
        /// detailed in the supplied bindingProperties.
        /// </summary>
        private static void CreateRelayBinding(FrameworkElement targetElement, FrameworkElement sourceElement,
            BindingProperties bindingProperties)
        {

            string sourcePropertyName = bindingProperties.SourceProperty + "Property";
            string targetPropertyName = bindingProperties.TargetProperty + "Property";

            // find the source dependency property
            FieldInfo[] sourceFields = sourceElement.GetType().GetFields(dpFlags);
            FieldInfo sourceDependencyPropertyField = sourceFields.First(i => i.Name == sourcePropertyName);
            DependencyProperty sourceDependencyProperty = sourceDependencyPropertyField.GetValue(null) as DependencyProperty;

            // find the target dependency property
            FieldInfo[] targetFields = targetElement.GetType().GetFields(dpFlags);
            FieldInfo targetDependencyPropertyField = targetFields.First(i => i.Name == targetPropertyName);
            DependencyProperty targetDependencyProperty = targetDependencyPropertyField.GetValue(null) as DependencyProperty;


            ValueObject relayObject;
            bool relayObjectBoundToSource = false;

            // create a key that identifies this source binding
            RelayBindingKey key = new RelayBindingKey()
            {
                dependencyObject = sourceDependencyProperty,
                frameworkElement = sourceElement
            };

            // do we already have a binding to this property?
            if (relayBindings.ContainsKey(key))
            {
                relayObject = relayBindings[key];
                relayObjectBoundToSource = true;
            }
            else
            {
                // create a relay binding between the two elements
                relayObject = new ValueObject();
            }
            

            // initialise the relay object with the source dependency property value 
            relayObject.Value = sourceElement.GetValue(sourceDependencyProperty);

            // create the binding for our target element to the relay object, this binding will
            // include the value converter
            Binding targetToRelay = new Binding();
            targetToRelay.Source = relayObject;
            targetToRelay.Path = new PropertyPath("Value");
            targetToRelay.Mode = BindingMode.TwoWay;
            targetToRelay.Converter = bindingProperties.Converter;
            targetToRelay.ConverterParameter = bindingProperties.ConverterParameter;

            // set the binding on our target element
            targetElement.SetBinding(targetDependencyProperty, targetToRelay);
            
            if (!relayObjectBoundToSource)
            {
                // create the binding for our source element to the relay object
                Binding sourceToRelay = new Binding();
                sourceToRelay.Source = relayObject;
                sourceToRelay.Path = new PropertyPath("Value");
                sourceToRelay.Mode = BindingMode.TwoWay;

                // set the binding on our source element
                sourceElement.SetBinding(sourceDependencyProperty, sourceToRelay);

                relayBindings.Add(key, relayObject);
            }                            
        }
    }
}
