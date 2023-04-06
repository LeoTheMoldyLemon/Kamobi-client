using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace Kamobi
{
    public class Popular : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        private ObservableCollection<Popularni_restorani> Popularni_restorani;

        public ObservableCollection<Popularni_restorani> popularni_restorani
        {
            get { return Popularni_restorani; }
            set
            {
                Popularni_restorani = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("popularni_restorani"));
            }
        }

        public Popular()
        {
            popularni_restorani = new ObservableCollection<Popularni_restorani>();

            categories = new ObservableCollection<category>();
            addData();
            addDataCategories();
        }

        private void addData()
        {
            popularni_restorani.Add(new Popularni_restorani
            {

                id = 0,
                title = "McDonalds",
                ponuda = "1 + 1 za 1.99€",
                imgSource = "https://mcdonalds.hr/media/McD_1_1_WEB_Slider_Mobile_mobile.gif"
            });
            popularni_restorani.Add(new Popularni_restorani
            {

                id = 0,
                title = "KFC",
                ponuda = "bucket za 4.99€",
                imgSource = "https://sawepecomcdn.blob.core.windows.net/kfc-web-ordering/KFC_CRO/26_CheeserPromo/recommends_b41/kfc_b4o_recommends_dexktop_581x581.jpg"
            });

        }

        private ObservableCollection<category> category;
        public ObservableCollection<category> categories
        {
            get { return category; }
            set
            {
                category = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("categories"));
            }
        }
        

        private void addDataCategories()
        {
            categories.Add(new category
            {

                id = 0,
                title = "Burger",
                imgSource = "@drawable/burger.png"
            });
            categories.Add(new category
            {

                id = 0,
                title = "asian",
                imgSource = "@drawable/asian.png"
            });
        }
    }
}