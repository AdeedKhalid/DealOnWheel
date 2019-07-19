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
    public partial class SignIn : ContentPage
    {
        public SignIn()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private async void signin_btn_Clicked(object sender, EventArgs e)
        {
            signin_btn.IsEnabled = false;
            if (String.IsNullOrWhiteSpace(login_username.Text) ||
                String.IsNullOrWhiteSpace(login_userpassword.Text))
            {
                await DisplayAlert("Alert", "Please Fill Out All The Field(s)", "Ok");
            }
            else
            {
                try
                {
                    var data = API_SignIn(login_username.Text, login_userpassword.Text);
                    if (data.GetValue("stat") != null)
                    {
                        string status = data.GetValue("stat").ToString();
                        if (status == "200")
                        {
                            string jwtToken = "";
                            string userId = "";
                            string userName = "";

                            if (data.GetValue("user") != null)
                            {
                                var loggedInUser = data.GetValue("user");
                                if (loggedInUser["username"] != null)
                                {
                                    userName = loggedInUser["username"].ToString();
                                }
                            }
                            jwtToken = data.GetValue("token").ToString();
                            userId = data.GetValue("id").ToString();

                            CrossSecureStorage.Current.SetValue("userId", userId);
                            CrossSecureStorage.Current.SetValue("userName", userName);
                            CrossSecureStorage.Current.SetValue("jwtToken", jwtToken);
                        }

                        if (status == "200")
                        {
                            await Navigation.PushModalAsync(new Master());
                        }
                        else
                        {
                            await DisplayAlert("Alert", "Invalid Credentials!", "Ok");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Alert", "Something Went Wrong!", "Ok");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Exception: ", "Signin Screen :" + ex.Message.ToString(), "OK");
                }
            }
            signin_btn.IsEnabled = true;
        }

        public JObject API_SignIn(string email, string password)
        {
            JObject rss = new JObject(
                    new JProperty("email", email),
                    new JProperty("password", password)
            );
            var json = JsonConvert.SerializeObject(rss);
            var client = new HttpClient();
            var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage result = client.PostAsync("http://184.72.134.26:5001/api/users/login", content).Result;
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

        private async void SignUpTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUp());
        }

        private async void ForgotPasswordTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ForgotPassword());
        }

    }
}