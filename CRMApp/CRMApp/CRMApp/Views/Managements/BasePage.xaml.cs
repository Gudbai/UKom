using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRMApp.Views.Managements
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BasePage : ContentPage
    {
        public BasePage()
        {
            InitializeComponent();
            btnScale = BtnForms.Scale;
        }

        private readonly double btnScale;
        private async void OnButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            await button.ScaleTo(btnScale - 0.1, 100);
            button.BackgroundColor = Color.Gray;

            await button.ScaleTo(btnScale, 100);
            button.BackgroundColor = Color.Black;

            if (button == BtnForms)
            {
                await Navigation.PushAsync(new FormsPage());
            }
            else if (button == BtnMasters)
            {
                await Navigation.PushAsync(new FilesPage());
            }
        }
    }
}