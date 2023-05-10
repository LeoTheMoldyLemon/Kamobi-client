using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Kamobi.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Kamobi.Models
{
    public class ObservableCollections : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        private ThreadSafeObservableCollection<PopularRestaurant> LocalPopularRestaurants;
        private ThreadSafeObservableCollection<Restaurant> LocalRestaurants;
        private ThreadSafeObservableCollection<HotDeal> LocalHotDeals;
        private ThreadSafeObservableCollection<Category> LocalCategories;
        //private ThreadSafeObservableCollection<Review> LocalReviews;
        private ThreadSafeObservableCollection<Friend> LocalFriendsList;
        private ThreadSafeObservableCollection<Friend> LocalFriendRequestsList;
        private ThreadSafeObservableCollection<Friend> LocalMemberList;


        public ObservableCollections()
        {
            PopularRestaurants = new ThreadSafeObservableCollection<PopularRestaurant>();
            HotDeals = new ThreadSafeObservableCollection<HotDeal>();
            Categories = new ThreadSafeObservableCollection<Category>();
            //Reviews = new ThreadSafeObservableCollection<Review>();
            Restaurants = new ThreadSafeObservableCollection<Restaurant>();
            FriendsList = new ThreadSafeObservableCollection<Friend>();
            FriendRequestsList = new ThreadSafeObservableCollection<Friend>();
            MemberList = new ThreadSafeObservableCollection<Friend>();

            AddData();
        }
        public ThreadSafeObservableCollection<Friend> MemberList
        {
            get { return LocalMemberList; }
            set
            {
                LocalMemberList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MemberList"));
            }
        }
        public ThreadSafeObservableCollection<Friend> FriendRequestsList
        {
            get { return LocalFriendRequestsList; }
            set
            {
                LocalFriendRequestsList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FriendRequestsList"));
            }
        }
        public ThreadSafeObservableCollection<Friend> FriendsList
        {
            get { return LocalFriendsList; }
            set
            {
                LocalFriendsList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FriendsList"));
            }
        }
        public ThreadSafeObservableCollection<PopularRestaurant> PopularRestaurants
        {
            get { return LocalPopularRestaurants; }
            set
            {
                LocalPopularRestaurants = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PopularRestaurants"));
            }
        }
        public ThreadSafeObservableCollection<Restaurant> Restaurants
        {
            get { return LocalRestaurants; }
            set
            {
                LocalRestaurants = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Restaurants"));
            }
        }
        public ThreadSafeObservableCollection<HotDeal> HotDeals
        {
            get { return LocalHotDeals; }
            set
            {
                LocalHotDeals = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HotDeals"));
            }
        }

        public ThreadSafeObservableCollection<Category> Categories
        {
            get { return LocalCategories; }
            set
            {
                LocalCategories = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Categories"));
            }
        }

        //public ThreadSafeObservableCollection<Review> Reviews
        //{
        //    get { return LocalReviews; }
        //    set
        //    {
        //        LocalReviews = value;
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Reviews"));
        //    }
        //}

        private async void OnImageButtonClicked(object sender, EventArgs e)
        {
            var imageButton = (ImageButton)sender;
            var item = imageButton.BindingContext as PopularRestaurant;
            if (item != null && !string.IsNullOrEmpty(item.link))
            {
                await Launcher.OpenAsync(new Uri(item.link));
            }
        }

        private void AddData()
        {
            PopularRestaurants.Add(new PopularRestaurant
            {

                id = 0,
                title = "Chef Mate Janković pokreće novi projekt: ‘Imat ćemo 12 novih jela svaki tjedan‘",
                link = "https://www.jutarnji.hr/dobrahrana/price/chef-mate-jankovic-pokrece-novi-projekt-imat-cemo-12-novih-jela-svaki-tjedan-15296789",
                imgSource = "https://static.jutarnji.hr/images/slike/2023/01/18/27642993.jpg"
            });
            PopularRestaurants.Add(new PopularRestaurant
            {

                id = 0,
                title = "PROSLAVITE ROĐENDAN U BATKU!",
                link = "https://batak-grill.hr/proslavite-rodendan-u-batku/",
                imgSource = "https://batak-grill.hr/wp-content/uploads/2022/10/Batak-6029-1024x683.webp"
            });
            PopularRestaurants.Add(new PopularRestaurant
            {

                id = 0,
                title = "HOT CHALLANGE, ALI NA SUBMARINE NAČIN!",
                link = "https://submarineburger.com/",
                imgSource = "https://lh5.googleusercontent.com/p/AF1QipOr8B9YwvkmdCnuZIRXm_iHwNsDswptFJ-9W-E=w325-h218-n-k-no"
            });

            Restaurants.Add(new Restaurant
            {

                id = 0,
                rating = 4.5,
                distance = 0.8,
                score = 4.6,
                price = 0,
                title = "Pizza Bar Fratelli",
                link = "https://www.google.com/maps/place/Pizza+Bar+Fratelli/@45.7922026,16.0011225,16z/data=!4m6!3m5!1s0x4765d76d33043781:0x4f13421e42cfc015!8m2!3d45.7884113!4d16.0066622!16s%2Fg%2F11rv96lj73",
                imgSource = "https://lh5.googleusercontent.com/p/AF1QipP59p5u2w8ti9rsgM06oumCzWBtleBS9TgprMKM=w426-h240-k-no"
            });

            Restaurants.Add(new Restaurant
            {

                id = 0,
                rating = 4.5,
                distance = 1.2,
                score = 3.6,
                price = 2,
                title = "Submarine Burger Bogovićeva",
                link = "https://www.google.com/maps/place/Submarine+Burger+Bogovi%C4%87eva/@45.7712118,15.8041667,12z/data=!4m6!3m5!1s0x4765d6fc5415e05b:0xb63cee1b8dbbc06d!8m2!3d45.811938!4d15.9749036!16s%2Fg%2F11c30y056h",
                imgSource = "https://submarineburger.com/wp-content/uploads/2022/10/Submarine-burger-hot-burger-ninja-contest_2-938x385.webp"
            });

            Restaurants.Add(new Restaurant
            {

                id = 0,
                rating = 4.3,
                distance = 0.4,
                score = 1.6,
                price = 2,
                title = "McDonald's Kruge",
                link = "https://www.google.com/maps/place/McDonald's+Kruge/@45.7958301,15.9836195,15z/data=!4m14!1m7!3m6!1s0x4765d6fc5415e05b:0xb63cee1b8dbbc06d!2sSubmarine+Burger+Bogovi%C4%87eva!8m2!3d45.811938!4d15.9749036!16s%2Fg%2F11c30y056h!3m5!1s0x4765d64306eea911:0x3d042a1e4966c1dc!8m2!3d45.7958138!4d15.9918038!16s%2Fg%2F11bx5v0y2s",
                imgSource = "https://submarineburger.com/wp-content/uploads/2022/10/Submarine-burger-hot-burger-ninja-contest_2-938x385.webp"
            });

            Restaurants.Add(new Restaurant
            {

                id = 0,
                rating = 4.4,
                distance = 0.4,
                score = 1.7,
                price = 2,
                title = "McDonald's Felix (Heinzelova)",
                link = "https://www.google.com/maps/place/McDonald's+Felix+(Heinzelova)/@45.7907158,15.9947191,14.44z/data=!4m16!1m9!3m8!1s0x4765d76d33043781:0x4f13421e42cfc015!2sPizza+Bar+Fratelli!8m2!3d45.7884113!4d16.0066622!9m1!1b1!16s%2Fg%2F11rv96lj73!3m5!1s0x4765d636e93b595f:0x4358a2d30906e65!8m2!3d45.8030989!4d16.0108818!16s%2Fg%2F1260j1_54",
                imgSource = "https://lh5.googleusercontent.com/p/AF1QipPAzuabn1zQRdsMQkh0rEpZdrGEsDV5zYs9gSAl=w408-h272-k-no"
            });

            HotDeals.Add(new HotDeal
            {
                id = 0,
                title = "McDonalds ponuda 1+1",
                link = "https://mcdonalds.hr/o-nama/novosti/11-ponuda-uzitak-koji-vrijedi-vise/",
                imgSource = "https://mcdonalds.hr/media/McD_1_1_WEB_Slider_Mobile_mobile.gif"
            });

            HotDeals.Add(new HotDeal
            {
                id = 0,
                title = "KFC bucket za jednu osobu",
                link = "https://kfc.hr/",
                imgSource = "https://sawepecomcdn.blob.core.windows.net/kfc-web-ordering/KFC_CRO/26_CheeserPromo/recommends_b41/kfc_b4o_recommends_dexktop_581x581.jpg"
            });

            HotDeals.Add(new HotDeal
            {
                id = 0,
                title = "MENU ZA KLINCE; KIDS EAT FREE SUNDAY",
                link = "https://submarineburger.com/menu/",
                imgSource = "https://submarineburger.com/wp-content/uploads/2020/11/Kiddie%C2%B4s-menu-558x373.webp"
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

            Categories.Add(new Category
            {

                id = 0,
                title = "Pasta",
                imgSource = "@drawable/pasta.png"
            });

            Categories.Add(new Category
            {

                id = 0,
                title = "Fish",
                imgSource = "@drawable/fish.png"
            });

            Categories.Add(new Category
            {

                id = 0,
                title = "Healthy Food",
                imgSource = "@drawable/healthy.png"
            });

            Categories.Add(new Category
            {

                id = 0,
                title = "Croatian",
                imgSource = "@drawable/croatian.png"
            });

            Categories.Add(new Category
            {

                id = 0,
                title = "Street Food",
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
                title = "Fast Food",
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
                title = "Pastry",
                imgSource = "@drawable/bakery.png"
            });
        }
    }
}