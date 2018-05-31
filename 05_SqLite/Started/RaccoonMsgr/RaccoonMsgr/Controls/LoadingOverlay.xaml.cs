using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace RaccoonMsgr.Controls
{
    public partial class LoadingOverlay : ContentView
    {

        public static BindableProperty TextProperty = BindableProperty.Create(
                                                        propertyName: "Text",
                                                        returnType: typeof(string),
                                                        declaringType: typeof(LoadingOverlay),
                                                        defaultValue: "",
                                                        defaultBindingMode: BindingMode.TwoWay,

                                                        propertyChanged: (bindable, oldValue, newValue) =>
                                                        {
                                                            // Property has changed

                                                            // We can call something here to update the UI
                                                            var thisView = (LoadingOverlay)bindable;

                                                            // Update the UI to display
                                                            thisView.overlayText.Text = (string)newValue;

                                                        });
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
        public LoadingOverlay()
        {
            InitializeComponent();
        }
    }
}
