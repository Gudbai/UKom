using Android.App;
using Android.Content.PM;
using Android.OS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;
using Rg.Plugins.Popup.Events;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Extensions;

namespace CRMApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();

            btnScale = BtnSettings.Scale;
        }

        private double btnScale;
        private async void OnSettingsClick(object sender, EventArgs e)
        {
            ImageButton button = (ImageButton)sender;

            await button.ScaleTo(btnScale - 0.1, 100);
            button.BackgroundColor = Color.Gray;

            await button.ScaleTo(btnScale, 100);
            button.BackgroundColor = Color.Transparent;

            await Navigation.PushPopupAsync(new HomePopupPage());
        }
    }
}