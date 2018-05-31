using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Runtime.CompilerServices;

namespace RaccoonMsgr.Controls
{
    public partial class LoadingOverlay : ContentView
    {

        //public static BindableProperty TextProperty = BindableProperty.Create(
        //propertyName: "Text",
        //returnType: typeof(string),
        //declaringType: typeof(LoadingOverlay),
        //defaultValue: "",
        //defaultBindingMode: BindingMode.TwoWay,

        //propertyChanged: (bindable, oldValue, newValue) =>
        //{
        //            // Property has changed

        //            // We can call something here to update the UI
        //            var thisView = (LoadingOverlay)bindable;

        //            // Update the UI to display
        //            thisView.overlayText.Text = (string)newValue;

        //});
        public static BindableProperty TextProperty = BindableProperty.Create(
                                                      propertyName: "Text",
                                                      returnType: typeof(string),
                                                      declaringType: typeof(LoadingOverlay),
                                                      defaultValue: "",
                                                      defaultBindingMode: BindingMode.TwoWay);
        public static BindableProperty TextColorProperty = BindableProperty.Create(
                                                       propertyName: "TextColor",
                                                       returnType: typeof(Color),
                                                       declaringType: typeof(LoadingOverlay),
                                                       defaultValue: Color.White,
                                                       defaultBindingMode: BindingMode.TwoWay);
        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }
        public Color TextColor
        {
            get
            {
                return (Color)GetValue(TextColorProperty);
            }
            set
            {
                SetValue(TextColorProperty, value);
            }
        }

        public LoadingOverlay()
        {
            InitializeComponent();
        }
    }

}
