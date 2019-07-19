using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace dealonwheel.Screens
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ShowProfilePhoto : ContentPage
	{
		public ShowProfilePhoto ()
		{
			InitializeComponent ();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (App.SelectedImage != null && !App.SelectedImage.ToString().ToLower().Equals("file: icon_profile_new.png"))
            {
                ProfilePic.Source = App.SelectedImage;
                Master_EditPhoto.Obj_Master_EditPhoto.UpdateProfilePic();
                Master.Obj_Master.UpdateProfilePic();
            }
        }

        private async void Update_Clicked(object sender, EventArgs e)
        {
            App.SelectedImage = ProfilePic.Source;
            await Navigation.PushAsync(new UpdateProfilePhoto());
        }
    }
}