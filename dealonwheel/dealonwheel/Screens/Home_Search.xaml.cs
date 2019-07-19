using dealonwheel.Models;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms.Xaml;

namespace dealonwheel.Screens
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Home_Search : Rg.Plugins.Popup.Pages.PopupPage
    {
        private ObservableCollection<Wish> _WishList;

        public Home_Search ()
		{
			InitializeComponent ();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            PopulateWishList();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        // Invoked when back button is clicked
        protected override bool OnBackButtonPressed()
        {
            // Return true if you don't want to close this popup page when a back button is pressed
            // return true;

            // Return 'base.OnBackButtonPressed()' if you want to close this popup page when a back button is pressed
            return base.OnBackButtonPressed();
        }

        // Invoked when background is clicked
        protected override bool OnBackgroundClicked()
        {
            // Return 'false' if you don't want to close this popup page when a background of the popup page is clicked
            // return false;

            // Return 'base.OnBackgroundClicked();' if you want to close this popup page when a background of the popup page is clicked
            return base.OnBackgroundClicked();
        }

        public void PopulateWishList()
        {
            _WishList = GetWishList();
            WishList.ItemsSource = _WishList;
        }

        public ObservableCollection<Wish> GetWishList()
        {
            _WishList = new ObservableCollection<Wish>();

            return new ObservableCollection<Wish> {
                new Wish { Id="1", Title= "Wish List 1", Picture = "icon_compliment_thumbsup.png"},
                new Wish { Id="2", Title= "Wish List 2", Picture = "icon_compliment_rocket.png"},
                new Wish { Id="3", Title= "Wish List 3", Picture = "icon_compliment_diamond.png"},
                new Wish { Id="4", Title= "Wish List 4", Picture = "icon_compliment_thumbsup.png"},
                new Wish { Id="5", Title= "Wish List 5", Picture = "icon_compliment_rocket.png"},
                new Wish { Id="6", Title= "Wish List 6", Picture = "icon_compliment_diamond.png"}
            };
        }

        private void Search_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {

        }

        private void WishList_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            WishList.Focus();
            var myItem = e.Item as Wish;
            int cardId = Convert.ToInt32(myItem.Id.ToString());
        }

    }
}