using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kamobi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            UsernameEntry.ReturnCommand = new Command(() => { ChangeUsernameButtonClicked(null, null); });
        }

        private async void ChangeUsernameButtonClicked(object sender, EventArgs e)
        {
            LoadingPopup loading = new LoadingPopup();
            Navigation.ShowPopup(loading);
            var data = JsonNode.Parse("{}");
            var random = new Random((int)DateTime.Now.Ticks);
            data["username"] = UsernameEntry.Text + "#" + random.Next(0, 10000).ToString().PadLeft(4, ' ');
            UsernameEntry.Text = "";
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
            App.User.username = (string)data["username"];
            App.User.displayname = App.User.username.Substring(0, App.User.username.Length - 5);
            Navigation.ShowPopup(new InfoPopup("You have successfully changed your username!"));
            await Shell.Current.GoToAsync("//ProfilePage");
        }

        private async void ChangePasswordButtonClicked(object sender, EventArgs e)
        {
            /*string passwordHash, password = PasswordEntry.Text;
            if (password.Length < 8)
            {
                Navigation.ShowPopup(new InfoPopup("Password length must be at least 8."));
                return;
            }
            if (password.Length > 32)
            {
                Navigation.ShowPopup(new InfoPopup("Password length must be no more than 32."));
                return;
            }
            
            LoadingPopup loading = new LoadingPopup();
            Navigation.ShowPopup(loading);
            using (SHA256 sha256Hash = SHA256.Create()) //hashing password
            {
                passwordHash = Encoding.UTF8.GetString(sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password)));
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
            }*/
            LoadingPopup loading = new LoadingPopup();
            Navigation.ShowPopup(loading);
            var data = JsonNode.Parse("{}");
            data["id"] = App.User.id;
            JsonNode returnData = await App.socket.sendRequest("sendSMS", data, 20000);
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
            await Navigation.PushAsync(new ChangePasswordSMSPage());
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

        private async void ChangePhoneNumberClicked(object sender, EventArgs e) {
            string phoneNumber = PhoneNumberEntry.Text.Replace("+", "");
            if (phoneNumber.StartsWith("00"))
            {
                phoneNumber = phoneNumber.Remove(0, 2);
            }
            if (!Regex.IsMatch(phoneNumber, @"^[+]?[0-9]{11,12}$"))
            {
                Navigation.ShowPopup(new InfoPopup("Invalid phone number. Please use the \"+385 91 234 5678\" format."));
                return;
            }
            LoadingPopup loading = new LoadingPopup();
            Navigation.ShowPopup(loading); //displaying loading circle popup
            var data = JsonNode.Parse("{}");
            data["phoneNumber"] = phoneNumber;
            data["id"] = App.User.id;
            JsonNode returnData = await App.socket.sendRequest("sendSMS", data, 20000); //parsing and sending data to server
            loading.Dismiss(null);
            if (returnData == null)
            {
                Navigation.ShowPopup(new InfoPopup("Server response timed out. Please try again later."));
                return;
            }
            if (!(bool)returnData["success"])
            { //response error handling
                Navigation.ShowPopup(new InfoPopup((string)returnData["error"]["description"]));
                return;
            }

            PhoneNumberEntry.Text = "";
            await Navigation.PushAsync(new ChangePhoneNumberSMSPage(phoneNumber));
        }
        private async void BackButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//ProfilePage");
        }
    }
}