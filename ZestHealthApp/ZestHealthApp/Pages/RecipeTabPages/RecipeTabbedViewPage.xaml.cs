﻿using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        MediaFile file;
        public SingleRecipeData thisRecipe;
        public RecipeTabbedViewPage(SingleRecipeData objectInstance)
        {
            Shell.SetTabBarIsVisible(this, false);
            InitializeComponent();
            thisRecipe = objectInstance;
            BindingContext = thisRecipe;
            recipeDetails.BindingContext = thisRecipe;
            recipeDetails.thisNutrition = recipeNutrition;
            recipeInstructions.BindingContext = thisRecipe;
            recipeInstructions.preSavedRecipe = thisRecipe;
            recipeInstructions.InitializeInstructions();
            recipeNutrition.BindingContext = thisRecipe;
            recipeNutrition.thisTabbedPage = this;

        }
        protected override bool OnBackButtonPressed()
        {
            //TODO: if unsaved on instructions page show dialog
            if(!recipeInstructions.isSaved)
            {
                NotifyUnsaved();
            }
            GoToRecipe();
            return true;
        }

        public async void NotifyUnsaved()
        {
            bool answer = await App.Current.MainPage.DisplayAlert("Whoops!", "You had unsaved recipe instructions!", "Save", "Discard Changes");
            if (answer)
                recipeInstructions.SavePage();
        }
        public async void GoToRecipe()
        {
            await Shell.Current.GoToAsync("//Recipe");
        }
        public void UpdateViews()
        {
            CalorieLabel.Text = thisRecipe.TotalCalories.ToString() + " Total Calories";
            RatingStars.Value = thisRecipe.RatingStars;
        }

        private async void AddPicture()
        {
            await CrossMedia.Current.Initialize();
            try
            {
                file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                {
                    PhotoSize = PhotoSize.Medium
                });
                if (file == null)
                    return;
                Image.Source = ImageSource.FromStream(() =>
                {
                    var imageStram = file.GetStream();
                    return imageStram;
                });

                await FirebaseHelper.RecipeImage(file.GetStream(), thisRecipe.RecipeTitle);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }

        private async void ChangePicture_Clicked(object sender, EventArgs e)
        {

            int commandParameter = Convert.ToInt32(((Button)sender).CommandParameter);

            Permission permission = (Permission)commandParameter;
            var permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);
            if (permissionStatus != PermissionStatus.Granted)
            {
                var response = await CrossPermissions.Current.RequestPermissionsAsync(permission);
                var userRespone = response[permission];
                if (userRespone == PermissionStatus.Granted)
                {
                    AddPicture();
                }
                Debug.WriteLine($"Permission {permission}, {userRespone}");
            }
            else
            {
                AddPicture();
            }
        }

        private async void TitleEntry_Completed(object sender, EventArgs e)
        {

            var recipeNames = await FirebaseHelper.GetRecipeNames();
            if (recipeNames.Contains(TitleEntry.Text))
            {
                await App.Current.MainPage.DisplayAlert("Alert", "This name already exists, please enter a different name.", "Cancel");
                RecipeTitleLabel.Text = null;
            }
            else
            {
                thisRecipe = (BindingContext as SingleRecipeData);
                await FirebaseHelper.UpdateRecipeTitle(TitleEntry.Text, thisRecipe);
                
                RecipeTitleLabel.Text = TitleEntry.Text;
                TitleEntry.IsEnabled = false;
                TitleEntry.IsVisible = false;
                UpdateTitleButton.IsEnabled = true;
                thisRecipe.RecipeTitle = TitleEntry.Text;

            }


        }

        private void UpdateTitleButton_Clicked(object sender, EventArgs e)
        {
            RecipeTitleLabel.Text = null;
            TitleEntry.IsEnabled = true;
            TitleEntry.IsVisible = true;
            UpdateTitleButton.IsEnabled = false;
        }
    }
}