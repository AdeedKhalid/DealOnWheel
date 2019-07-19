using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.SecureStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace dealonwheel.Screens
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SignUp : ContentPage
	{
		public SignUp ()
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

        private async void signup_btn_Clicked(object sender, EventArgs e)
        {
            signup_btn.IsEnabled = false;
            if (String.IsNullOrWhiteSpace(signup_username.Text) ||
                String.IsNullOrWhiteSpace(signup_email.Text) ||
                String.IsNullOrWhiteSpace(signup_password.Text) ||
                String.IsNullOrWhiteSpace(signup_gender.Text) ||
                signup_gender.Text.ToString().Equals("Please Specify...") ||
                termsAgreement.IsToggled == false)
            {
                await DisplayAlert("Alert", "Please Fill Out All The Field(s) & Agree To Our Terms & Conditions", "Ok");
            }
            else
            {
                try
                {
                    string promotionCode = "";
                    if (promotionCodeSwitch.IsToggled == true)
                        if (!String.IsNullOrWhiteSpace(lbl_promotioncode.Text))
                            promotionCode = lbl_promotioncode.Text;

                    var data = API_SignUp(signup_email.Text, signup_password.Text, signup_username.Text, "123456789", signup_gender.Text, "", promotionCode);
                    if (data.GetValue("id") != null)
                    {
                        string id = data.GetValue("id").ToString();
                        string status = data.GetValue("status").ToString();
                        string jwtToken = data.GetValue("token").ToString();
                        string userName = "";

                        if (data.GetValue("user") != null)
                        {
                            var loggedInUser = data.GetValue("user");
                            if (loggedInUser["username"] != null)
                            {
                                userName = loggedInUser["username"].ToString();
                            }
                        }
                        CrossSecureStorage.Current.SetValue("userId", id);
                        CrossSecureStorage.Current.SetValue("userName", userName);
                        CrossSecureStorage.Current.SetValue("jwtToken", jwtToken);

                        if (status == "Register Successfully")
                        {
                            await Navigation.PushModalAsync(new Master());
                        }
                        else
                        {
                            await DisplayAlert("Alert", "Something Went Wrong!", "Ok");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Alert", "Something Went Wrong!", "Ok");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Exception: ", "SignUp Screen :" + ex.Message.ToString(), "OK");
                }
            }
            signup_btn.IsEnabled = true;
        }

        public JObject API_SignUp(string email, string password, string username, string phone, string gender, string birthday, string code)
        {
            string bdaydate = "2019-05-05";

            int genderval = -1;
            if (gender.ToLower().Equals("male"))
                genderval = 1;
            if (gender.ToLower().Equals("female"))
                genderval = 2;
            if (gender.ToLower().Equals("other"))
                genderval = 3;

            JObject rss = new JObject(
                    new JProperty("user",
                        new JObject(
                            new JProperty("email", email),
                            new JProperty("password", password),
                            new JProperty("username", username),
                            new JProperty("phone", phone),
                            new JProperty("role", 1),
                            new JProperty("birthday", bdaydate),
                            new JProperty("gender", genderval),
                            new JProperty("code", code)
                )));
            var json = JsonConvert.SerializeObject(rss);
            var client = new HttpClient();
            var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage result = client.PostAsync("http://184.72.134.26:5001/api/users/create", content).Result;
            if (result.IsSuccessStatusCode)
            {
                var dataObjects = result.Content.ReadAsStringAsync().Result;
                JObject data = JObject.Parse(dataObjects);
                return data;
            }
            else
            {
                return null;
            }
        }

        private void SignInTapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void GenderTapped(object sender, EventArgs e)
        {
            try
            {
                var response = await DisplayActionSheet("Select Gender...", "Cancel", "Clear", "Male", "Female", "Other");
                if (response != null)
                {
                    switch (response.ToString())
                    {
                        case "Male":
                            signup_gender.Text = "Male";
                            break;
                        case "Female":
                            signup_gender.Text = "Female";
                            break;
                        case "Other":
                            signup_gender.Text = "Other";
                            break;
                        case "Clear":
                            signup_gender.Text = "Please Specify...";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Exception: ", "SignUp Screen :" + ex.Message.ToString(), "OK");
            }
        }

        private void PromotionCodeSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            if (e.Value)
            {
                lbl_promotioncode.IsVisible = true;
            }
            else
            {
                lbl_promotioncode.IsVisible = false;
            }
        }


    }
}