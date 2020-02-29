using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZestHealthApp.Pages;

namespace ZestHealthApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();

        }

        protected override void OnAppearing()
        {
            buildingAnim.Play();
        }

            private async void ImageButton_Clicked(object sender, EventArgs e)
        {

            await Shell.Current.GoToAsync("RecipeAddItem");

        }
    }
}
