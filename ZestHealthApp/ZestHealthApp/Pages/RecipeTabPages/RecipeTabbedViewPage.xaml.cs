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
    public partial class RecipeTabbedViewPage : RecipeContentPage, IBindablePage
    {
        public SingleRecipeData thisRecipe;
        public RecipeTabbedViewPage(SingleRecipeData objectInstance)
        {
            Shell.SetTabBarIsVisible(this, false);
            InitializeComponent();
            thisRecipe = objectInstance;
            BindingContext = thisRecipe;
            recipeDetails.BindingContext = thisRecipe;

        }
        protected override bool OnBackButtonPressed()
        {
            GoToRecipe();
            return true;
        }

        private async void GoToRecipe()
        {
            await Shell.Current.GoToAsync("//Recipe");
        }

      
    }
}