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
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            
            InitializeComponent();
            BindingContext = App.CollectionVM;
            App.CollectionVM.PopularRestaurants.Add(new PopularRestaurant
            {

                id = 0,
                title = "McDonalds",
                link = "https://mcdonalds.hr/o-nama/novosti/11-ponuda-uzitak-koji-vrijedi-vise/",
                imgSource = "https://mcdonalds.hr/media/McD_1_1_WEB_Slider_Mobile_mobile.gif"
            });
        }
        
    }
}