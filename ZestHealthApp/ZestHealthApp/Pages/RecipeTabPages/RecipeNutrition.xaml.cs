using Sharpnado.Presentation.Forms.CustomViews;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZestHealthApp.Models;
using ZestHealthApp.ViewModel;

namespace ZestHealthApp.Pages.RecipeTabPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeNutrition : ContentView, IAnimatableReveal // Tab 3
    {
        SingleRecipeData thisRecipe;
        public RecipeTabbedViewPage thisTabbedPage { get; set; }
        public RecipeNutrition()
        {
            InitializeComponent();
        }

        public bool Animate { get; set; }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(calorieCalculator.TextColor == Color.White)
            {
                calorieEntry.PlaceholderColor = Color.Accent;
                calorieCalculator.TextColor = Color.Accent;
            }
            if (calorieEntry.Text != string.Empty)
            {
                thisRecipe = (BindingContext as SingleRecipeData);
                double caloriesPerGram = (thisRecipe.NutritionValues.TotalCalories / thisRecipe.NutritionValues.TotalWeight);
                calorieCalculator.Text = (caloriesPerGram * Convert.ToDouble(calorieEntry.Text)).ToString() + " cal.";

            }
            if (calorieCalculator.TextColor != Color.White) 
                     calorieCalculator.Text = "0 cal.";
               
        }

        private void calorieEntry_Completed(object sender, EventArgs e)
        {
            calorieEntry.PlaceholderColor = Color.White;
            calorieCalculator.TextColor = Color.White;
            calorieEntry.Placeholder ="A "+ calorieEntry.Text + " Gram Serving is";
            calorieEntry.Text = string.Empty;
           
        }

        private void ServingSizeButton_Clicked(object sender, EventArgs e)
        {
            if (ServingFrame.IsVisible == true && ServingEntry.Text != string.Empty && Convert.ToInt32(ServingEntry.Text) != 0)
            {
                thisRecipe = (BindingContext as SingleRecipeData);
                thisRecipe.NutritionValues.Servings = Convert.ToInt32(ServingEntry.Text);
                FirebaseHelper.UpdateNutrition(thisRecipe);
                ServingEntry.Placeholder = ServingEntry.Text;
                ServingEntry.Text = string.Empty;
                UpdateValues();
            }
            ServingFrame.IsVisible = !ServingFrame.IsVisible;
           
        }

        public void UpdateValues()
        {
            thisRecipe = (BindingContext as SingleRecipeData);
            thisRecipe.UpdateNutrition();
            // Update Views
            thisTabbedPage.UpdateViews();
            ServingView.Text = thisRecipe.NutritionValues.Servings.ToString() + " Servings per Recipe";
            CaloriesView.Text = "Calories per Serving : " + thisRecipe.NutritionValues.CaloriesPerServing.ToString();
            WeightView.Text = "Approx. Weight per Serving : " + thisRecipe.NutritionValues.WeightPerServing.ToString() + " grams";
            TotalWeightView.Text = "Approx. Total Weight of Recipe : " + thisRecipe.NutritionValues.TotalWeight.ToString() + " grams";

        }

        private void RatingStarButton_Clicked(object sender, EventArgs e)
        {
            if(RatingFrame.IsVisible == true && RatingEntry.Text != string.Empty)
            {
                thisRecipe = (BindingContext as SingleRecipeData);
                thisRecipe.RatingStars = Convert.ToDouble(RatingEntry.Text);
                FirebaseHelper.UpdateRating(thisRecipe);
                RatingEntry.Placeholder = RatingEntry.Text;
                RatingEntry.Text = string.Empty;
                thisTabbedPage.UpdateViews();
            }
            RatingFrame.IsVisible = !RatingFrame.IsVisible;
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            // TODO: Add ARE YOU SURE?????
            thisRecipe = (BindingContext as SingleRecipeData);
            if (await FirebaseHelper.DeleteRecipe(thisRecipe))
                thisTabbedPage.GoToRecipe();
            else
                Debug.WriteLine("Delete Failed");
        }
    }
}