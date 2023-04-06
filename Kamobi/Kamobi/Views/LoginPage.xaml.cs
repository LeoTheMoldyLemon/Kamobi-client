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


            string loginname = UsernameEntry.Text;
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
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "userInfo.json");
            
            UserInfo.username = (string)returnData["username"]; //if successfully logged in, remember user data and go to home page, skipping login and register entirely
            UserInfo.phoneNumber = (string)returnData["phoneNumber"];
            UserInfo.passwordHash = passwordHash;
            if ((bool)returnData["confirmedSMS"])
            {
                File.WriteAllText(fileName, data.ToJsonString());
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
    }
}