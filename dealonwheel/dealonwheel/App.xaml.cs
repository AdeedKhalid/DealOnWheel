using Newtonsoft.Json;
using Plugin.Geolocator;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace dealonwheel
{
    public partial class App : Application
    {
        public static Position MyPosition;
        public static string MapStylingTheme = "";
        public static ImageSource SelectedImage;

        public App()
        {
            InitializeComponent();
            SetMapStyleJsonTheme();
            MainPage = new NavigationPage(new Screens.SignIn());
        }

        public static bool IsLocationAccessAvailable()
        {
            return CrossGeolocator.Current.IsGeolocationAvailable;
        }

        public static async void getCurrentLocation()
        {
            var position = await CrossGeolocator.Current.GetPositionAsync();
            MyPosition = new Position(position.Latitude, position.Longitude);
        }

        public static void zoomToCurrentLocation(Map map)
        {
            map.MoveToRegion(
                    MapSpan.FromCenterAndRadius(
                        new Position(MyPosition.Latitude, MyPosition.Longitude), Distance.FromMiles(0.5)
                    )
            );
        }

        public static async void getCurrentLocationThenZoom(Map map)
        {
            var position = await CrossGeolocator.Current.GetPositionAsync();
            MyPosition = new Position(position.Latitude, position.Longitude);
            map.MoveToRegion(
                    MapSpan.FromCenterAndRadius(
                        new Position(MyPosition.Latitude, MyPosition.Longitude), Distance.FromMiles(0.5)
                    )
            );
        }

        public static async void RequestAndGrantLocationPermission()
        {
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
            if (status != PermissionStatus.Granted)
            {
                if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                {
                    //await DisplayAlert("Need Location Permission", "Need that location Access", "OK");
                }

                var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                if (results.ContainsKey(Permission.Location))
                    status = results[Permission.Location];
            }
            if (status == PermissionStatus.Granted)
            {
            }
            else if (status != PermissionStatus.Unknown)
            {
                //await DisplayAlert("Location Permission Denied", "Can not continue, try again.", "OK");
            }
        }

        private void SetMapStyleJsonTheme()
        {
            var assembly = GetType().GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("dealonwheel.Extensions.MapStyling.txt");
            string text = "";
            using (var reader = new System.IO.StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }
            MapStylingTheme = text;
        }

        public static void SetMapSettings(Map MyMap, bool isLocationPermissionEnabled)
        {
            MyMap.UiSettings.ZoomControlsEnabled = false;
            MyMap.UiSettings.ZoomGesturesEnabled = true;
            MyMap.MyLocationEnabled = isLocationPermissionEnabled;
            MyMap.UiSettings.MyLocationButtonEnabled = false;
            MyMap.IsTrafficEnabled = true;
            MyMap.MapType = MapType.Street;
            MyMap.UiSettings.CompassEnabled = false;
        }

        public static async Task<ImageSource> OpenAndTakeSnapFromCamera(Image image)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                //await DisplayAlert("No Camera", ":( No camera available.", "OK");
                return null;
            }

            MediaFile photo = null;
            if (Device.RuntimePlatform == Device.iOS)
            {
                photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions()
                {
                    PhotoSize = PhotoSize.Small,
                    //PhotoSize = PhotoSize.Custom,
                    //CustomPhotoSize = 90, // Resize to 90% of original
                    CompressionQuality = 100,
                    AllowCropping = true,
                    DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Front,
                    SaveToAlbum = true
                });
            }
            else
            {
                photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions()
                {
                    PhotoSize = PhotoSize.Small,
                    //PhotoSize = PhotoSize.Custom,
                    //CustomPhotoSize = 90, // Resize to 90% of original
                    CompressionQuality = 100,
                    AllowCropping = true,
                    DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Front,
                    SaveToAlbum = false
                });
            }

            if (photo != null)
            {
                image.Source = ImageSource.FromStream(() =>
                {
                    if (Device.RuntimePlatform == Device.iOS)
                    {
                        var stream = photo.GetStreamWithImageRotatedForExternalStorage();
                        return stream;
                    }
                    else
                    {
                        return photo.GetStream();
                    }
                });
                return image.Source;
            }
            else
            {
                return null;
            }
        }

        public static async Task<ImageSource> RequestAndGrantPermission(Image image)
        {
            var status1 = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
            var status2 = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Photos);

            if (status1 != PermissionStatus.Granted || status2 != PermissionStatus.Granted)
            {
                status1 = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
                var results1 = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Camera);
                if (results1.ContainsKey(Permission.Camera))
                    status1 = results1[Permission.Camera];

                status2 = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Photos);
                var results2 = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Photos);
                if (results2.ContainsKey(Permission.Photos))
                    status2 = results2[Permission.Photos];
            }

            if (status1 == PermissionStatus.Granted && status2 == PermissionStatus.Granted)
                return await OpenAndTakeSnapFromCamera(image);
            else
            {
                if (Device.RuntimePlatform == Device.iOS)
                {
                    //await DisplayAlert("Permissions Denied", "Please Enable Permission Access For Camera & Photos.", "OK");
                    CrossPermissions.Current.OpenAppSettings();
                }
                else
                {
                    //await DisplayAlert("Permissions Denied", "Please Enable Permission Access For Camera & Storage.", "OK");
                }
                return null;
            }
        }

        public static async Task<ImageSource> GetPhotoFromGallery(Image image)
        {
            var photo = await Plugin.Media.CrossMedia.Current.PickPhotoAsync();
            if (photo != null)
            {
                image.Source = ImageSource.FromStream(() =>
                {
                    if (Device.RuntimePlatform == Device.iOS)
                    {
                        var stream = photo.GetStreamWithImageRotatedForExternalStorage();
                        return stream;
                    }
                    else
                    {
                        return photo.GetStream();
                    }
                });
                return image.Source;
            }
            else
                return null;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            if (App.IsLocationAccessAvailable() == true)
            {
                //getCurrentLocation();
            }
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            if (App.IsLocationAccessAvailable() == true)
            {
                //getCurrentLocation();
            }
        }
    }
}
