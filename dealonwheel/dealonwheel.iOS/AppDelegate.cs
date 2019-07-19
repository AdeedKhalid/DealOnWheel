using System;
using System.Collections.Generic;
using System.Linq;
using FFImageLoading.Forms.Touch;
using Foundation;
using ImageCircle.Forms.Plugin.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps.iOS;
using Xamarin.Forms.Platform.iOS;

namespace dealonwheel.iOS
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
            ImageCircleRenderer.Init();
            Rg.Plugins.Popup.Popup.Init();
            CachedImageRenderer.Init();
            var platformConfig = new PlatformConfig { ImageFactory = new CachingImageFactory() };
            Xamarin.FormsGoogleMaps.Init("AIzaSyD24zazv6d2ljGMFdH31MDVog-A0k09HGs", platformConfig);
            LoadApplication(new App());

            var statusBar = UIApplication.SharedApplication.ValueForKey(new NSString("statusBar")) as UIView;
            if (statusBar.RespondsToSelector(new ObjCRuntime.Selector("setBackgroundColor:")))
            {
                statusBar.BackgroundColor = UIColor.Clear;
            }


            var attribute = new UITextAttributes();
            attribute.TextColor = Color.FromHex("#000").ToUIColor();
            UIBarButtonItem.Appearance.SetTitleTextAttributes(attribute, UIControlState.Normal);
            UIBarButtonItem.Appearance.SetTitleTextAttributes(attribute, UIControlState.Highlighted);
            UINavigationBar.Appearance.TintColor = Color.FromHex("#000").ToUIColor();

            return base.FinishedLaunching(app, options);
        }

        public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations(UIApplication application, UIWindow forWindow)
        {
            switch (Device.Idiom)
            {
                case TargetIdiom.Phone:
                    return UIInterfaceOrientationMask.Portrait;
                case TargetIdiom.Tablet:
                    return UIInterfaceOrientationMask.All;
                default:
                    return UIInterfaceOrientationMask.Portrait;
            }
        }

    }
}
