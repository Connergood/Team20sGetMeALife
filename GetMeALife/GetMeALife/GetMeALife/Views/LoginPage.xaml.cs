using GetMeALibrary.Model;
using GetMeALifeLibrary.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GetMeALife.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent ();

            BackgroundImage = "background.jpg";
        }

        public async void OnLoginClicked(object sender, EventArgs e)
        {
            var userName = etyUsername.Text;
            var passWord = etyPassword.Text;

            // query for user with that username - if none, create/ if found, check password
            var user = Api.GetList<User>("http://getmealife.azurewebsites.net/graphql", new User()).Where(u => u.username == userName).FirstOrDefault();
            
            if (user != null)
            {
                if (user.password == passWord)
                {
                    Application.Current.MainPage = new NavigationPage(new FilterPage());
                    App.CurrentUser = user;
                }
                else
                {
                    lblError.Text = "Password Doesn't Match";
                }
            }
            else
            {
                user = new User()
                {
                    username = etyUsername.Text,
                    password = etyPassword.Text,
                    firstname = "New",
                    lastname = "User",
                    phone = "555-555-5555"
                };
                user = Api.Create<User>(App.ApiUrl, user);
                if (user != null && user.id > 0)
                {
                    await DisplayAlert("Welcome!", "Welcome New User! We are excited for you to join many others in living their lives together!", "OK!");
                    App.CurrentUser = user;
                    App.FirstTime = true;
                }
                else
                {
                    lblError.Text = "Failed to connect, Please try again...";
                }
            }
        }
	}
}