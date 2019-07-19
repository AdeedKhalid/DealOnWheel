using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using UIKit;
using dealonwheel.Controls;
using dealonwheel.iOS.Controls;
using System.ComponentModel;

[assembly: ExportRenderer (typeof(MySearchBar), typeof(MySearchBarRenderer))]
namespace dealonwheel.iOS.Controls
{
    public class MySearchBarRenderer : SearchBarRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.ShowsCancelButton = false;

                UITextField txSearchField = (UITextField)Control.ValueForKey(new Foundation.NSString("searchField"));
                txSearchField.BackgroundColor = UIColor.White;
                txSearchField.BorderStyle = UITextBorderStyle.None;
                txSearchField.Layer.BorderWidth = 5.0f;
                txSearchField.Layer.CornerRadius = 2.0f;
                txSearchField.Layer.BorderColor = UIColor.Clear.CGColor;

                Control.SearchTextPositionAdjustment = new UIOffset(10.0f, 0.0f);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //Override needed, otherwise the original Xamarin code will force show the Cancel button on the right side of the entry field
            if (e.PropertyName == SearchBar.TextProperty.PropertyName)
                Control.Text = Element.Text;

            if (e.PropertyName != SearchBar.CancelButtonColorProperty.PropertyName && e.PropertyName != SearchBar.TextProperty.PropertyName)
                base.OnElementPropertyChanged(sender, e);
        }
    }
}

