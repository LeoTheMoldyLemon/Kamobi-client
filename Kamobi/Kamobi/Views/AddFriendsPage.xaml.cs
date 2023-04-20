﻿using Kamobi.Models;
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

        }
    }
}