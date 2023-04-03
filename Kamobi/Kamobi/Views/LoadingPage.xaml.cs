﻿using Kamobi.Services;
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
                Navigation.ShowPopup(new InfoPopup("Failed to connect to server, check your internet connection and try again.", true));
                return;
            }
            loading.Dismiss(null);
            if (!File.Exists(fileName))
            { //if file doesnt exist, create it
                Console.WriteLine("Creating userInfo.json");
                File.WriteAllText(fileName, "{\"loginname\":\"\",\"password\":\"\"}");
            }
            string jsonString = File.ReadAllText(fileName);
            Console.WriteLine(fileName + " JSON STRING: " + jsonString);
            var savedInfo = JsonNode.Parse(jsonString); //reading and parsing any saved login data
            if (savedInfo["loginname"] != null && savedInfo["password"] != null)
            {
                LoadingPopup loading1 = new LoadingPopup();
                Navigation.ShowPopup(loading1);
                JsonNode returnData = await App.socket.sendRequest("loginUser", savedInfo, 20000); //if there is login data, compile it and send it to the server
                loading1.Dismiss(null);
                if (returnData == null)
                {
                    Navigation.ShowPopup(new InfoPopup("Server response timed out. Please try again later."){IsLightDismissEnabled=false });
                    return;
                }
                if (!(bool)returnData["success"])
                {

                    await Shell.Current.GoToAsync("//LoginPage");
                    if ((int)returnData["error"]["code"] != 2)
                    {
                        Navigation.ShowPopup(new InfoPopup((string)returnData["error"]["description"])); //error handling
                    }
                    return;
                }
                UserInfo.username = (string)returnData["username"]; //if successfully logged in, remember user data and go to home page, skipping login and register entirely
                UserInfo.email = (string)returnData["email"];
                UserInfo.passwordHash = (string)savedInfo["password"];
                if (!(bool)returnData["confirmedEmail"])
                {
                    await Shell.Current.GoToAsync("//LoginPage");
                    await Navigation.PushAsync(new EmailConfirmPage());
                    return;
                }
                await Shell.Current.GoToAsync("//HomePage");
                return;
            }
        }
    }
}