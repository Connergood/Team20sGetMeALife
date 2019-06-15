using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GetMeALife.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavigationMaster : ContentPage
    {
        public ListView ListView;

        public NavigationMaster()
        {
            InitializeComponent();

            BindingContext = new NavigationMasterViewModel();
            ListView = MenuItemsListView;
        }

        class NavigationMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<NavigationMenuItem> MenuItems { get; set; }
            
            public NavigationMasterViewModel()
            {
                MenuItems = new ObservableCollection<NavigationMenuItem>(new[]
                {
                    new NavigationMenuItem { Id = 0, Title = "New Life" },
                    new NavigationMenuItem { Id = 1, Title = "Host" },
                    new NavigationMenuItem { Id = 2, Title = "Life Choices" },
                    new NavigationMenuItem { Id = 3, Title = "Log Out" }
                });
            }
            
            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }

        public static void OnLogOutClicked()
        {

        }

        public static void OnNewLifeClicked()
        {

        }

        public static void OnNotifyChanged()
        {

        }

        public static void OnPriceChanged()
        {

        }

        public static void OnRadiusChanged()
        {

        }
    }
}