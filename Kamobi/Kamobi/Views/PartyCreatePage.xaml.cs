using Kamobi.Models;
using System;
using System.Collections.Generic;
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
    public partial class PartyCreatePage : ContentPage
    {
        public PartyCreatePage()
        {
            InitializeComponent();
            BindingContext = App.CollectionVM;
        }


        private async void InviteButton_Clicked(object sender, EventArgs e)
        {
            Friend target = (Friend)((Button)sender).CommandParameter;
            LoadingPopup loading = new LoadingPopup();
            Navigation.ShowPopup(loading);
            var data = JsonNode.Parse("{}");
            data["id"] = target.id;
            JsonNode returnData = await App.socket.sendRequest("sendPartyRequest", data, 20000);
            loading.Dismiss(null);
            if (returnData == null)
            {
                Navigation.ShowPopup(new InfoPopup("Server response timed out. Please try again later."));
                return;
            }
            if (!(bool)returnData["success"])
            {
                Navigation.ShowPopup(new InfoPopup((string)returnData["error"]["description"]));
                return;
            }
            Navigation.ShowPopup(new InfoPopup("Request sent to "+target.displayname));
            return;
        }

    }
}