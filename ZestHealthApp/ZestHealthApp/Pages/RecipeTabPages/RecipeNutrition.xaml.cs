using Sharpnado.Presentation.Forms.CustomViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ZestHealthApp.Pages.RecipeTabPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeNutrition : ContentView, IAnimatableReveal // Tab 3
    {
        public RecipeNutrition()
        {
            InitializeComponent();
        }

        public bool Animate { get; set; }
    }
}