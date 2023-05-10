﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kamobi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilterPage : ContentPage
    {
        public FilterPage()
        {
            InitializeComponent();
        }

        private async void CloseFilter_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//ResultPage");
        }

        private async void ApplyFilter_Clicked(object sender, EventArgs e)
        {
            //dodati kod za primjeniti filter
            await Shell.Current.GoToAsync("//ResultPage"); 
        }
    }
}