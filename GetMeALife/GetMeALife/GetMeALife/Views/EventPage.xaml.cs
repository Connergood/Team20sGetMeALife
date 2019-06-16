using GetMeALibrary.Model;
using GetMeALife.ViewModels;
using GetMeALifeLibrary.Api;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GetMeALife.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventPage : ContentPage
    {
        public ListView ListView;
        public ObservableCollection<EventDetailViewModel> eventList = new ObservableCollection<EventDetailViewModel>();
        List<int> eventTypeIDs = new List<int>();
        public EventPage(List<int> typeIDs)
        {
            InitializeComponent();
            ListView = lstEvents;
            ListView.ItemTapped += OnEventClicked;
            eventList.CollectionChanged += OnEventListChanged;
            eventTypeIDs = typeIDs;
            LoadEvents();
        }

        public void LoadEvents()
        {
            // Grab all events of types
            var events = Api.GetList<Event>("http://getmealife.azurewebsites.net/graphql", new Event()).Where(e => !eventTypeIDs.Contains(e.eventtypeid));

            if (events != null)
            {
                List<EventDetailViewModel> eventModels = new List<EventDetailViewModel>();
                foreach (var e in events)
                {
                    var eventModel = new EventDetailViewModel();
                    eventModel.eventDetail = new EventDetail();
                    eventModel.eventDetail.accessibility = e.accessibility;
                    eventModel.eventDetail.description = e.description;
                    eventModel.eventDetail.eventdate = e.eventdate;
                    eventModel.eventDetail.eventend = e.eventend;
                    eventModel.eventDetail.eventstart = e.eventstart;
                    eventModel.eventDetail.eventtypeid = e.eventtypeid;
                    eventModel.eventDetail.id = e.id;
                    eventModel.eventDetail.latitude = e.latitude;
                    eventModel.eventDetail.locationaddress = e.locationaddress;
                    eventModel.eventDetail.locationname = e.locationname;
                    eventModel.eventDetail.longitude = e.longitude;
                    eventModel.eventDetail.name = e.name;
                    eventModel.eventDetail.participants = e.participants;
                    eventModel.eventDetail.price = e.price;
                    eventModels.Add(eventModel);
                }
                ignore_OnEventListChanged = true;
                for (int i = 0; i < eventModels.Count; i++)
                {
                    if (i == eventModels.Count - 1)
                        ignore_OnEventListChanged = false;
                    eventList.Add(eventModels[i]);
                } 
            }
        }

        protected override void OnBindingContextChanged()
        {
            //Don't remove this
            base.OnBindingContextChanged();

            //eventList = (ObservableCollection<EventDetailViewModel>)BindingContext;
        }

        public static void OnEventClicked(object sender, ItemTappedEventArgs e)
        {
            try
            {
                var listView = (ListView)sender;

                if (listView.SelectedItem == null)
                    return;

                var selectedEvent = (EventDetailViewModel)listView.SelectedItem;

                Application.Current.MainPage.Navigation.PushAsync(new EventDetailPage(selectedEvent));
            }
            catch (Exception ex)
            {
            }
        }

        private bool ignore_OnEventListChanged = false;
        public void OnEventListChanged(object sender, EventArgs e)
        {
            if (ignore_OnEventListChanged) return;   
            lstEvents.ItemsSource = eventList;
            OnBindingContextChanged();
        }
    }
}