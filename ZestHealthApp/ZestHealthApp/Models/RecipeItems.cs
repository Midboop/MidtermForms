using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ZestHealthApp.Models
{
   public class RecipeItems : BaseFodyObservable
    {
        public string RecipeName { get; set; }
        public List<IngredientItem> IngredientsList = new List<IngredientItem>();
        public List<InstructionItem> Instructions = new List<InstructionItem>();
        public NutritionFacts NutritionValues = new NutritionFacts();
        public double RecipeRating { get; set; }
        public ImageSource RecipeImage { get; set; }
    }
}
