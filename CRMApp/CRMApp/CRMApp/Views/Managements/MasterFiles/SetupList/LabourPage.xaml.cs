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
    public partial class LabourPage : ContentPage
    {
        public LabourPage()
        {
            InitializeComponent();
            InsertInput();
        }

        private void InsertInput()
        {
            MessagingCenter.Subscribe<Page, int>(this, "labourID", (sender, arg) =>
            {
                labourID = arg;
                LblTitle.Text = "Edit Labour";
                mode = "Edit";
            });

            MessagingCenter.Subscribe<Page, string>(this, "labourCode", (sender, arg) =>
            {
                EntryCode.Text = arg;
            });

            MessagingCenter.Subscribe<Page, string>(this, "labourDescription", (sender, arg) =>
            {
                EntryDescription.Text = arg;
            });
        }

        private int labourID;
        private string mode = "Setup";
        private void SaveInput()
        {
            if (mode == "Setup")
            {
                QueryConnector.Run("INSERT INTO tblLabour " +
                    "(labourCode, labourDescription) VALUES " +
                    "(@str1, @str2);",
                    str1: EntryCode.Text, str2: EntryDescription.Text);
            }
            else if (mode == "Edit")
            {
                QueryConnector.Run("UPDATE tblLabour SET " +
                    "labourCode = @str1, " +
                    "labourDescription = @str2 " +
                    "WHERE labourID = @int1;",
                    int1: labourID, str1: EntryCode.Text, str2: EntryDescription.Text);
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
                    if (EntryDescription.Text != null && EntryDescription.Text.Trim().Length != 0)
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
                        await DisplayAlert("Error", "Labour Description cannot be empty", "OK");
                }
                else
                    await DisplayAlert("Error", "Labour Code cannot be empty", "OK");
            }
        }
    }
}