using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Content;
using dealonwheel.Controls;
using dealonwheel.Droid.Controls;
using Android.Widget;

[assembly: ExportRenderer(typeof(MySearchBar), typeof(MySearchBarRenderer))]
namespace dealonwheel.Droid.Controls
{
    class MySearchBarRenderer : SearchBarRenderer
    {
        public MySearchBarRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                var color = global::Xamarin.Forms.Color.LightGray;
                var searchView = Control as SearchView;

                int searchPlateId = searchView.Context.Resources.GetIdentifier("android:id/search_plate", null, null);
                global::Android.Views.View searchPlateView = searchView.FindViewById(searchPlateId);
                searchPlateView.SetBackgroundColor(global::Android.Graphics.Color.Transparent);
            }
        }
    }
}

