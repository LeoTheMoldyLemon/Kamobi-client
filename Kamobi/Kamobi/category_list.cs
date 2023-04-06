using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace Kamobi
{
    public class category_list : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
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
        public category_list()
        {
            categories = new ObservableCollection<category>();
            addData();
        }

        private void addData()
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

