using RaccoonMsgr.iOS.Renderers;
using RaccoonMsgr.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using System.Xml.Serialization;
using System;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using CoreGraphics;
using RaccoonMsgr.Controls;

[assembly: ExportRenderer(typeof(LoadingOverlay), typeof(LoadingOverlayRenderer))]
namespace RaccoonMsgr.iOS.Renderers
{
    public class LoadingOverlayRenderer : VisualElementRenderer<ContentView>
    {
        public LoadingOverlayRenderer()
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<ContentView> e)
        {
            base.OnElementChanged(e);

            FadeAnimation(this, true);
        }

        public static void FadeAnimation(UIView view, bool isIn, double duration = 0.6, Action s = null)
        {
            var minAlpha = (nfloat)0.0f;
            var maxAlpha = (nfloat)1.0f;
            view.Alpha = isIn ? minAlpha : maxAlpha;
            view.Transform = CGAffineTransform.MakeIdentity();
            UIView.Animate(duration, 0, UIViewAnimationOptions.CurveEaseInOut, () =>
                 {
                     view.Alpha = isIn ? maxAlpha : minAlpha;
                 }, s);
        }

    }

}
