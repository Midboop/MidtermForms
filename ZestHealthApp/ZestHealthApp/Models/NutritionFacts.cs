using System;
using System.Collections.Generic;
using System.Text;

namespace ZestHealthApp.Models
{
   public class NutritionFacts
    {
        public NutritionFacts()
        {
            Servings = 1;
            TotalCalories = 0;
            CaloriesPerServing = 0;
            TotalWeight = 0;
            WeightPerServing = 0;
        }
        public NutritionFacts(NutritionFacts nutritionValues)
        {
            Servings = nutritionValues.Servings;
            TotalCalories = nutritionValues.TotalCalories;
            CaloriesPerServing = nutritionValues.CaloriesPerServing;
            TotalWeight = nutritionValues.TotalWeight;
            WeightPerServing = nutritionValues.WeightPerServing;
        }
        public int Servings { get; set; }
        public int TotalCalories { get; set; }
        public double CaloriesPerServing { get; set; }
        public int TotalWeight { get; set; }
        public double WeightPerServing { get; set; }

    }
}
