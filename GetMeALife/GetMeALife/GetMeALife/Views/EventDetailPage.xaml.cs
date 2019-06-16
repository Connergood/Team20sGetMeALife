using GetMeALife.ViewModels;
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
		public EventDetailPage ()
		{
			InitializeComponent ();
		}

        public EventDetailPage(EventDetailViewModel selectedEvent) : this()
        {
            eventDetails = selectedEvent;
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

        public void OnConfirmClick(object sender, EventArgs e)
        {
            // Ping the DB to increment this type

            // Flag this event as confirmed
            eventDetails.isConfirmed = true;
        }
	}
}