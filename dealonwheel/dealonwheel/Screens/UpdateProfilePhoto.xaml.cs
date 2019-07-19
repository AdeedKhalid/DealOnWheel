using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace dealonwheel.Screens
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UpdateProfilePhoto : ContentPage
	{
		public UpdateProfilePhoto ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            CheckForPicStatus();
        }

        public void CheckForPicStatus()
        {
            if (App.SelectedImage != null && !App.SelectedImage.ToString().ToLower().Equals("file: icon_profile_new.png"))
            {
                ProfilePicDefault.IsVisible = false;
                ProfilePicCroppable.IsVisible = true;
                ProfilePicCroppable.Source = App.SelectedImage;
                CancelButton.IsVisible = false;
                SaveButton.IsVisible = true;
                textProfilePic.Text = "Set/Crop Photo";
            }
            else
            {
                ProfilePicDefault.IsVisible = true;
                ProfilePicCroppable.IsVisible = false;
                ProfilePicDefault.Source = "icon_profile_new.png";
                CancelButton.IsVisible = true;
                SaveButton.IsVisible = false;
                textProfilePic.Text = "Update Profile Photo";
            }
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void TakePhoto_Clicked(object sender, EventArgs e)
        {
            var pic = await App.RequestAndGrantPermission(ProfilePicDefault);
            if (pic != null)
                App.SelectedImage = pic;
            CheckForPicStatus();
            //await Navigation.PopAsync();
        }

        private async void ChooseExisting_Clicked(object sender, EventArgs e)
        {
            var pic = await App.GetPhotoFromGallery(ProfilePicDefault);
            if (pic != null)
                App.SelectedImage = pic;
            CheckForPicStatus();
            //await Navigation.PopAsync();
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            var result = await ProfilePicCroppable.GetImageAsJpegAsync();
            byte[] bytes = null;
            using (MemoryStream ms = new MemoryStream())
            {
                result.CopyTo(ms);
                bytes = ms.ToArray();
            }
            var imageSource = ImageSource.FromStream(() =>
            {
                return new MemoryStream(bytes);
            });
            App.SelectedImage = imageSource;
            await Navigation.PopAsync();
        }
    }
}