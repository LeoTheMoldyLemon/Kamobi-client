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
    public partial class InfoPopup : Popup
    {
        private bool exitApp;
        public InfoPopup(string descriptionText, Boolean exitApp=false)
        {
            InitializeComponent();
            description.Text = descriptionText;
            dismissButton.Text = "Ok";
            this.exitApp = exitApp;
        }

        private void dismissButton_Clicked(object sender, EventArgs e)
        {
            if (exitApp)
            {
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
            Dismiss(null);
            
        }
    }
}