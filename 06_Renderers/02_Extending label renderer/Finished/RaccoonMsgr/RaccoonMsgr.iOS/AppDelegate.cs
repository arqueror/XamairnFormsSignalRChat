using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Lottie.Forms.iOS.Renderers;
using Plugin.Toasts;
using Xamarin.Forms;
using UserNotifications;

namespace RaccoonMsgr.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {

            global::Xamarin.Forms.Forms.Init();
            DependencyService.Register<ToastNotification>(); // Register your dependency
            ToastNotification.Init();
            // Request Permissions
            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {
                // Request Permissions
                UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound, (granted, error) =>
                {
                    // Do something if needed
                });
            }
            else if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            {
                var notificationSettings = UIUserNotificationSettings.GetSettingsForTypes(
                UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound, null);

                app.RegisterUserNotificationSettings(notificationSettings);
            }
            LoadApplication(new App());
            AnimationViewRenderer.Init();
            ImageCircle.Forms.Plugin.iOS.ImageCircleRenderer.Init();
            BadgeView.iOS.CircleViewRenderer.Initialize();
            Messier16.Forms.Controls.iOS.PlatformTabbedPageRenderer.Init();
            return base.FinishedLaunching(app, options);
        }
    }
}
