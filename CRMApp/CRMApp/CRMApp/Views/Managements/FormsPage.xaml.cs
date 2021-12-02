using CRMApp.Views.Managements.Forms;
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
    public partial class FormsPage : ContentPage
    {
        public FormsPage()
        {
            InitializeComponent();

            btnScale = BtnCreate.Scale;
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

                if (sender == BtnCreate)
                    await Navigation.PushAsync(new CreateFormPage());
                else if (sender == BtnCheck)
                    await Navigation.PushAsync(new CheckFormPage());
            }
        }
    }
}