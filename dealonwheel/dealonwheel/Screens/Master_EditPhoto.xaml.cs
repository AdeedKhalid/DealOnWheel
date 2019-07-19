using dealonwheel.Controls;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace dealonwheel.Screens
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Master_EditPhoto : MasterDetailPage
    {
        public static Master_EditPhoto Obj_Master_EditPhoto;

        public Master_EditPhoto()
        {
            InitializeComponent();
            Obj_Master_EditPhoto = this;
            Detail = new MyNavigationPage(new ShowProfilePhoto());
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

        private async void GetDeal_Tapped(object sender, EventArgs e)
        {
            IsPresented = false;
            await Navigation.PushModalAsync(new Master());
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

        private void EditPhoto_Tapped(object sender, EventArgs e)
        {
            IsPresented = false;
        }
    }
}