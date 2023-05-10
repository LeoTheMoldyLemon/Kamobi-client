using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Kamobi.Models
{
    public class Restaurant
    {
        public int id { get; set; }
        public double distance { get; set; }
        public double rating { get; set; }
        public double score { get; set; }
        public int price { get; set; }
        public string title { get; set; }
        public string link { get; set; }
        public string imgSource { get; set; }

        public ICommand OpenLinkCommand => new Command(OpenLink);

        private void OpenLink()
        {
            if (!string.IsNullOrEmpty(link))
            {
                Launcher.OpenAsync(new Uri(link));
            }
        }
    }
}
