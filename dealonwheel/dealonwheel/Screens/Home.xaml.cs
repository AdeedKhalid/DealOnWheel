using dealonwheel.Dependency;
using Plugin.Geolocator;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace dealonwheel.Screens
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        private Position MyMapCenterPosition;

        public Home()
        {
            InitializeComponent();
            MyMap.MapStyle = MapStyle.FromJson(App.MapStylingTheme);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (App.IsLocationAccessAvailable() == true)
            {
                if (Device.RuntimePlatform == Device.Android)
                    DependencyService.Get<ILocationService>().TurnOnDeviceLocation();

                if (App.MyPosition.Latitude == 0 && App.MyPosition.Longitude == 0)
                    App.getCurrentLocationThenZoom(MyMap);
                else
                    App.zoomToCurrentLocation(MyMap);
            }
            else
            {
                if (Device.RuntimePlatform == Device.Android)
                    DependencyService.Get<ILocationService>().TurnOnDeviceLocation();
                App.RequestAndGrantLocationPermission();
            }

            App.SetMapSettings(MyMap, App.IsLocationAccessAvailable());
            Master.Obj_Master.UpdateProfilePic();
        }

        private void MoveToCurrentLocation_Clicked(object sender, EventArgs e)
        {
            if (App.MyPosition.Latitude == 0 && App.MyPosition.Longitude == 0)
            {
                App.getCurrentLocationThenZoom(MyMap);
            }
            else
            {
                App.zoomToCurrentLocation(MyMap);
            }
            btn_MoveToCurrentLocation.IsVisible = false;
            btn_SetRotation.IsVisible = false;
        }

        private void MyMap_CameraMoveStarted(object sender, CameraMoveStartedEventArgs e)
        {
            if (e.IsGesture == true)
                btn_MoveToCurrentLocation.IsVisible = true;
        }

        private async void Search_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new Home_Search());
        }

        private async void SetRotation_Clicked(object sender, EventArgs e)
        {
            btn_SetRotation.IsVisible = false;

            // Without Animation
            //await MyMap.MoveCamera(CameraUpdateFactory.NewCameraPosition(
            //    new CameraPosition(
            //        new Position(MyMapCenterPosition.Latitude, MyMapCenterPosition.Longitude), // center
            //        15d, // zoom
            //        0d, // bearing(rotation)
            //        0d))); // tilt

            // With Animation
            await MyMap.AnimateCamera(CameraUpdateFactory.NewCameraPosition(
                new CameraPosition(
                    new Position(MyMapCenterPosition.Latitude, MyMapCenterPosition.Longitude), // center
                    15d, // zoom
                    0d, // bearing(rotation)
                    0d)), // tilt
                    TimeSpan.FromSeconds(0.5));
        }

        private void MyMap_PropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            var _map = (sender as Map);
            if (_map.VisibleRegion != null)
            {
                var currentViewLat = _map.VisibleRegion.Center.Latitude;
                var currentViewLon = _map.VisibleRegion.Center.Longitude;
                MyMapCenterPosition = new Position(currentViewLat, currentViewLon);
            }
        }

        private void MyMap_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var _map = (sender as Map);
            if (_map.CameraPosition != null)
            {
                var _bearing = _map.CameraPosition.Bearing;
                if (_bearing != 0)
                    btn_SetRotation.IsVisible = true;
                else
                    btn_SetRotation.IsVisible = false;
            }
        }

    }
}