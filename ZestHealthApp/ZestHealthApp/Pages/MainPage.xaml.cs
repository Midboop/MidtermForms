using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZestHealthApp.Models;
using ZestHealthApp.Pages;
using ZestHealthApp.Pages.RecipeTabPages;
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
            BindingContext = new FBRecipeView();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await (BindingContext as FBRecipeView).RefreshRecipes();

        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            List<string> ingredients = new List<string>();
            RecipeItems newRecipe = new RecipeItems();
            newRecipe.RecipeName = "New Recipe";
            newRecipe.IngredientsList = ingredients;
            await FirebaseHelper.AddRecipe(newRecipe);
            SingleRecipeData selected = new SingleRecipeData(newRecipe);
            await Navigation.PushModalAsync(new RecipeTabbedViewPage(selected));

        }

        private async void RecipeCards_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RecipeItems selectedItem = (e.CurrentSelection.FirstOrDefault() as RecipeItems);
            SingleRecipeData selected = new SingleRecipeData(selectedItem);
            await Navigation.PushModalAsync(new RecipeTabbedViewPage(selected));
            selectedItem = null;
        }
    }
}
