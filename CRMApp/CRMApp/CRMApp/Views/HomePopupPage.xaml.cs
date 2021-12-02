using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRMApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePopupPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        private readonly double lblAccSettingsScale, lblLogoutScale;
        public HomePopupPage()
        {
            InitializeComponent();

            lblAccSettingsScale = LblAccSettings.Scale;
            TapGestureRecognizer AccSettingsProcedure = new TapGestureRecognizer();
            AccSettingsProcedure.Tapped += async (s, e) =>
            {
                LblAccSettings.BackgroundColor = Color.LightGray;
                await LblAccSettings.ScaleTo(lblAccSettingsScale - 0.1, 100);

                LblAccSettings.BackgroundColor = Color.Transparent;
                await LblAccSettings.ScaleTo(lblAccSettingsScale, 100);

                await Navigation.PushAsync(new AccountSettingsPage());
                await Navigation.PopPopupAsync();
            };

            lblLogoutScale = LblLogout.Scale;
            TapGestureRecognizer LogoutProcedure = new TapGestureRecognizer();
            LogoutProcedure.Tapped += async (s, e) =>
            {
                LblLogout.BackgroundColor = Color.LightGray;
                await LblLogout.ScaleTo(lblLogoutScale - 0.1, 100);

                LblLogout.BackgroundColor = Color.Transparent;
                await LblLogout.ScaleTo(lblLogoutScale, 100);

                await Navigation.PushAsync(new LoginPage());
                Navigation.RemovePage(Navigation.NavigationStack.First());
                await Navigation.PopPopupAsync();
            };

            LblAccSettings.GestureRecognizers.Add(AccSettingsProcedure);
            LblLogout.GestureRecognizers.Add(LogoutProcedure);
        }

        public void OnAnimationStarted(bool isPopAnimation)
        {
            // optional code here   
        }

        public void OnAnimationFinished(bool isPopAnimation)
        {
            // optional code here   
        }

        protected override bool OnBackButtonPressed()
        {
            // Return true if you don't want to close this popup page when a back button is pressed
            return false;
        }

        // Invoked when background is clicked
        protected override bool OnBackgroundClicked()
        {
            // Return false if you don't want to close this popup page when a background of the popup page is clicked
            return true;
        }

        private async void CloseBtn_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync(true);
        }
    }
}
