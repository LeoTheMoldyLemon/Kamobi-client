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
    public partial class WaitingPage : ContentPage
    {
        public WaitingPage()
        {
            InitializeComponent();
        }

        private async void LeonardaResult_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//ResultPage");
        }
    }
}