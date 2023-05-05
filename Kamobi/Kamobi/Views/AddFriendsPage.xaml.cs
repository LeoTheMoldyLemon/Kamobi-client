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
    public partial class AddFriendsPage : ContentPage
    {
        public AddFriendsPage()
        {
            InitializeComponent();
            BindingContext = App.CollectionVM;
            
            
        }


        private async void AddFriendClicked(object sender, EventArgs e)
        {
            string search = FriendRequestName.Text;
            if (search.Length == 0)
            {
                Navigation.ShowPopup(new InfoPopup("Please enter a valid username or phone number."));
                return;
            }
            LoadingPopup loading = new LoadingPopup();
            Navigation.ShowPopup(loading);
            var data = JsonNode.Parse("{}");
            data["search"] = search;
            JsonNode returnData = await App.socket.sendRequest("addFriend", data, 20000);
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
            Navigation.ShowPopup(new InfoPopup("Friend request has been sent."));
            FriendRequestName.Text = "";
        }

        private async void AcceptClicked(object sender, EventArgs e)
        {
            Friend target = (Friend)((ImageButton)sender).CommandParameter;
            LoadingPopup loading = new LoadingPopup();
            Navigation.ShowPopup(loading);
            var data = JsonNode.Parse("{}");
            data["id"] = target.id;
            JsonNode returnData = await App.socket.sendRequest("acceptFriend", data, 20000);
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
        }

        private async void DenyClicked(object sender, EventArgs e)
        {
            Friend target = (Friend)((ImageButton)sender).CommandParameter;
            LoadingPopup loading = new LoadingPopup();
            Navigation.ShowPopup(loading);
            var data = JsonNode.Parse("{}");
            data["id"] = target.id;
            JsonNode returnData = await App.socket.sendRequest("denyFriend", data, 20000);
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
        }

        private async void UnfriendClicked(object sender, EventArgs e)
        {
            Friend target = (Friend)((ImageButton)sender).CommandParameter;

            if (!(bool)await Navigation.ShowPopupAsync(new YesNoPopup("Are you sure you want to unfriend "+target.username+"?"))) {
                return;
            }
            LoadingPopup loading = new LoadingPopup();
            Navigation.ShowPopup(loading);
            var data = JsonNode.Parse("{}");
            data["id"] = target.id;
            JsonNode returnData = await App.socket.sendRequest("removeFriend", data, 20000);
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
        }
    }
}