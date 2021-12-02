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
    public partial class ServicePage : ContentPage
    {
        public ServicePage()
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
                    await Navigation.PushAsync(new SetupList.ServicePage());
                    MessagingCenter.Send<Page, int>(this, "serviceID", serviceID);
                    MessagingCenter.Send<Page, string>(this, "serviceCode", serviceCode);
                    MessagingCenter.Send<Page, string>(this, "serviceDescription", serviceDescription);
                    MessagingCenter.Send<Page, string>(this, "serviceCost", serviceCost);
                }
            }
        }

        private int serviceID;
        private string serviceCode, serviceDescription, serviceCost;
        private void OnGridTapped(object sender, GridTappedEventArgs e)
        {
            foreach (var column in dataGrid.Columns)
            {
                if (column.MappingName == "ServiceID")
                {
                    var rowData = dataGrid.GetRecordAtRowIndex(e.RowColumnIndex.RowIndex);
                    serviceID = (int)dataGrid.GetCellValue(rowData, column.MappingName);
                }
                if (column.MappingName == "ServiceCode")
                {
                    var rowData = dataGrid.GetRecordAtRowIndex(e.RowColumnIndex.RowIndex);
                    serviceCode = dataGrid.GetCellValue(rowData, column.MappingName).ToString();
                }
                if (column.MappingName == "ServiceDescription")
                {
                    var rowData = dataGrid.GetRecordAtRowIndex(e.RowColumnIndex.RowIndex);
                    serviceDescription = dataGrid.GetCellValue(rowData, column.MappingName).ToString();
                }
                if (column.MappingName == "ServiceCost")
                {
                    var rowData = dataGrid.GetRecordAtRowIndex(e.RowColumnIndex.RowIndex);
                    serviceCost = dataGrid.GetCellValue(rowData, column.MappingName).ToString();
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