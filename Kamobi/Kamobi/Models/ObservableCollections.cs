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
                link = "https://submarineburger.com/2022/10/27/hot-challange-ali-na-submarine-nacin/",
                imgSource = "https://submarineburger.com/wp-content/uploads/2022/10/Submarine-burger-hot-burger-ninja-contest_2-938x385.webp"
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