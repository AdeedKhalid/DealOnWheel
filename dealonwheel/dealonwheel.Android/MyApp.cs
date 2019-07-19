using System;
using Android.App;
using Android.Runtime;

namespace dealonwheel.Droid
{
    [Application]
    [MetaData("com.google.android.maps.v2.API_KEY",
              Value = "AIzaSyAvoU1_MAgmcocL21R-Roi1Bxr916h3D0A")]
    public class MyApp : Application
    {
        public MyApp(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }
    }
}

