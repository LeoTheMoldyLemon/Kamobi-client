using Kamobi.Models;
using Kamobi.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kamobi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultPage : ContentPage
    {
        public ResultPage()
        {
            InitializeComponent();
            BindingContext = App.CollectionVM;
        }

        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//HomePage");
        }

        private async void FilterButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//FilterPage");
        }
    }
}