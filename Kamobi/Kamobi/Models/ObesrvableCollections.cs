using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Kamobi.Models;

namespace Kamobi.Models
{
    public class ObesrvableCollections : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        private ObservableCollection<PopularRestaurant> LocalPopularRestaurants;
        private ObservableCollection<Category> LocalCategories;


        public ObesrvableCollections()
        {
            PopularRestaurants = new ObservableCollection<PopularRestaurant>();
            Categories = new ObservableCollection<Category>();

            AddData();
        }
        public ObservableCollection<PopularRestaurant> PopularRestaurants
        {
            get { return LocalPopularRestaurants; }
            set
            {
                LocalPopularRestaurants = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("popular_restaurants"));
            }
        }
        public ObservableCollection<Category> Categories
        {
            get { return LocalCategories; }
            set
            {
                LocalCategories = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("categories"));
            }
        }


        private void AddData()
        {
            PopularRestaurants.Add(new PopularRestaurant
            {

                id = 0,
                title = "McDonalds",
                ponuda = "1 + 1 za 1.99€",
                imgSource = "https://mcdonalds.hr/media/McD_1_1_WEB_Slider_Mobile_mobile.gif"
            });
            PopularRestaurants.Add(new PopularRestaurant
            {

                id = 0,
                title = "KFC",
                ponuda = "bucket za 4.99€",
                imgSource = "https://sawepecomcdn.blob.core.windows.net/kfc-web-ordering/KFC_CRO/26_CheeserPromo/recommends_b41/kfc_b4o_recommends_dexktop_581x581.jpg"
            });
            Categories.Add(new Category
            {

                id = 0,
                title = "Burger",
                imgSource = "@drawable/burger.png"
            });
            Categories.Add(new Category
            {

                id = 0,
                title = "asian",
                imgSource = "@drawable/asian.png"
            });
        }

        
        

    }
}