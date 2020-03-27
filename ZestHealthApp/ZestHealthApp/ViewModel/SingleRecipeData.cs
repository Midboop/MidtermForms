using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using ZestHealthApp.Models;

namespace ZestHealthApp.ViewModel
{
    public class SingleRecipeData : NavigationVM
    {
        public SingleRecipeData(RecipeItems recipe)
        {
            if (recipe != null)
            {
                RecipeTitle = recipe.RecipeName;
                Items = new ObservableCollection<IngredientItem>(recipe.IngredientsList);
                NutritionValues = new NutritionFacts(recipe.NutritionValues);
                TotalCalories = NutritionValues.TotalCalories;
                RatingStars = recipe.RecipeRating;
                RecipeImage = recipe.RecipeImage;
            }
            
        }
        public NutritionFacts NutritionValues { get; set; }
        public int TotalCalories { get; private set; }

        public string RecipeTitle { get; }
        public double RatingStars { get; set; }

        public ObservableCollection<IngredientItem> Items { get; set; }

        public ImageSource RecipeImage { get; set; }

        private int _selectedViewModelIndex = 0;

        public int SelectedViewModelIndex
        {
            get => _selectedViewModelIndex;
            set => SetAndRaise(ref _selectedViewModelIndex, value);
        }

        public void UpdateNutrition()
        { 
            int totalCalories = 0;
            int totalWeight = 0;
            for (int i = 0; i < Items.Count; i++)
            {
                totalCalories += Items[i].Calories;
                totalWeight += Items[i].Weight;
               
            }
           TotalCalories = totalCalories;
           NutritionValues.TotalCalories = totalCalories;
           NutritionValues.TotalWeight = totalWeight;

           NutritionValues.CaloriesPerServing = totalCalories / NutritionValues.Servings;
           NutritionValues.WeightPerServing = totalWeight / NutritionValues.Servings;

        }
    }

}
