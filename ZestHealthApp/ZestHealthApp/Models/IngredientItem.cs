using System;
using System.Collections.Generic;
using System.Text;

namespace ZestHealthApp.Models
{
   public class IngredientItem : BaseFodyObservable  
    {
        public IngredientItem()
        {
            Name = "NAME NOT SET";
            Quantity = 99;
            Calories = 99;
            Weight = 99;
            Unit = "UNIT NOT SET";
        }
        public IngredientItem (IngredientItem copy)
        {
            Name = copy.Name;
            Quantity = copy.Quantity;
            Calories = copy.Calories;
            Weight = copy.Weight;
            Unit = copy.Unit;
        }
        public IngredientItem( string name, double quantity, int calories, int weight, string unit )
        {
            Name = name;
            Quantity = quantity;
            Calories = calories;
            Weight = weight;
            Unit = unit;
        }

        public string Name { get; set; }

        public double Quantity { get; set; }
        public int Calories { get; set; }
        public int Weight { get; set; }
        public string Unit { get; set; }

    }
}
