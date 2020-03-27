using Sharpnado.Presentation.Forms.CustomViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZestHealthApp.Models;
using ZestHealthApp.ViewModel;

namespace ZestHealthApp.Pages.RecipeTabPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeNutrition : ContentView, IAnimatableReveal // Tab 3
    {
        NutritionFacts thisRecipe;
        public RecipeNutrition()
        {
            InitializeComponent();
        }

        public bool Animate { get; set; }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(calorieCalculator.TextColor == Color.White)
            {
                calorieEntry.PlaceholderColor = Color.Accent;
                calorieCalculator.TextColor = Color.Accent;
            }
            if (calorieEntry.Text != string.Empty)
            {
                thisRecipe = (BindingContext as NutritionFacts);
                double caloriesPerGram = (thisRecipe.TotalCalories / thisRecipe.TotalWeight);
                calorieCalculator.Text = (caloriesPerGram * Convert.ToDouble(calorieEntry.Text)).ToString() + " cal.";

            }
            else if (calorieCalculator.TextColor != Color.White) 
                     calorieCalculator.Text = "0 cal.";
               
        }

        private void calorieEntry_Completed(object sender, EventArgs e)
        {
            calorieEntry.PlaceholderColor = Color.White;
            calorieCalculator.TextColor = Color.White;
            calorieEntry.Placeholder ="A "+ calorieEntry.Text + " Gram Serving is";
            calorieEntry.Text = string.Empty;
           
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}