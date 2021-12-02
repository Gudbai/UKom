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
    public partial class CheckFormPage : ContentPage
    {
        public CheckFormPage()
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
                
            }
        }
    }
}