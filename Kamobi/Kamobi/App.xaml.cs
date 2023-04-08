using Kamobi.Services;
using Kamobi.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kamobi
{
    public partial class App : Application
    {
        public static Socket socket;
        public App()
        {
            string host = "http://kamobi-app.site:42069/";
            socket = new Socket(host);
            Console.WriteLine(host);
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
