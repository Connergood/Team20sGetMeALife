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
        }

        class NavigationMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<NavigationMenuItem> MenuItems { get; set; }
            
            public NavigationMasterViewModel()
            {
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

        public void OnLogOutClicked(object sender, EventArgs e)
        {

        }

        public void OnNewLifeClicked(object sender, EventArgs e)
        {

        }

        public void OnNotifyChanged(object sender, PropertyChangedEventArgs e)
        {

        }

        public void OnPriceChanged(object sender, EventArgs e)
        {

        }

        public void OnRadiusChanged(object sender, EventArgs e)
        {

        }
    }
}