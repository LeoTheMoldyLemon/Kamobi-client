using Kamobi.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Kamobi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            UsernameEntry.Focus();
            UsernameEntry.ReturnCommand = new Command(() => { PasswordEntry.Focus(); });
            PasswordEntry.ReturnCommand = new Command(()=> { LoginButtonClicked(null, null); });
        }
        
        private async void LoginButtonClicked(object sender, EventArgs e)
        {


            string loginname = UsernameEntry.Text.Replace("+", "");
            if (loginname.StartsWith("00")) {
                loginname = loginname.Replace("00", "");
            }
            string password = PasswordEntry.Text;
            PasswordEntry.Text = "";
            
            
            if (loginname.Length == 0)
            {
                Navigation.ShowPopup(new InfoPopup("Field can't be empty."));
                return;
            }
            if (password.Length == 0)
            {
                Navigation.ShowPopup(new InfoPopup("Please enter a password."));
                return;
            }
            string passwordHash = "";
            using (SHA256 sha256Hash = SHA256.Create())
            {
                passwordHash = Encoding.UTF8.GetString(sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password)));
            }
            LoadingPopup loading = new LoadingPopup();
            Navigation.ShowPopup(loading);
            var data = JsonNode.Parse("{}");
            data["loginname"] = loginname;
            data["password"] = passwordHash;
            data["rememberUUID"] = RememberCheckBox.IsChecked;
            JsonNode returnData = await App.socket.sendRequest("loginUser", data, 20000);
            loading.Dismiss(null);
            if (returnData == null) {
                Navigation.ShowPopup(new InfoPopup("Server response timed out. Please try again later."));
                return;
            }
            if (!(bool)returnData["success"]) {
                Navigation.ShowPopup(new InfoPopup((string)returnData["error"]["description"]));
                return;
            }

            App.User.id = (string)returnData["id"];
            App.User.username = (string)returnData["username"]; //if successfully logged in, remember user data and go to home page, skipping login and register entirely
            App.User.displayname = App.User.username.Substring(0, App.User.username.Length - 5);
            App.User.phoneNumber = (string)returnData["phoneNumber"];
            App.User.passwordHash = passwordHash;
            if ((bool)returnData["confirmedSMS"])
            {
                loading = new LoadingPopup();
                Navigation.ShowPopup(loading);
                await Shell.Current.GoToAsync("//HomePage");
                loading.Dismiss(null);
            }
            else
            {
                await Navigation.PushAsync(new SMSConfirmPage());
            }
            return;
        }
        private async void RegisterButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//RegisterPage");
        }
        private void ToggleHidePasswordClicked(object sender, EventArgs e)
        {
            PasswordEntry.IsPassword = !PasswordEntry.IsPassword;
            if (PasswordEntry.IsPassword) {
                ToggleHidePasswordButton.Source = "@drawable/eye_open";
            }
            else
            {
                ToggleHidePasswordButton.Source = "@drawable/eye_closed";
            }
        }
        private async void ForgotPasswordClicked(object sender, EventArgs e)
        {
            string phoneNumber = UsernameEntry.Text.Replace("+", "");
            if (phoneNumber.StartsWith("00"))
            {
                phoneNumber = phoneNumber.Replace("00", "");
            }
            if (phoneNumber.Length == 0)
            {
                Navigation.ShowPopup(new InfoPopup("Field can't be empty."));
                return;
            }
            LoadingPopup loading = new LoadingPopup();
            Navigation.ShowPopup(loading);
            var data = JsonNode.Parse("{}");
            data["phoneNumber"] = phoneNumber;
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
            App.User.phoneNumber = phoneNumber;
            await Navigation.PushAsync(new ForgotPasswordSMSPage());
        }
    }
}