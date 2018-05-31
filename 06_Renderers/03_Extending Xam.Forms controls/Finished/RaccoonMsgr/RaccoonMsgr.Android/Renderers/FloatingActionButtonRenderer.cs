using System;
using RaccoonMsgr.Droid.Renderers;
using RaccoonMsgr.Renderers;
using Xamarin.Forms;
using Android.Support.Design.Widget;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using Android.Content.Res;
using Android.Views;
using static Android.App.ActionBar;

[assembly: Xamarin.Forms.Dependency(typeof(FloatingActionButtonRenderer))]
namespace RaccoonMsgr.Droid.Renderers
{
    public class FloatingActionButtonRenderer : IFloatingActionButtonRenderer
    {
        public FloatingActionButtonRenderer()
        {
        }

        public Xamarin.Forms.View CreateFloatingActionButton(Action tappedCallback)
        {
            //Get native control
            var actionButton = new FloatingActionButton(MainActivity.Activity);

            
            //Set Button background and icon
            actionButton.BackgroundTintList = ColorStateList.ValueOf(global::Android.Graphics.Color.ParseColor("#0098E9"));
            actionButton.SetImageResource(Resource.Drawable.icon);


            //Fix Button margin (this will add a shadow to button)
            float d = MainActivity.Activity.Resources.DisplayMetrics.Density;
            var margin = (int)(16 * d); // margin in pixels (margin * dpi)
            var lp = new FrameLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);
            lp.Gravity = GravityFlags.CenterVertical | GravityFlags.CenterHorizontal;
            lp.LeftMargin = margin;
            lp.TopMargin = margin;
            lp.BottomMargin = margin;
            lp.RightMargin = margin;
            actionButton.LayoutParameters = lp;

            //When Native control is tapped , we will pass the call to Xam.Forms through the Action received
            actionButton.Click += (s, a) => { tappedCallback(); };

            //set the position of button bottom-right corner of container
            var actionButtonFrame = new FrameLayout(MainActivity.Activity);
            actionButtonFrame.SetClipToPadding(true);
            actionButtonFrame.SetPadding(0, 0, 50, 50);
            actionButtonFrame.AddView(actionButton);

            //Convert native control  to Xam.Forms View  and return it to Xam.Forms
            var actionButtonFrameView = actionButtonFrame.ToView();
            actionButtonFrameView.HorizontalOptions = LayoutOptions.End;
            actionButtonFrameView.VerticalOptions = LayoutOptions.End;

            return actionButtonFrameView;
        }
    }
}
