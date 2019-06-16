using GetMeALibrary.Model;
using GetMeALifeLibrary.Api;
using GetMeALifeLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GetMeALife.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilterPage : ContentPage
    {
        public FilterPage()
        {
            InitializeComponent();

            BackgroundImage = "background.jpg";
        }

        public async void OnAnotherLifeClicked(object sender, EventArgs e)
        {
            if (App.FirstTime)
                await DisplayAlert("Hold on there", "Great to see you're so proactive! Another Life will provide you with insights to all the nearby events based on events you've never attended. The more events you track, the more suggestions we can provide, so until you start tracking events we can't provide as filtered suggestions here.", "OK!");

            var eventTypesIDs = GetMostFrequentedEventTypes();
            if (!Util.ArrayIsNullOrEmpty(eventTypesIDs.ToArray()))
                Application.Current.MainPage = new NavigationPage(new EventPage(eventTypesIDs, true));
            else
            {
                OnGetLifeClicked(sender, e);
            }
        }

        public async void OnFindLifeClicked(object sender, EventArgs e)
        {
            await DisplayAlert("We're Sorry!", "This feature is still in development. Please try one of our other life options.", "Darn!");
        }

        public async void OnGetLifeClicked(object sender, EventArgs e)
        {
            if(App.FirstTime)
                await DisplayAlert("Let's Get Started", "Get Me A Life will provide you with insights to all the nearby events. The more events you track, the more suggestiosn we can provide in other life options. ", "OK!");
            List<int> eventTypeIDs = Api.GetList<EventType>(App.ApiUrl, new EventType()).Select(et => et.id).ToList();

            if (eventTypeIDs != null)
            {
                Application.Current.MainPage = new NavigationPage(new EventPage(eventTypeIDs));
            }
        }

        public async void OnSuggestLifeClicked(object sender, EventArgs e)
        {
            if (App.FirstTime)
                await DisplayAlert("Hold on there", "Great to see you're so proactive! Suggest Me A Life will provide you with insights to all the nearby events based on events you've attended. The more events you track, the more suggestions we can provide, so until you start tracking events we can't provide as filtered suggestions here.", "OK!");
            var eventTypesIDs = GetMostFrequentedEventTypes();
            if (!Util.ArrayIsNullOrEmpty(eventTypesIDs.ToArray()))
                Application.Current.MainPage = new NavigationPage(new EventPage(eventTypesIDs));
            else
            {
                OnGetLifeClicked(sender, e);
            }
        }

        private List<int> GetMostFrequentedEventTypes()
        {
            List<UserType> userTypes = Api.GetList<UserType>(App.ApiUrl, new UserType()).Where(ut => ut.userid == App.CurrentUser.id).ToList();
            List<int> eventTypeIDs = new List<int>();
            if (!Util.ArrayIsNullOrEmpty(userTypes.ToArray()))
            {
                // Take top 3 most frequented
                userTypes = userTypes.OrderByDescending(ut => ut.occurrences).ToList();
                eventTypeIDs.Add(userTypes[0].eventtypeid);
                if (userTypes.Count >= 2)
                {
                    eventTypeIDs.Add(userTypes[1].eventtypeid);
                    if (userTypes.Count >= 2)
                        eventTypeIDs.Add(userTypes[2].eventtypeid);
                }
            }
            return eventTypeIDs;
        }

        protected override bool OnBackButtonPressed()
        {
            try
            {
                Application.Current.MainPage = new NavigationPage(new LoginPage());
                return true;
            }
            catch
            {
                return base.OnBackButtonPressed();
            }
        }
    }
}