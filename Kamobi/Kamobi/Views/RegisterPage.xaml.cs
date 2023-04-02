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
        }
       
        private async void RegisterButtonClicked(object sender, EventArgs e)
        {


            string username = UsernameEntry.Text; //loading and checking data user has entered
            string password = PasswordEntry.Text;
            PasswordEntry.Text = "";
            string email = EmailEntry.Text;
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
            if (!Regex.IsMatch(email, @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$"))
            {
                Navigation.ShowPopup(new InfoPopup("Invalid email adress."));
                return;
            }
            if (username.Length == 0)
            {
                Navigation.ShowPopup(new InfoPopup("Please enter a username."));
                return;
            }
            string passwordHash = "";
            using (SHA256 sha256Hash = SHA256.Create()) //hashing password
            {
                passwordHash = Encoding.UTF8.GetString(sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password)));
            }
            LoadingPopup loading = new LoadingPopup();
            Navigation.ShowPopup(loading); //displaying loading circle popup
            var data = JsonNode.Parse("{}");
            data["username"] = username;
            data["password"] = passwordHash;
            data["email"] = email;
            JsonNode returnData = await App.socket.sendRequest("emailConfirmation", data, 20000); //parsing and sending data to server
            loading.Dismiss(null);
            if (returnData == null) {
                Navigation.ShowPopup(new InfoPopup("Server response timed out. Please try again later."));
                return;
            }
            if (!(bool)returnData["success"]) { //response error handling
                Navigation.ShowPopup(new InfoPopup((string)returnData["error"]["description"]));
                return;
            }
            DataManager.confirmationCode = (string)returnData["code"];
            UserInfo.username = username; //remember user data for when they confirm email
            UserInfo.passwordHash = passwordHash;
            UserInfo.email = email;
            await Navigation.PushAsync(new EmailConfirmPage()); //send user to email confirmation page
        }
        private async void LoginButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}