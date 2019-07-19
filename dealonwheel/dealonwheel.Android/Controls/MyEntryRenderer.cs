using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Content;
using dealonwheel.Controls;
using dealonwheel.Android;

[assembly: ExportRenderer(typeof(MyEntry), typeof(MyEntryRenderer))]
namespace dealonwheel.Android
{
    class MyEntryRenderer : EntryRenderer
    {
        public MyEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                // Customize here
                Control.SetBackgroundColor(global::Android.Graphics.Color.Transparent);
            }
        }
    }
}

