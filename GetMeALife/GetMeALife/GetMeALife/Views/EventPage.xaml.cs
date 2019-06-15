using GetMeALife.ViewModels;
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
        public EventPage()
        {
            InitializeComponent();
            ListView = lstEvents;
            eventList.CollectionChanged += OnEventListChanged;
        }

        public static void LoadEvents(List<string> types, bool noRestriction = false)
        {
            // Find Me - No restriction, pull everything
            if (noRestriction)
            { return; }

            // Grab all events of types

        }

        protected override void OnBindingContextChanged()
        {
            //Don't remove this
            base.OnBindingContextChanged();

            eventList = (ObservableCollection<EventDetailViewModel>)BindingContext;
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

        public void OnEventListChanged(object sender, EventArgs e)
        {
            OnBindingContextChanged();
        }
    }
}