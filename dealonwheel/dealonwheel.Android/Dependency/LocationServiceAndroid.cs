using dealonwheel.Dependency;
using dealonwheel.Droid.Dependency;
using Xamarin.Forms;

[assembly: Dependency(typeof(LocationServiceAndroid))]
namespace dealonwheel.Droid.Dependency
{
    public class LocationServiceAndroid : Java.Lang.Object, ILocationService
    {
        public void TurnOnDeviceLocation()
        {
            MainActivity.Instance.TurnOnAndroidDeviceLocation();
        }
    }
}