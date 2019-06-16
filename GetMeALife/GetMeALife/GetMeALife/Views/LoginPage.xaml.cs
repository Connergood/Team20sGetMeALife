using GetMeALibrary.Model;
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

        public void OnLoginClicked(object sender, EventArgs e)
        {
            var userName = etyUsername.Text;
            var passWord = etyPassword.Text;

            // query for user with that username - if none, create/ if found, check password
            var responseString = App.client.GetStringAsync("http://getmealife.azurewebsites.net/graphql?query={users{firstName,lastName,username,password,phone}}");
            responseString.Wait();

            User user = null;
            
            if (user != null)
            {
                if (user.Password == passWord)
                    Application.Current.MainPage.Navigation.PushAsync(new FilterPage());
                else
                {
                    lblError.Text = "Password Doesn't Match";
                }
            }
            else
            {
                User newUser = new User();
            }
        }
	}
}