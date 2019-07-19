using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Text.Method;

[assembly: ResolutionGroupName("Xamarin")]
[assembly: ExportEffect(typeof(dealonwheel.Droid.Controls.ShowHidePassEffect), "ShowHidePassEffect")]
namespace dealonwheel.Droid.Controls
{
    public class ShowHidePassEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            ConfigureControl();
        }

        protected override void OnDetached()
        {
        }

        private void ConfigureControl()
        {
            EditText editText = ((EditText)Control);
            editText.SetCompoundDrawablesRelativeWithIntrinsicBounds(0, 0, Resource.Drawable.icon_showpassword, 0);
            editText.SetOnTouchListener(new OnDrawableTouchListener());

        }
    }

    public class OnDrawableTouchListener : Java.Lang.Object, global::Android.Views.View.IOnTouchListener
    {
        public bool OnTouch(global::Android.Views.View v, MotionEvent e)
        {
            if (v is EditText && e.Action == MotionEventActions.Up)
            {
                EditText editText = (EditText)v;
                var right = editText.Right;
                var width = editText.GetCompoundDrawables()[2].Bounds.Width();
                var raw = e.RawX;

                if (raw >= (right - width + (right / 3)))
                {
                    if (editText.TransformationMethod == null)
                    {
                        editText.TransformationMethod = PasswordTransformationMethod.Instance;
                        editText.SetCompoundDrawablesRelativeWithIntrinsicBounds(0, 0, Resource.Drawable.icon_showpassword, 0);
                    }
                    else
                    {
                        editText.TransformationMethod = null;
                        editText.SetCompoundDrawablesRelativeWithIntrinsicBounds(0, 0, Resource.Drawable.icon_hidepassword, 0);
                    }

                    return true;
                }
            }

            return false;
        }
    }
}