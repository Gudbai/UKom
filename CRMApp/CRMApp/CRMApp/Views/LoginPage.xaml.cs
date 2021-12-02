using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CRMApp.Models;
using System.Collections.ObjectModel;
using Rg.Plugins.Popup.Extensions;

namespace CRMApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

            EntryUsername.Text = "";
            EntryPassword.Text = "";

            EntryUsername.Completed += (s, e) => EntryPassword.Focus();
            EntryPassword.Completed += (s, e) => BtnSignIn.Focus();

            // Forgot Password Procedure
            TapGestureRecognizer ForgotPasswordProcedure = new TapGestureRecognizer();
            ForgotPasswordProcedure.Tapped += async (s, e) =>
            {
                await Navigation.PushPopupAsync(new ForgotPasswordPopupPage());
            };
            LblForgotPassword.GestureRecognizers.Add(ForgotPasswordProcedure);

            btnScale = BtnSignIn.Scale;
        }

        private readonly double btnScale;
        private async void SignInProcedure(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            button.BackgroundColor = Color.Gray;
            await button.ScaleTo(btnScale - 0.1, 100);

            button.BackgroundColor = Color.Black;
            await button.ScaleTo(btnScale, 100);

            ObservableCollection<UserInfo> data = UserInfoRepository.LoginCheck(EntryUsername.Text, EntryPassword.Text);
            int loginCheck = data.Count();
            if (loginCheck != 0)
            {
                await Navigation.PushAsync(new HomeNavPage());
                Navigation.RemovePage(this);
                BtnSignIn.IsEnabled = false;
            }
            else
            {
                await DisplayAlert("Login", "Login Failed", "OK");
            }
        }
    }
}