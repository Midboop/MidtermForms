using System;
using System.Collections.Generic;
using System.Text;

namespace ZestHealthApp.Models
{
   public class RecipeItems : BaseFodyObservable
    {
        public string RecipeTitle { get; set; }
        public List<IngredientItem> IngredientsList = new List<IngredientItem>();
        public string RecipeName { get; set; }
        
    }
}
