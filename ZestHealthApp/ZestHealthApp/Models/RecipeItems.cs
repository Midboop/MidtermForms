using System;
using System.Collections.Generic;
using System.Text;

namespace ZestHealthApp.Models
{
   public class RecipeItems : BaseFodyObservable
    {
        public List<string> IngredientsList = new List<string>();
        public string RecipeName { get; set; }
    }
}
