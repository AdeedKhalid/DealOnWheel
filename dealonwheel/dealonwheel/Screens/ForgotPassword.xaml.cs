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
	public partial class ForgotPassword : ContentPage
	{
		public ForgotPassword ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private async void forgotpwd_btn_Clicked(object sender, EventArgs e)
        {
            forgotpwd_btn.IsEnabled = false;
            if (String.IsNullOrWhiteSpace(forgotpwd_email.Text))
            {
                await DisplayAlert("Alert", "Please Fill Out The Field", "Ok");
            }
            else
            {
                await DisplayAlert("Alert", "An Email For Password Reset Has Been Sent To Your Email!", "Ok");
                await Navigation.PopAsync();
            }
            forgotpwd_btn.IsEnabled = true;
        }

        private async void SignInTapped(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }


    }
}