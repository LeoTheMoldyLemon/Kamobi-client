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
    public partial class LoadingPopup : Popup
    {
        public LoadingPopup()
        {
            InitializeComponent();
            this.BackgroundColor = new Color(0, 0, 0, 0.4);

        }

        private void dismissButton_Clicked(object sender, EventArgs e)
        {
            Dismiss(null);
        }
    }
}