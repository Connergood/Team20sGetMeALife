using GetMeALibrary.Model;
using GetMeALife.ViewModels;
using GetMeALifeLibrary.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GetMeALife.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EventDetailPage : ContentPage
	{
        private EventDetailViewModel eventDetails { get; set; }
        private List<int> previouslySeenTypesOnEventPage { get; set; }
        private UserType userType { get; set; }
		public EventDetailPage ()
		{
			InitializeComponent ();
		}

        public EventDetailPage(EventDetailViewModel selectedEvent, List<int> previousSeenTypes) : this()
        {
            eventDetails = selectedEvent;
            previouslySeenTypesOnEventPage = previousSeenTypes;

            //Check to see if user already has a track record.
            userType = Api.GetList(App.ApiUrl, new UserType()).Where(ut => ut.eventtypeid == eventDetails.eventDetail.eventtypeid &&
                                                                                     ut.userid == App.CurrentUser.id).FirstOrDefault();

            var details = selectedEvent.eventDetail;

            lblName.Text = details.name;
            lblType.Text = details.TypeName;
            lblTime.Text = details.eventstart.ToShortTimeString() + " - " + details.eventend.ToShortTimeString();
            lblLocation.Text = details.locationname;
            lblDescription.Text = details.description;

            if (details.price == 0)
                lblPrice.Text = "Free";
            else if (details.price == 1)
                lblPrice.Text = "$50+";
            else
            {
                var price = details.price * 50;
                lblPrice.Text = "Around $" + Math.Round(price, 0);
            }
        }

        public async void OnConfirmClicked(object sender, EventArgs e)
        {
            // Ping the DB to increment this type or start tracking it
            if (userType == null)
            {
                userType = new UserType()
                {
                    userid = App.CurrentUser.id,
                    eventtypeid = eventDetails.eventDetail.eventtypeid,
                    occurrences = 1
                };
                userType = Api.Create(App.ApiUrl, userType);
            }
            else
            {
                userType.occurrences++;
                userType = Api.Update(App.ApiUrl, userType, userType.id);
            }

            // Flag this event as confirmed
            eventDetails.isConfirmed = true;
            if (App.FirstTime)
            {
                await DisplayAlert("You're in!", $"Congrats {App.CurrentUser.username}! We are glad to see you taking matters into your own hands! We look forward in seeing all the events you attend.", "Thanks!");
                App.FirstTime = false;
            }
            else await DisplayAlert("You're in!", $"See you at {eventDetails.eventDetail.name}", "Can't Wait!");

            Application.Current.MainPage = new NavigationPage(new EventPage(previouslySeenTypesOnEventPage));
        }

        protected override bool OnBackButtonPressed()
        {
            try
            {
                Application.Current.MainPage = new NavigationPage(new EventPage(previouslySeenTypesOnEventPage));
                return true;
            }
            catch
            {
                return base.OnBackButtonPressed();
            }
        }
    }
}