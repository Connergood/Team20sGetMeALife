using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GetMeALife.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavigationDetail : ContentPage
    {
        public NavigationDetail()
        {
            InitializeComponent();
        }

        public static void OnConfirmClick()
        {

        }
    }
}