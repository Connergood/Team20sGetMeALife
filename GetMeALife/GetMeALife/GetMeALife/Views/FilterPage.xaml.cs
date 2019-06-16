using GetMeALibrary.Model;
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
	public partial class FilterPage : ContentPage
	{
		public FilterPage ()
		{
			InitializeComponent ();

            BackgroundImage = "background.jpg";
        }

        public static void OnAnotherLifeClicked(object sender, EventArgs e)
        {

        }

        public static void OnFindLifeClicked(object sender, EventArgs e)
        {

        }

        public static void OnGetLifeClicked(object sender, EventArgs e)
        {
            List<int> eventTypeIDs = Api.GetList<EventType>("http://getmealife.azurewebsites.net/graphql", new EventType()).Select(et => et.id).ToList();
            
            if (eventTypeIDs != null)
            {
                Application.Current.MainPage = new NavigationPage(new EventPage(eventTypeIDs));
            }
        }

        public static void OnSuggestLifeClicked(object sender, EventArgs e)
        {

        }
	}
}