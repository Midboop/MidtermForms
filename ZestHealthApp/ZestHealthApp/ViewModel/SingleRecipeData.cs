using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using ZestHealthApp.Models;

namespace ZestHealthApp.ViewModel
{
    public class SingleRecipeData : NavigationVM
    {
        public SingleRecipeData(RecipeItems recipe)
        {
            if (recipe != null)
            {
                RecipeTitle = recipe.RecipeName;
                Items = new ObservableCollection<string>(recipe.IngredientsList);
                
            }
        }


        public string RecipeTitle { get; }

        public ObservableCollection<string> Items { get; set; }

        private int _selectedViewModelIndex = 0;

        public int SelectedViewModelIndex
        {
            get => _selectedViewModelIndex;
            set => SetAndRaise(ref _selectedViewModelIndex, value);
        }
    }

}
