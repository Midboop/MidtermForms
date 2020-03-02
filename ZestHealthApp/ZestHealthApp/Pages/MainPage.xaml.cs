using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZestHealthApp.Pages;
using ZestHealthApp.ViewModel;

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
            BindingContext = new RecipeView();

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await (BindingContext as RecipeView).RefreshRecipes();

        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("RecipeAddItem");

        }

        private void RecipeCards_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
