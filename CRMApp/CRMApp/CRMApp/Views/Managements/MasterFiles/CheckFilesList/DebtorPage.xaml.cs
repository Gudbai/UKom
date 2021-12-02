using CRMApp.Models.MasterFiles;
using Syncfusion.SfDataGrid.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRMApp.Views.Managements.MasterFiles.CheckFilesList
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DebtorPage : ContentPage
    {
        public DebtorPage()
        {
            InitializeComponent();
            viewModel.filterTextChanged = OnFilterChanged;

            btnScale = BtnSelected.Scale;

            MessagingCenter.Subscribe<Page>(this, "SuspendDataGrid", (sender) =>
            {
                dataGrid.View.BeginInit();
            });

            MessagingCenter.Subscribe<Page>(this, "RefreshDataGrid", (sender) =>
            {
                dataGrid.View.EndInit();
            });
        }

        private readonly double btnScale;
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
                Button button = (Button)sender;

                await button.ScaleTo(btnScale - 0.1, 100);
                button.BackgroundColor = Color.Gray;

                await button.ScaleTo(btnScale, 100);
                button.BackgroundColor = Color.Black;

                if (dataGrid.SelectedIndex != -1)
                {
                    await Navigation.PushAsync(new SetupList.DebtorPage());
                    MessagingCenter.Send<Page, int>(this, "debtorID", debtorID);
                    MessagingCenter.Send<Page, string>(this, "debtorCode", debtorCode);
                    MessagingCenter.Send<Page, string>(this, "debtorName", debtorName);
                    MessagingCenter.Send<Page, string>(this, "debtorEmail", debtorEmail);
                    MessagingCenter.Send<Page, string>(this, "debtorAddress", debtorAddress);
                    MessagingCenter.Send<Page, string>(this, "debtorPhone", debtorPhone);
                }
            }
        }
        
        private int debtorID;
        private string debtorCode, debtorName, debtorEmail, debtorAddress, debtorPhone;
        private void OnGridTapped(object sender, GridTappedEventArgs e)
        {
            foreach (var column in dataGrid.Columns)
            {
                if (column.MappingName == "DebtorID")
                {
                    var rowData = dataGrid.GetRecordAtRowIndex(e.RowColumnIndex.RowIndex);
                    debtorID = (int)dataGrid.GetCellValue(rowData, column.MappingName);
                }
                if (column.MappingName == "DebtorCode")
                {
                    var rowData = dataGrid.GetRecordAtRowIndex(e.RowColumnIndex.RowIndex);
                    debtorCode = dataGrid.GetCellValue(rowData, column.MappingName).ToString();
                }
                if (column.MappingName == "DebtorName")
                {
                    var rowData = dataGrid.GetRecordAtRowIndex(e.RowColumnIndex.RowIndex);
                    debtorName = dataGrid.GetCellValue(rowData, column.MappingName).ToString();
                }
                if (column.MappingName == "DebtorEmail")
                {
                    var rowData = dataGrid.GetRecordAtRowIndex(e.RowColumnIndex.RowIndex);
                    debtorEmail = dataGrid.GetCellValue(rowData, column.MappingName).ToString();
                }
                if (column.MappingName == "DebtorAddress")
                {
                    var rowData = dataGrid.GetRecordAtRowIndex(e.RowColumnIndex.RowIndex);
                    debtorAddress = dataGrid.GetCellValue(rowData, column.MappingName).ToString();
                }
                if (column.MappingName == "DebtorPhone")
                {
                    var rowData = dataGrid.GetRecordAtRowIndex(e.RowColumnIndex.RowIndex);
                    debtorPhone = dataGrid.GetCellValue(rowData, column.MappingName).ToString();
                }
            }
        }

        private void OnFilterChanged()
        {
            if (dataGrid.View != null)
            {
                this.dataGrid.View.Filter = viewModel.FilterRecords;
                this.dataGrid.View.RefreshFilter();
            }
        }

        private void OnFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue == null)
                viewModel.FilterText = "";
            else
                viewModel.FilterText = e.NewTextValue;
        }

        private void OnColumnsSelectionChanged(object sender, EventArgs e)
        {
            Picker newPicker = (Picker)sender;
            viewModel.SelectedColumn = newPicker.Items[newPicker.SelectedIndex];
        }
    }
}