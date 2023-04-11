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
            infoText.Text = infotext.Replace("{phoneNumber}", App.User.phoneNumber);
            CodeEntry.ReturnCommand = new Command(() => { ConfirmButtonClicked(null, null); });
        }

        private async void ConfirmButtonClicked(object sender, EventArgs e)
        {
            LoadingPopup loading = new LoadingPopup();
            Navigation.ShowPopup(loading); //displaying loading circle popup
            var data = JsonNode.Parse("{}");
            data["id"] = App.User.id;
            data["code"] = CodeEntry.Text;
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
            Navigation.ShowPopup(new InfoPopup("You have successfully confirmed your phone number!"));
            await Shell.Current.GoToAsync("//HomePage");
            return;
            
        }
        private async void RegisterButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//RegisterPage");
        }

    }
}