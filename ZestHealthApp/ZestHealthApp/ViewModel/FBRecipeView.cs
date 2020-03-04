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

        private async void HandleDelete(RecipeItems recipe)
        {
            await FirebaseHelper.DeleteRecipe(recipe.RecipeName);
            await RefreshRecipes();
        }
    }
}
