using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZestHealthApp.Models;

namespace ZestHealthApp.ViewModel
{
    public class FBRecipeView : BaseFodyObservable
    {
        public ObservableCollection<RecipeItems> RecipeList { get; set; }
        public string RecipeName { get; set; }
        public List<string> IngredientsList { get; set; }
        public Command<RecipeItems> Delete { get; set; }

        public FBRecipeView()
        {
            GetRecipeItems().ContinueWith(t => { RecipeList = new ObservableCollection<RecipeItems>(t.Result); });
            Delete = new Command<RecipeItems>(HandleDelete);
        }

        private async Task<List<RecipeItems>> GetRecipeItems()
        {
            return (await FirebaseHelper.GetRecipes());
        }

        public async Task RefreshRecipes()
        {
            await GetRecipeItems().ContinueWith(t => { RecipeList = new ObservableCollection<RecipeItems>(t.Result); });

        }
        public void UpdateNutrition()
        {
           
            for(int i = 0; i < RecipeList.Count; i++)
            {
                int totalCalories = 0;
                int totalWeight = 0;

                for(int j = 0; i < RecipeList[i].IngredientsList.Count; i++)
                {
                    totalCalories += RecipeList[i].IngredientsList[j].Calories;
                    totalWeight += RecipeList[i].IngredientsList[j].Weight;
                }
                RecipeList[i].NutritionValues.TotalCalories = totalCalories;
                RecipeList[i].NutritionValues.TotalWeight = totalWeight;

                RecipeList[i].NutritionValues.CaloriesPerServing = totalCalories / RecipeList[i].NutritionValues.Servings;
                RecipeList[i].NutritionValues.WeightPerServing = totalWeight / RecipeList[i].NutritionValues.Servings;
            }
            
        }

        private async void HandleDelete(RecipeItems recipe)
        {
            await FirebaseHelper.DeleteRecipe(recipe.RecipeName);
            await RefreshRecipes();
        }
    }
}
