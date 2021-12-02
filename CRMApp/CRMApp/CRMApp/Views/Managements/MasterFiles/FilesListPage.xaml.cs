using CRMApp.Views.Managements.MasterFiles.SetupList;
using CRMApp.Views.Managements.MasterFiles.CheckFilesList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRMApp.Views.Managements.MasterFiles
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilesListPage : ContentPage
    {
        public FilesListPage()
        {
            InitializeComponent();

            btnScale = BtnDebtor.Scale;

            MessagingCenter.Subscribe<Page>(this, "ChangeMode", (sender) =>
            {
                LblName.Text = "Check Master Files";
                btnMode = "Check";
            });
        }

        private readonly double btnScale;
        private string btnMode;
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

                if (btnMode == "Check")
                {
                    switch (button.ClassId)
                    {
                        case "BtnDebtor":
                            await Navigation.PushAsync(new CheckFilesList.DebtorPage());
                            break;
                        case "BtnArea":
                            await Navigation.PushAsync(new CheckFilesList.AreaPage());
                            break;
                        case "BtnSource":
                            await Navigation.PushAsync(new CheckFilesList.SourcePage());
                            break;
                        case "BtnLabour":
                            await Navigation.PushAsync(new CheckFilesList.LabourPage());
                            break;
                        case "BtnService":
                            await Navigation.PushAsync(new CheckFilesList.ServicePage());
                            break;
                        case "BtnStatus":
                            await Navigation.PushAsync(new CheckFilesList.StatusPage());
                            break;
                    }
                }
                else
                {
                    switch (button.ClassId)
                    {
                        case "BtnDebtor":
                            await Navigation.PushAsync(new SetupList.DebtorPage());
                            break;
                        case "BtnArea":
                            await Navigation.PushAsync(new SetupList.AreaPage());
                            break;
                        case "BtnSource":
                            await Navigation.PushAsync(new SetupList.SourcePage());
                            break;
                        case "BtnLabour":
                            await Navigation.PushAsync(new SetupList.LabourPage());
                            break;
                        case "BtnService":
                            await Navigation.PushAsync(new SetupList.ServicePage());
                            break;
                        case "BtnStatus":
                            await Navigation.PushAsync(new SetupList.StatusPage());
                            break;
                    }
                }
            }
        }
    }
}