using CRMApp.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRMApp.Views.Managements.MasterFiles.SetupList
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DebtorPage : ContentPage
    {
        public DebtorPage()
        {
            InitializeComponent();
            InsertInput();
        }

        private void InsertInput()
        {
            MessagingCenter.Subscribe<Page, int>(this, "debtorID", (sender, arg) =>
            {
                debtorID = arg;
                LblTitle.Text = "Edit Debtor";
                mode = "Edit";
            });

            MessagingCenter.Subscribe<Page, string>(this, "debtorCode", (sender, arg) =>
            {
                EntryCode.Text = arg;
            });

            MessagingCenter.Subscribe<Page, string>(this, "debtorName", (sender, arg) =>
            {
                EntryName.Text = arg;
            });

            MessagingCenter.Subscribe<Page, string>(this, "debtorEmail", (sender, arg) =>
            {
                EntryEmail.Text = arg;
            });

            MessagingCenter.Subscribe<Page, string>(this, "debtorAddress", (sender, arg) =>
            {
                EntryAddress.Text = arg;
            });

            MessagingCenter.Subscribe<Page, string>(this, "debtorPhone", (sender, arg) =>
            {
                EntryPhoneNumber.Text = arg;
            });
        }

        private int debtorID;
        private string mode = "Setup";
        private void SaveInput()
        {
            if (mode == "Setup")
            {
                QueryConnector.Run("INSERT INTO tblDebtor " +
                    "(debtorCode, debtorName, debtorEmail, debtorAddress, debtorPhone) VALUES " +
                    "(@str1, @str2, @str3, @str4, @str5);",
                    str1: EntryCode.Text, str2: EntryName.Text, str3: EntryEmail.Text, str4: EntryAddress.Text, str5: EntryPhoneNumber.Text);
            }
            else if (mode == "Edit")
            {
                QueryConnector.Run("UPDATE tblDebtor SET " +
                    "debtorCode = @str1, " +
                    "debtorName = @str2, " +
                    "debtorEmail = @str3, " +
                    "debtorAddress = @str4, " +
                    "debtorPhone = @str5 " +
                    "WHERE debtorID = @int1;",
                    int1: debtorID, str1: EntryCode.Text, str2: EntryName.Text, str3: EntryEmail.Text, str4: EntryAddress.Text, str5: EntryPhoneNumber.Text);
            }
        }

        private async void OnButtonClick(object sender, EventArgs e)
        {
            if (sender == BtnBack)
            {
                await BtnBack.ScaleTo(BtnBack.Scale - 0.1, 100);
                BtnBack.BackgroundColor = Color.Gray;

                await BtnBack.ScaleTo(BtnBack.Scale, 100);
                BtnBack.BackgroundColor = Color.Black;

                await Navigation.PopAsync();
            }
            else
            {
                BtnCreate.BackgroundColor = Color.Gray;
                await Task.Delay(100);
                BtnCreate.BackgroundColor = Color.Black;
                await Task.Delay(100);

                if (EntryCode.Text != null && EntryCode.Text.Trim().Length != 0)
                {
                    if (EntryName.Text != null && EntryName.Text.Trim().Length != 0)
                    {
                        if (mode == "Setup")
                        {
                            SaveInput();
                        }
                        else if (mode == "Edit")
                        {
                            MessagingCenter.Send<Page>(this, "SuspendDataGrid");
                            SaveInput();
                            MessagingCenter.Send<Page>(this, "RefreshDataGrid");
                        }

                        await Navigation.PopAsync();
                    }
                    else
                        await DisplayAlert("Error", "Debtor Name cannot be empty", "OK");
                }
                else
                    await DisplayAlert("Error", "Debtor Code cannot be empty", "OK");
            }
        }
    }
}