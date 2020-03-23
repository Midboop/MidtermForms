using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.IO;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ZestHealthApp.Models
{
   public class RecipeItems : BaseFodyObservable
    {
        public List<IngredientItem> IngredientsList = new List<IngredientItem>();
        public string RecipeName { get; set; }

        public ImageSource RecipeImage { get; set; }



    }
}

