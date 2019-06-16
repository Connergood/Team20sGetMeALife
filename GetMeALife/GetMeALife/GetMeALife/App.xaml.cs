using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GetMeALife.Views;
using System.Net.Http;
using GetMeALibrary.Model;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace GetMeALife
{
    public partial class App : Application
    {
        public static readonly HttpClient client = new HttpClient();
        public static User CurrentUser { get; set; }
        public const string ApiUrl = "http://getmealife.azurewebsites.net/graphql";
        public static bool FirstTime { get; set; } = false;
        public App()
        {
            InitializeComponent();
            MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
