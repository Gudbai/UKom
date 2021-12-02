using CRMApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRMApp.Views.Managements.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateFormPage : ContentPage
    {
        public CreateFormPage()
        {
            InitializeComponent();
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

                SaveInput();
                await Navigation.PopAsync();
            }
        }

        private void OnCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (sender == RdBtnTenant)
                billingType = "Tenant/Customer";
            else if (sender == RdBtnBuilding)
                billingType = "Building";
        }

        private string billingType = "Yes";
        private void SaveInput()
        {
            QueryConnector.Run("INSERT INTO tblForm " +
                "(debtorCode, formName, sourceCode, billingType, workRequest, areaCode, priorityLevel, assignTo, requesterName, requesterPhone) VALUES " +
                "(@str1, @str2, @str3, @str4, @str5, @str6, @str7, @str8, @str9, @str10);",
                str1: EntryDebtorCode.Text, str2: EntryFormName.Text, str3: EntrySource.Text, str4: billingType, str5: EntryWorkRequest.Text, str6: EntryArea.Text, str7: PickerPriorityLevel.Items[PickerPriorityLevel.SelectedIndex], str8: EntryAssignTo.Text, str9: EntryRequesterName.Text, str10: EntryRequesterContactNumber.Text);
        }

        private void EntryWithLabel_TextChanged(object sender, TextChangedEventArgs e)
        {
            Entry entry = (Entry)sender;

            if (entry.Text.Length <= 2)
            {
                switch (entry.ClassId)
                {
                    case "EntryDebtorCode":
                        LblDebtorName.FontSize = 0.000001;
                        break;
                    case "EntrySource":
                        LblSourceName.FontSize = 0.000001;
                        break;
                    case "EntryArea":
                        LblAreaName.FontSize = 0.000001;
                        break;
                    case "EntryAssignTo":
                        LblAssignTo.FontSize = 0.000001;
                        break;
                }
            }
            else if (entry.Text.Length > 2)
            {
                switch (entry.ClassId)
                {
                    case "EntryDebtorCode":
                        LblDebtorName.FontSize = 15;
                        break;
                    case "EntrySource":
                        LblSourceName.FontSize = 15;
                        break;
                    case "EntryArea":
                        LblAreaName.FontSize = 15;
                        break;
                    case "EntryAssignTo":
                        LblAssignTo.FontSize = 15;
                        break;
                }
            }
        }
    }
}