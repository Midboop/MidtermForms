using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ZestHealthApp.Models;

namespace ZestHealthApp.ViewModel
{
    public class SingleRecipeData
    {
        public SingleRecipeData(RecipeItems recipe)
        {
            if (recipe != null)
            {
                RecipeTitle = recipe.RecipeName;
                Items = recipe.IngredientsList;
                
            }
        }


        public string RecipeTitle { get; }

        public List<string> Items { get; }

       
    }

}
