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
    public partial class ForgotPasswordSMSPage : ContentPage
    {
        public ForgotPasswordSMSPage()
        {
            InitializeComponent();
            string infotext = infoText.Text;
            infoText.Text = infotext.Replace("{phoneNumber}", App.User.phoneNumber);
            CodeEntry.ReturnCommand = new Command(() => { ConfirmButtonClicked(null, null); });
        }

        private async void ConfirmButtonClicked(object sender, EventArgs e)
        {
            string password = PasswordEntry.Text;
            if (password.Length < 8) {
                Navigation.ShowPopup(new InfoPopup("Password must be at least 8 characters long."));
                return;
            }
            if (password.Length > 32)
            {
                Navigation.ShowPopup(new InfoPopup("Password can be at most 32 characters long."));
                return;
            }
            string passwordHash;
            using (SHA256 sha256Hash = SHA256.Create()) //hashing password
            {
                passwordHash = Encoding.UTF8.GetString(sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password)));
            }
            LoadingPopup loading = new LoadingPopup();
            Navigation.ShowPopup(loading); //displaying loading circle popup
            var data = JsonNode.Parse("{}");
            data["phoneNumber"] = App.User.phoneNumber;
            data["code"] = CodeEntry.Text;
            data["password"] = passwordHash;
            JsonNode returnData = await App.socket.sendRequest("newPassword", data, 20000); //sending a request to the server, waiting for response
            loading.Dismiss(null);
            if (returnData == null)
            {
                Navigation.ShowPopup(new InfoPopup("Server response timed out. Please try again later."));
                return;
            }
            if (!(bool)returnData["success"]) { //error handling
                Navigation.ShowPopup(new InfoPopup((string)returnData["error"]["description"]));
                return;
            }
            Navigation.ShowPopup(new InfoPopup("Your password has been successfully reset! You may now attempt to login."));
            await Shell.Current.GoToAsync("//LoginPage");
            return;
            
        }
        private void ToggleHidePasswordClicked(object sender, EventArgs e)
        {
            PasswordEntry.IsPassword = !PasswordEntry.IsPassword;
            if (PasswordEntry.IsPassword)
            {
                ToggleHidePasswordButton.Source = "@drawable/eye_open";
            }
            else
            {
                ToggleHidePasswordButton.Source = "@drawable/eye_closed";
            }
        }
        private async void LoginButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }

    }
}