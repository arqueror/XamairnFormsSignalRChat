using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ImageCircle.Forms.Plugin.Droid;
using Lottie.Forms.Droid;
using Plugin.DeviceInfo;
using Xamarin.Forms;
using Plugin.Toasts;
using Java.Security;
using Android.Support.Design.Widget;
using RaccoonMsgr.Droid.Renderers;

namespace RaccoonMsgr.Droid
{
    [Activity(Label = "RaccoonMsgr", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {


        public static Activity Activity;
        protected override void OnCreate(Bundle bundle)
        {
            Activity = this;
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            DependencyService.Register<ToastNotification>(); // Register your dependency
            ToastNotification.Init(this);
            ImageCircleRenderer.Init();
            Messier16.Forms.Controls.Droid.PlatformTabbedPageRenderer.Init();
            BadgeView.Android.CircleViewRenderer.Initialize();

            LoadApplication(new App());
        }
    }
}

