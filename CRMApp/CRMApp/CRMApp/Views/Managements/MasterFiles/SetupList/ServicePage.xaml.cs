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
    public partial class ServicePage : ContentPage
    {
        public ServicePage()
        {
            InitializeComponent();
            InsertInput();
        }

        private void InsertInput()
        {
            MessagingCenter.Subscribe<Page, int>(this, "serviceID", (sender, arg) =>
            {
                serviceID = arg;
                LblTitle.Text = "Edit Service";
                mode = "Edit";
            });

            MessagingCenter.Subscribe<Page, string>(this, "serviceCode", (sender, arg) =>
            {
                EntryCode.Text = arg;
            });

            MessagingCenter.Subscribe<Page, string>(this, "serviceDescription", (sender, arg) =>
            {
                EntryDescription.Text = arg;
            });

            MessagingCenter.Subscribe<Page, string>(this, "serviceCost", (sender, arg) =>
            {
                EntryCost.Text = arg;
            });
        }

        private int serviceID;
        private string mode = "Setup";
        private void SaveInput()
        {
            if (mode == "Setup")
            {
                QueryConnector.Run("INSERT INTO tblService " +
                    "(serviceCode, serviceDescription, serviceCost) VALUES " +
                    "(@str1, @str2, str3);",
                    str1: EntryCode.Text, str2: EntryDescription.Text, str3: EntryCost.Text);
            }
            else if (mode == "Edit")
            {
                QueryConnector.Run("UPDATE tblService SET " +
                    "serviceCode = @str1, " +
                    "serviceDescription = @str2 " +
                    "serviceCost = @str3 " +
                    "WHERE serviceID = @int1;",
                    int1: serviceID, str1: EntryCode.Text, str2: EntryDescription.Text, str3: EntryCost.Text);
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
                        await DisplayAlert("Error", "Service Description cannot be empty", "OK");
                }
                else
                    await DisplayAlert("Error", "Service Code cannot be empty", "OK");
            }
        }
    }
}