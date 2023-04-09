using Kamobi.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kamobi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        private async void LogoutButtonClicked(object sender, EventArgs e)
        {
            LoadingPopup loading = new LoadingPopup();
            Navigation.ShowPopup(loading);
            var data = JsonNode.Parse("{}");
            JsonNode returnData = await App.socket.sendRequest("logoutUser", data, 20000);
            loading.Dismiss(null);
            await Shell.Current.GoToAsync("//LoginPage");
        }
        private async void ChangeUsernameButtonClicked(object sender, EventArgs e)
        {
            LoadingPopup loading = new LoadingPopup();
            Navigation.ShowPopup(loading);
            var data = JsonNode.Parse("{}");
            var random = new Random((int)DateTime.Now.Ticks);
            data["username"] = "noviUN" + ((random.Next(0, 10000)).ToString()).PadLeft(4, ' ');
            JsonNode returnData = await App.socket.sendRequest("updateUser", data, 20000);
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
            UserInfo.username = (string)data["username"];
            UserInfo.displayname = UserInfo.username.Substring(0, UserInfo.username.Length - 5);
        }
        private async void ChangePasswordButtonClicked(object sender, EventArgs e)
        {   
            string passwordHash, password1 = "ass", password2 = "ass";
            if (password1 != password2)
            {
                Navigation.ShowPopup(new InfoPopup("Passwords don't match."));
                return;
            }
            LoadingPopup loading = new LoadingPopup();
            Navigation.ShowPopup(loading);
            using (SHA256 sha256Hash = SHA256.Create()) //hashing password
            {
                passwordHash = Encoding.UTF8.GetString(sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password1)));
            }
            var data = JsonNode.Parse("{}");
            data["password"] = passwordHash;
            JsonNode returnData = await App.socket.sendRequest("updateUser", data, 20000);
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
            UserInfo.username = (string)data["username"];
            UserInfo.displayname = UserInfo.username.Substring(0, UserInfo.username.Length - 5);
        }

    }
}