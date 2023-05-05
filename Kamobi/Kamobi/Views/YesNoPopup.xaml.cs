using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.CommunityToolkit.UI.Views;
namespace Kamobi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class YesNoPopup : Popup
    {
        private bool exitApp;
        public YesNoPopup(string descriptionText)
        {
            InitializeComponent();
            description.Text = descriptionText;
        }

        private void noButton_Clicked(object sender, EventArgs e)
        {

            Dismiss(false);

        }
        private void yesButton_Clicked(object sender, EventArgs e)
        {

            Dismiss(true);

        }
    }
}