using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;
using AView = Android.Views.View;
using dealonwheel.Droid.Controls;
using dealonwheel.Controls;
using Android.Widget;

[assembly: ExportRenderer(typeof(MyNavigationPage), typeof(MyNavigationPageRenderer))]
namespace dealonwheel.Droid.Controls
{
    public class MyNavigationPageRenderer : NavigationPageRenderer
    {
        public MyNavigationPageRenderer(Context context) : base(context)
        {

        }

        IPageController PageController => Element as IPageController;
        MyNavigationPage MyNavigationPage => Element as MyNavigationPage;

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            MyNavigationPage.IgnoreLayoutChange = true;
            base.OnLayout(changed, l, t, r, b);
            MyNavigationPage.IgnoreLayoutChange = false;

            int containerHeight = b - t;


            // Reference from: https://stackoverflow.com/questions/33229560/xamarin-forms-center-title-and-transparent-navigation-bar-android/47726843#comment88440342_47726843
            // For Centeralized Text //
            var toolbar = FindViewById<global::Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            if (toolbar != null)
            {
                for (int index = 0; index < toolbar.ChildCount; index++)
                {
                    if (toolbar.GetChildAt(index) is TextView)
                    {
                        var title = toolbar.GetChildAt(index) as TextView;
                        float toolbarCenter = toolbar.MeasuredWidth / 2;
                        float titleCenter = title.MeasuredWidth / 2;
                        title.SetX(toolbarCenter - titleCenter);
                    }
                }
            }
            // For Centeralized Text //

            PageController.ContainerArea = new Rectangle(0, 0, Context.FromPixels(r - l), Context.FromPixels(containerHeight));

            for (var i = 0; i < ChildCount; i++)
            {
                AView child = GetChildAt(i);

                if (child is global::Android.Support.V7.Widget.Toolbar)
                {
                    continue;
                }

                child.Layout(0, 0, r, b);
            }
        }
    }
}