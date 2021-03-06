﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZestHealthApp.Models;
using ZestHealthApp.Pages;
using ZestHealthApp.Pages.RecipeTabPages;
using ZestHealthApp.ViewModel;

namespace ZestHealthApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        int CurrentFrame = 0;
        bool TileCards;
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new FBRecipeView();
           // if (Application.Current.Properties["IsToggled"] == null)

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await (BindingContext as FBRecipeView).RefreshRecipes();
            AnimButton.PlayFrameSegment(0, 25);
            CurrentFrame = 25;
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            
            RecipeItems newRecipe = new RecipeItems();
            newRecipe.RecipeImage = await FirebaseHelper.GetDefaultImage();
            newRecipe.RecipeName = "New Recipe";
            newRecipe.RecipeRating = 3;
            newRecipe.Instructions.Add(new InstructionItem(1, null));
            // End example entry changes
            await FirebaseHelper.AddRecipe(newRecipe);
            SingleRecipeData selected = new SingleRecipeData(newRecipe);
            await Navigation.PushModalAsync(new RecipeTabbedViewPage(selected));

        }

        private async void RecipeCards_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RecipeItems selectedItem = (e.CurrentSelection.FirstOrDefault() as RecipeItems);
            SingleRecipeData selected = new SingleRecipeData(selectedItem);
            selected.RecipeImage = await FirebaseHelper.GetImage(selectedItem.RecipeName);
            await Navigation.PushModalAsync(new RecipeTabbedViewPage(selected));
            selectedItem = null;
        }
    }
}
