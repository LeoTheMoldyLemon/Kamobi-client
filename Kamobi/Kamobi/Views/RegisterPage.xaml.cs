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
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
            UsernameEntry.ReturnCommand = new Command(() => { PhoneNumberEntry.Focus(); });
            PhoneNumberEntry.ReturnCommand = new Command(() => { PasswordEntry.Focus(); });
            PasswordEntry.ReturnCommand = new Command(() => { RegisterButtonClicked(null, null); });
        }
       
        private async void RegisterButtonClicked(object sender, EventArgs e)
        {
            var random = new Random((int)DateTime.Now.Ticks);
            string username = UsernameEntry.Text+"#"+((random.Next(0, 10000)).ToString()).PadLeft(4, ' '); //loading and checking data user has entered
            string password = PasswordEntry.Text;
            PasswordEntry.Text = "";
            string phoneNumber = PhoneNumberEntry.Text.Replace("+", "");
            if (phoneNumber.StartsWith("00"))
            {
                phoneNumber = phoneNumber.Remove(0, 2);
            }
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
            if (!Regex.IsMatch(phoneNumber, @"^[+]?[0-9]{11,12}$"))
            {
                Navigation.ShowPopup(new InfoPopup("Invalid phone number. Please use the \"+385 91 234 5678\" format."));
                return;
            }
            if (username.Length == 0)
            {
                Navigation.ShowPopup(new InfoPopup("Please enter a username."));
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
            data["username"] = username;
            data["password"] = passwordHash;
            data["phoneNumber"] = phoneNumber;
            JsonNode returnData = await App.socket.sendRequest("SMSConfirmation", data, 20000); //parsing and sending data to server
            loading.Dismiss(null);
            if (returnData == null) {
                Navigation.ShowPopup(new InfoPopup("Server response timed out. Please try again later."));
                return;
            }
            if (!(bool)returnData["success"]) { //response error handling
                Navigation.ShowPopup(new InfoPopup((string)returnData["error"]["description"]));
                return;
            }
            UserInfo.id= (string)returnData["id"];
            UserInfo.username = username; //remember user data for when they confirm phone number
            UserInfo.displayname = UserInfo.username.Substring(0, UserInfo.username.Length - 5);
            UserInfo.passwordHash = passwordHash;
            UserInfo.phoneNumber = phoneNumber;
            await Navigation.PushAsync(new SMSConfirmPage()); //send user to SMS confirmation page
        }
        private async void LoginButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
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
    }
    
}