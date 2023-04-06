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
    public partial class SMSConfirmPage : ContentPage
    {
        public SMSConfirmPage()
        {
            InitializeComponent();
            string infotext = infoText.Text;
            infoText.Text = infotext.Replace("{phoneNumber}", UserInfo.phoneNumber);
        }

        private async void ConfirmButtonClicked(object sender, EventArgs e)
        {
            if (CodeEntry.Text != DataManager.confirmationCode)
            {
                Navigation.ShowPopup(new InfoPopup("Incorrect code, try again."));
                return;
            }
            else 
            {
                LoadingPopup loading = new LoadingPopup();
                Navigation.ShowPopup(loading); //displaying loading circle popup
                var data = JsonNode.Parse("{}");
                data["username"] = UserInfo.username;
                data["phoneNumber"] = UserInfo.phoneNumber;
                JsonNode returnData = await App.socket.sendRequest("registerUser", data, 20000); //sending a request to the server, waiting for response
                loading.Dismiss(null);
                if (returnData == null)
                {
                    Navigation.ShowPopup(new InfoPopup("Server response timed out. Please try again later."));
                    return;
                }
                if (!(bool)returnData["success"]) { //error handling
                    Navigation.ShowPopup(new InfoPopup((string)returnData["error"]["description"]));
                    if ((int)returnData["error"]["code"] == 2) {
                        await Navigation.PopAsync();
                    }
                    return;
                }
                await Navigation.PopAsync();
                string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "userInfo.json");
                var userdata = JsonNode.Parse("{}");
                userdata["loginname"] = UserInfo.username;
                userdata["password"] = UserInfo.passwordHash;
                File.WriteAllText(fileName, userdata.ToJsonString());
                Navigation.ShowPopup(new InfoPopup("You have successfully confirmed your phone number!"));
                await Shell.Current.GoToAsync("//HomePage");
                return;
            }
        }
        
    }
}