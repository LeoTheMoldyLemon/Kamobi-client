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
        private ThreadSafeObservableCollection<Category> LocalCategories;
        private ThreadSafeObservableCollection<Review> LocalReviews;
        private ThreadSafeObservableCollection<Friend> LocalFriendsList;
        private ThreadSafeObservableCollection<Friend> LocalFriendRequestsList;
        private ThreadSafeObservableCollection<Friend> LocalMemberList;


        public ObservableCollections()
        {
            PopularRestaurants = new ThreadSafeObservableCollection<PopularRestaurant>();
            Categories = new ThreadSafeObservableCollection<Category>();
            Reviews = new ThreadSafeObservableCollection<Review>();
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
        public ThreadSafeObservableCollection<Category> Categories
        {
            get { return LocalCategories; }
            set
            {
                LocalCategories = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Categories"));
            }
        }

        public ThreadSafeObservableCollection<Review> Reviews
        {
            get { return LocalReviews; }
            set
            {
                LocalReviews = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Reviews"));
            }
        }

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
                title = "McDonalds",
                link = "https://mcdonalds.hr/o-nama/novosti/11-ponuda-uzitak-koji-vrijedi-vise/",
                imgSource = "https://mcdonalds.hr/media/McD_1_1_WEB_Slider_Mobile_mobile.gif"
            });
            PopularRestaurants.Add(new PopularRestaurant
            {

                id = 0,
                title = "KFC",
                link = "https://kfc.hr/",
                imgSource = "https://sawepecomcdn.blob.core.windows.net/kfc-web-ordering/KFC_CRO/26_CheeserPromo/recommends_b41/kfc_b4o_recommends_dexktop_581x581.jpg"
            });

            Reviews.Add(new Review
            { 
                id = 0,
                name = "Leonardin restoran",
                rating = 1,
                imgSource = "https://cdn.vox-cdn.com/thumbor/WR9hE8wvdM4hfHysXitls9_bCZI=/0x0:1192x795/1400x1400/filters:focal(596x398:597x399)/cdn.vox-cdn.com/uploads/chorus_asset/file/22312759/rickroll_4k.jpg"
            });

            Reviews.Add(new Review
            {
                id = 0,
                name = "Leonardin restoran 2",
                rating = 5,
                imgSource = "https://s.yimg.com/ny/api/res/1.2/gts9lRWMvRWAcVXhlnODCA--/YXBwaWQ9aGlnaGxhbmRlcjt3PTk2MDtoPTU0MQ--/https://media.zenfs.com/en/nerdist_761/2d47d0794ed390d7807134077817ca40"
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