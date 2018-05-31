using System;
using RaccoonMsgr.Droid.Renderers;
using RaccoonMsgr.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using RaccoonMsgr.Controls;
using Android.Content;
using Android.Views.Animations;
using Java.Lang;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(LoadingOverlay), typeof(LoadingOverlayRenderer))]
namespace RaccoonMsgr.Droid.Renderers
{
    public class LoadingOverlayRenderer : VisualElementRenderer<ContentView>
    {
        public static void Init() { }
        private Context _context;
        public LoadingOverlayRenderer(Context context) : base(context)
        {
            _context = context;
        }
        protected override void OnElementChanged(ElementChangedEventArgs<ContentView> e)
        {
            base.OnElementChanged(e);
            global::Android.Views.Animations.Animation a = AnimationUtils.LoadAnimation(_context, Resource.Drawable.fade_in);
            a.Reset();

            
            this.ClearAnimation();
            this.StartAnimation(a);

        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == LoadingOverlay.IsVisibleProperty.PropertyName)
            {
                if (!Element.IsVisible)
                {
                    global::Android.Views.Animations.Animation a = AnimationUtils.LoadAnimation(_context, Resource.Drawable.fade_out);
                    a.Reset();


                    this.ClearAnimation();
                    this.StartAnimation(a);
                }
            }
        }

    }
}
