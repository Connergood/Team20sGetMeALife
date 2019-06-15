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
		public EventDetailPage ()
		{
			InitializeComponent ();
		}

        public static void OnConfirmClick()
        {
            // Ping the DB to increment this type

            // Flag this event as confirmed
        }
	}
}