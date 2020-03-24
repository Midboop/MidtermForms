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
            // the following is an example entry until we get everything flushed out 
            //List<IngredientItem> ingredients = new List<IngredientItem>();
            //IngredientItem cornFlakes = new IngredientItem("Corn Flakes", 1, 180, 150, "Cup");
            //IngredientItem powderedSugar = new IngredientItem("Powdered Sugar", 0.5, 220, 150, "Cup");
            //IngredientItem milk = new IngredientItem("Milk", 1, 1200, 200, "Cup");
            //ingredients.Add(cornFlakes);
            //ingredients.Add(powderedSugar);
            //ingredients.Add(milk);

            RecipeItems newRecipe = new RecipeItems();
            // newRecipe.RecipeName = "Frosted Flakes";
            // newRecipe.RecipeName = "New Recipe";

            // End example entry changes
           // newRecipe.IngredientsList = ingredients;
            //Taylor: add other dictionary items
           // await FirebaseHelper.AddRecipe(newRecipe);
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

        private void OKButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}
