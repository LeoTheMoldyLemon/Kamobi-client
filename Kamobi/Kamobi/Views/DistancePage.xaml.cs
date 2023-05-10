using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kamobi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DistancePage : ContentPage
    {
        public DistancePage()
        {
            InitializeComponent();
        }

        private async void LeonardaTierlist_Clicked(object sender, EventArgs e) /*remove kasije*/
        {
            await Shell.Current.GoToAsync("//TierlistPage");
        }
    }
}