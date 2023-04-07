using Kamobi.Services;
using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class LoadingPage : ContentPage
    {
        public LoadingPage()
        {
            InitializeComponent();
            Setup();
        }
        private async void Setup() //since this is the first actual page that loads, we have some preemptive work to do here
        {
            LoadingPopup loading = new LoadingPopup();
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "userInfo.json");
            Navigation.ShowPopup(loading);
            if (!(await App.socket.Connect(20000)))//connecting to the server
            {
                loading.Dismiss(null);
                Navigation.ShowPopup(new InfoPopup("Failed to connect to server, check your internet connection and try again.", true) { IsLightDismissEnabled = false });
                return;
            }
            loading.Dismiss(null);
            if (!File.Exists(fileName))
            { //if file doesnt exist, create it
                Console.WriteLine("Creating userInfo.json");
                File.WriteAllText(fileName, "{\"uuid\":\"" + Guid.NewGuid().ToString() + "\"}");
            }
            string jsonString = File.ReadAllText(fileName);
            Console.WriteLine(fileName + " JSON STRING: " + jsonString);
            var savedInfo = JsonNode.Parse(jsonString); //reading and parsing any saved login data
            if (savedInfo["uuid"] == null) {
                string JSONstring = "{\"uuid\":\"" + Guid.NewGuid().ToString() + "\"}";
                Console.WriteLine("Creating userInfo.json with content: "+ JSONstring);
                File.WriteAllText(fileName, JSONstring);
            }
             jsonString = File.ReadAllText(fileName);
            savedInfo = JsonNode.Parse(jsonString);
            LoadingPopup loading1 = new LoadingPopup();
            Navigation.ShowPopup(loading1);
            JsonNode returnData = await App.socket.sendRequest("loginUUID", savedInfo, 20000); //if there is login data, compile it and send it to the server
            loading1.Dismiss(null);
            if (returnData == null)
            {
                Navigation.ShowPopup(new InfoPopup("Server response timed out. Please try again later."){IsLightDismissEnabled=false });
                return;
            }
            if (!(bool)returnData["success"])
            {

                await Shell.Current.GoToAsync("//LoginPage");
                if ((int)returnData["error"]["code"] != 2 && (int)returnData["error"]["code"] != 3)
                {
                    Navigation.ShowPopup(new InfoPopup((string)returnData["error"]["description"])); //error handling
                }
                return;
            }
            App.UserInfo.id = (string)returnData["id"];
            App.UserInfo.username = (string)returnData["username"]; //if successfully logged in, remember user data and go to home page, skipping login and register entirely
            App.UserInfo.displayname = App.UserInfo.username.Substring(0, App.UserInfo.username.Length - 5);
            App.UserInfo.phoneNumber = (string)returnData["phoneNumber"];
            App.UserInfo.passwordHash = (string)savedInfo["password"];
            if (!(bool)returnData["confirmedSMS"])
            {
                DataManager.confirmationCode = (string)returnData["code"];
                await Navigation.PushAsync(new SMSConfirmPage());
                return;
            }
            await Shell.Current.GoToAsync("//HomePage");
            return;
            
        }
    }
}