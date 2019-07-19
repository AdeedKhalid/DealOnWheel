using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using ImageCircle.Forms.Plugin.Droid;
using Plugin.CurrentActivity;
using Plugin.Permissions;
using Xamarin.Forms.GoogleMaps.Android;
using System;
using Android.Gms.Common.Apis;
using Android.Gms.Location;
using Xamarin.Forms;
using Android.Content.Res;
using FFImageLoading.Forms.Droid;

namespace dealonwheel.Droid
{
    [Activity(Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        Int64 interval = 1000 * 60 * 1, fastestInterval = 1000 * 50;
        internal static MainActivity Instance { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            ImageCircleRenderer.Init();
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            CachedImageRenderer.Init();
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, savedInstanceState);
            var platformConfig = new PlatformConfig { BitmapDescriptorFactory = new CachingNativeBitmapDescriptorFactory() };
            Xamarin.FormsGoogleMaps.Init(this, savedInstanceState, platformConfig);

            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, savedInstanceState);
            Instance = this;
            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public async void TurnOnAndroidDeviceLocation()
        {
            try
            {
                GoogleApiClient googleApiClient = new GoogleApiClient.Builder(this)
                    .AddApi(LocationServices.API).Build();

                googleApiClient.Connect();

                LocationRequest locationRequest = LocationRequest.Create()
                .SetPriority(LocationRequest.PriorityBalancedPowerAccuracy)
                .SetInterval(interval)
                .SetFastestInterval(fastestInterval);

                LocationSettingsRequest.Builder locationSettingsRequestBuilder = new LocationSettingsRequest.Builder()
                        .AddLocationRequest(locationRequest);

                locationSettingsRequestBuilder.SetAlwaysShow(false);

                LocationSettingsResult locationSettingsResult = await LocationServices.SettingsApi.CheckLocationSettingsAsync(
                googleApiClient, locationSettingsRequestBuilder.Build());

                if (locationSettingsResult.Status.StatusCode == LocationSettingsStatusCodes.ResolutionRequired)
                {
                    locationSettingsResult.Status.StartResolutionForResult(this, 0);
                }
            }
            catch (Exception ex) { var error = ex.Message; }
        }

        public override void OnBackPressed()
        {
            if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            {
                // Do something if there are some pages in the `PopupStack`
            }
            else
            {
                // Do something if there are not any pages in the `PopupStack`
            }
        }

        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);

            switch (newConfig.Orientation)
            {
                case Orientation.Landscape:
                    switch (Device.Idiom)
                    {
                        case TargetIdiom.Phone:
                            LockRotation(Orientation.Portrait);
                            break;
                        case TargetIdiom.Tablet:
                            LockRotation(Orientation.Undefined);
                            break;
                    }
                    break;
                case Orientation.Portrait:
                    switch (Device.Idiom)
                    {
                        case TargetIdiom.Phone:
                            LockRotation(Orientation.Portrait);
                            break;
                        case TargetIdiom.Tablet:
                            LockRotation(Orientation.Undefined);
                            break;
                    }
                    break;
            }
        }

        private void LockRotation(Orientation orientation)
        {
            switch (orientation)
            {
                case Orientation.Portrait:
                    RequestedOrientation = ScreenOrientation.Portrait;
                    break;
                case Orientation.Landscape:
                    RequestedOrientation = ScreenOrientation.Landscape;
                    break;
                case Orientation.Undefined:
                    RequestedOrientation = ScreenOrientation.Unspecified;
                    break;
            }
        }

    }
}