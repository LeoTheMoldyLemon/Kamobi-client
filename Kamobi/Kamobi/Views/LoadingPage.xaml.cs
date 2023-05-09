using Kamobi.Models;
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
            App.socket.setupListener("partyMemberUpdate", (dataString) =>
            {

                var data = JsonArray.Parse(JsonArray.Parse(dataString.ToString())[0].ToString()).AsArray();
                App.CollectionVM.MemberList = new ThreadSafeObservableCollection<Friend>();
                foreach (JsonNode datanode in data)
                {
                    Console.WriteLine(datanode.ToJsonString());
                    App.CollectionVM.MemberList.Add(new Friend
                    {
                        id = (string)datanode["id"],
                        username = (string)datanode["username"],
                        displayname = ((string)datanode["username"]).Substring(0, ((string)datanode["username"]).Length - 5),
                        leader=(bool)datanode["leader"]
                    });
                    
                }
            });
            App.socket.setupListener("partyRequest", async (dataString) =>
            {

            var data = JsonArray.Parse(JsonArray.Parse(dataString.ToString())[0].ToString());
            Console.WriteLine(dataString);
            App.CollectionVM.MemberList = new ThreadSafeObservableCollection<Friend>();
            Console.WriteLine(data.ToJsonString());
            Device.BeginInvokeOnMainThread(async() =>  {
                if ((bool)await Navigation.ShowPopupAsync(new YesNoPopup(data["username"] + " has invited you to their KamopParty! Would you like to join?"))) {
                    LoadingPopup loading2 = new LoadingPopup();
                    Navigation.ShowPopup(loading2);
                    JsonNode returnData2 = await App.socket.sendRequest("acceptPartyRequest", data, 20000);
                    loading2.Dismiss(null);
                    if (returnData2 == null)
                    {
                        Navigation.ShowPopup(new InfoPopup("Server response timed out. Please try again later."));
                        return;
                    }
                    if (!(bool)returnData2["success"])
                    {
                        Navigation.ShowPopup(new InfoPopup((string)returnData2["error"]["description"]));
                        return;
                    }
                    await Shell.Current.GoToAsync("//PartyMemberPage");
                }
            });



            });
            App.socket.setupListener("friendsListUpdate", (dataString) =>
            {
                
                var data = JsonArray.Parse(JsonArray.Parse(dataString.ToString())[0].ToString()).AsArray();
                App.CollectionVM.FriendsList = new ThreadSafeObservableCollection<Friend>();
                App.CollectionVM.FriendRequestsList = new ThreadSafeObservableCollection<Friend>();
                foreach (JsonNode datanode in data)
                {
                    Console.WriteLine(datanode.ToJsonString());
                    if ((bool)datanode["accepted"])
                    {
                        App.CollectionVM.FriendsList.Add(new Friend
                        {
                            id = (string)datanode["id"],
                            username = (string)datanode["username"],
                            displayname = ((string)datanode["username"]).Substring(0, ((string)datanode["username"]).Length - 5)
                        });
                    }
                    else
                    {
                        App.CollectionVM.FriendRequestsList.Add(new Friend
                        {
                            id = (string)datanode["id"],
                            username = (string)datanode["username"],
                            displayname = ((string)datanode["username"]).Substring(0, ((string)datanode["username"]).Length - 5)
                        });
                    }
                }
            });
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
                Console.WriteLine((int)returnData["error"]["code"]);
                await Shell.Current.GoToAsync("//LoginPage");
                if ((int)returnData["error"]["code"] != 2 && (int)returnData["error"]["code"] != 3)
                {
                    Navigation.ShowPopup(new InfoPopup((string)returnData["error"]["description"])); //error handling
                }
                return;
            }
            App.User.id = (string)returnData["id"];
            App.User.username = (string)returnData["username"]; //if successfully logged in, remember user data and go to home page, skipping login and register entirely
            App.User.displayname = App.User.username.Substring(0, App.User.username.Length - 5);
            App.User.phoneNumber = (string)returnData["phoneNumber"];
            App.User.passwordHash = (string)returnData["password"];
            if (!(bool)returnData["confirmedSMS"])
            {
                await Navigation.PushAsync(new SMSConfirmPage());
                return;
            }
            await Shell.Current.GoToAsync("//HomePage");
            return;
            
        }
    }
}