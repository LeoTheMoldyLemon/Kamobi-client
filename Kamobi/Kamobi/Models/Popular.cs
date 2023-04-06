using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Kamobi.Models;

namespace Kamobi.Models
{
    public class Popular : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        private ObservableCollection<PopularRestaurant> popularRestaurants;

        public ObservableCollection<PopularRestaurant> popular_restaurants
        {
            get { return popularRestaurants; }
            set
            {
                popularRestaurants = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("popular_restaurants"));
            }
        }

        public Popular()
        {
            popular_restaurants = new ObservableCollection<PopularRestaurant>();
            addData();
        }

        private void addData()
        {
            popular_restaurants.Add(new PopularRestaurant
            {

                id = 0,
                title = "McDonalds",
                ponuda = "1 + 1 za 1.99€",
                imgSource = "https://mcdonalds.hr/media/McD_1_1_WEB_Slider_Mobile_mobile.gif"
            });
            popular_restaurants.Add(new PopularRestaurant
            {

                id = 0,
                title = "KFC",
                ponuda = "bucket za 4.99€",
                imgSource = "https://sawepecomcdn.blob.core.windows.net/kfc-web-ordering/KFC_CRO/26_CheeserPromo/recommends_b41/kfc_b4o_recommends_dexktop_581x581.jpg"
            });
        }
    }
}