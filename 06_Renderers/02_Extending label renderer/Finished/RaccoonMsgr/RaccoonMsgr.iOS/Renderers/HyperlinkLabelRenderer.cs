using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using RaccoonMsgr.iOS.Renderers;
using RaccoonMsgr.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(HyperlinkLabel), typeof(HyperlinkLabelRenderer))]
namespace RaccoonMsgr.iOS.Renderers
{
    public class HyperlinkLabelRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            Control.UserInteractionEnabled = true;

            Control.TextColor = UIColor.Blue;

            var gesture = new UITapGestureRecognizer();

            gesture.AddTarget(() =>
            {
                var url = new NSUrl("https://" + Control.Text);

                if (UIApplication.SharedApplication.CanOpenUrl(url))
                    UIApplication.SharedApplication.OpenUrl(url);
            });

            Control.AddGestureRecognizer(gesture);
        }
    }
}