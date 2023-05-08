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
    public partial class PartyCreatePage : ContentPage
    {
        public PartyCreatePage()
        {
            InitializeComponent();
            BindingContext = App.CollectionVM;
        }


        private async void InviteButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//TierlistPage");
        }

    }
}