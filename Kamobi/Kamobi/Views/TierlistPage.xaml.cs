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
    public partial class TierlistPage : ContentPage
    {
       
        public TierlistPage()
        {
            InitializeComponent();
            BindingContext = App.CollectionVM;
        }
        private void DragGestureRecognizer_DragStarting(object sender, DragStartingEventArgs e)
        {
            e.Data.Properties.Add("Element", (sender as Element).Parent as Frame);
        }

        private async void FinishButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//ProfilePage");
        }

        private void DropGestureRecognizer_Drop(object sender, DropEventArgs e)
        {
           
            ((StackLayout)(sender as Element).Parent).Children.Add((Frame)e.Data.Properties["Element"]);
        }

        private async void LeonardaWaiting_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//WaitingPage");
        }
    }
}