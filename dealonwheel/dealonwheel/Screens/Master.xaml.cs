using dealonwheel.Controls;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace dealonwheel.Screens
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Master : MasterDetailPage
    {
        public static Master Obj_Master;

        public Master()
        {
            InitializeComponent();
            Obj_Master = this;
            Detail = new MyNavigationPage(new Home());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        public void UpdateProfilePic()
        {
            if (App.SelectedImage != null && !App.SelectedImage.ToString().ToLower().Equals("file: icon_profile_new.png"))
                ProfilePic.Source = App.SelectedImage;
        }

        private void Home_Tapped(object sender, EventArgs e)
        {
            IsPresented = false;
            //Detail.Navigation.PopAsync();
        }

        private void WishList_Tapped(object sender, EventArgs e)
        {
            IsPresented = false;
        }

        private void Notifications_Tapped(object sender, EventArgs e)
        {
            IsPresented = false;
        }

        private void History_Tapped(object sender, EventArgs e)
        {
            IsPresented = false;
        }

        private void Settings_Tapped(object sender, EventArgs e)
        {
            IsPresented = false;
        }

        private void Help_Tapped(object sender, EventArgs e)
        {
            IsPresented = false;
        }

        private void Logout_Tapped(object sender, EventArgs e)
        {
            IsPresented = false;
            //await Navigation.PushModalAsync(new NavigationPage(new SignIn()));
        }

        private void SwitchToSeller_Clicked(object sender, EventArgs e)
        {
            IsPresented = false;
        }

        private async void EditPhoto_Tapped(object sender, EventArgs e)
        {
            IsPresented = false;
            await Navigation.PushModalAsync(new Master_EditPhoto());
        }
    }
}