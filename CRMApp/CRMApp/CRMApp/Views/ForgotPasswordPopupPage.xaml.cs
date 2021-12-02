using CRMApp.Models;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRMApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgotPasswordPopupPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        public ForgotPasswordPopupPage()
        {
            InitializeComponent();

            btnScale = BtnSendRequest.Scale;
        }

        internal static readonly char[] chars =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
        public static string GetUniqueKey(int size)
        {
            byte[] data = new byte[4 * size];
            using (var crypto = RandomNumberGenerator.Create())
            {
                crypto.GetBytes(data);
            }
            StringBuilder result = new StringBuilder(size);
            for (int i = 0; i < size; i++)
            {
                var rnd = BitConverter.ToUInt32(data, i * 4);
                var idx = rnd % chars.Length;

                result.Append(chars[idx]);
            }

            return result.ToString();
        }

        private static string GetString(string text)
        {
            if (text == null)
                return string.Empty;
            else
                return text.ToString();
        }

        private double btnScale;
        private async void SendEmailProcedure(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            await button.ScaleTo(btnScale - 0.1, 100);
            button.BackgroundColor = Color.Gray;

            await button.ScaleTo(btnScale, 100);
            button.BackgroundColor = Color.Black;

            ObservableCollection<UserInfo> data = UserInfoRepository.ResetPassword(email: EntryEmail.Text);
            int loginCheck = data.Count();
            if (loginCheck != 0)
            {
                string username = data[0].UserName;
                string password = GetUniqueKey(8);

                QueryConnector.Run("UPDATE tblUser SET userPassword = @str1 WHERE userEmail = @str2", str1: password, str2: EntryEmail.Text);

                try
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                    mail.From = new MailAddress("aldienmaulana@gmail.com");
                    mail.To.Add(EntryEmail.Text);
                    mail.Subject = "CRMApp Password Reset Request";
                    mail.Body = "Dear " + username + ", \n\n" + "Your new password is (" + password + "). \n\n" +
                    "If you wish to change your password, please do it at the Account Settings tab which can be accessed by pressing the gear button in the namecard at Home page. \n\n" +
                    "Regards, \n" +
                    "Alilwork Customer Service";

                    SmtpServer.Port = 587;
                    SmtpServer.Host = "smtp.gmail.com";
                    SmtpServer.EnableSsl = true;
                    SmtpServer.UseDefaultCredentials = false;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("aldienmaulana@gmail.com", "kdrkpovujvwlyubp");

                    SmtpServer.Send(mail);
                    await DisplayAlert("Request Sent", "Password reset request sent, please check your email.", "OK");
                    await Navigation.PopPopupAsync();
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Failed", ex.Message, "OK");
                }
            }
            else if(GetString(EntryEmail.Text).Length < 8)
            {
                await DisplayAlert("Error", "Please enter your email correctly", "OK");
            }
            else
            {
                await DisplayAlert("Request Sent", "Password reset request sent, please check your email.", "OK");
                await Navigation.PopPopupAsync();
            }
        }
    }
}