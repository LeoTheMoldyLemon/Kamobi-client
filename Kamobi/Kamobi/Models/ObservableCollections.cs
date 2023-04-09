using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Kamobi.Models;

namespace Kamobi.Models
{
    public class ObservableCollections : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        private ObservableCollection<PopularRestaurant> LocalPopularRestaurants;
        private ObservableCollection<Category> LocalCategories;


        public ObservableCollections()
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
                title = "Pizza",
                imgSource = "@drawable/pizza.png"
            });

            /*Categories.Add(new Category
            {

                id = 0,
                title = "Pasta",
                imgSource = "@drawable/pasta.png"
            });*/

            Categories.Add(new Category
            {

                id = 0,
                title = "Fish",
                imgSource = "@drawable/fish.png"
            });

            Categories.Add(new Category
            {

                id = 0,
                title = "Healthy",
                imgSource = "@drawable/healthy.png"
            });

            /*Categories.Add(new Category
            {

                id = 0,
                title = "Croatian",
                imgSource = "@drawable/croatian.png"
            });*/

            Categories.Add(new Category
            {

                id = 0,
                title = "StreetFood",
                imgSource = "@drawable/streetfood.png"
            });

            Categories.Add(new Category
            {

                id = 0,
                title = "Asian",
                imgSource = "@drawable/asian.png"
            });

            Categories.Add(new Category
            {

                id = 0,
                title = "Mexican",
                imgSource = "@drawable/mexican.png"
            });

            Categories.Add(new Category
            {

                id = 0,
                title = "BBQ",
                imgSource = "@drawable/bbq.png"
            });

            Categories.Add(new Category
            {

                id = 0,
                title = "Steak",
                imgSource = "@drawable/steak.png"
            });

            Categories.Add(new Category
            {

                id = 0,
                title = "Dessert",
                imgSource = "@drawable/dessert.png"
            });

            Categories.Add(new Category
            {

                id = 0,
                title = "FastFood",
                imgSource = "@drawable/fastfood.png"
            });
            
            Categories.Add(new Category
            {

                id = 0,
                title = "Sandwich",
                imgSource = "@drawable/sandwich.png"
            });

            Categories.Add(new Category
            {

                id = 0,
                title = "Soup",
                imgSource = "@drawable/soup.png"
            });

            Categories.Add(new Category
            {

                id = 0,
                title = "Bakery",
                imgSource = "@drawable/bakery.png"
            });
            
            
        }

        
        

    }
}