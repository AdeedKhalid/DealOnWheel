using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using dealonwheel.iOS.Renderers;
using dealonwheel.Controls;

[assembly: ExportRenderer(typeof(MyNavigationPage), typeof(MyNavigationPageRenderer))]
namespace dealonwheel.iOS.Renderers
{
    public class MyNavigationPageRenderer : NavigationRenderer
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            UINavigationBar.Appearance.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);
            UINavigationBar.Appearance.ShadowImage = new UIImage();
            UINavigationBar.Appearance.BackgroundColor = UIColor.Clear;  // set bg color of navigation bar
            UINavigationBar.Appearance.TintColor = UIColor.White;   // set text color of navigation bar
            UINavigationBar.Appearance.BarTintColor = UIColor.Clear;
            UINavigationBar.Appearance.Translucent = true;  // set property of navigation bar for transparency
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }

            base.Dispose(disposing);
        }
    }
}