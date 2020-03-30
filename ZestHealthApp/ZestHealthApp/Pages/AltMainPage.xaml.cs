using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZestHealthApp.Models;
using ZestHealthApp.Pages.RecipeTabPages;
using ZestHealthApp.ViewModel;

namespace ZestHealthApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AltMainPage : ContentPage
    {
        int CurrentFrame = 0;
        public AltMainPage()
        {
            InitializeComponent();
            BindingContext = new FBRecipeView();
           
        }

        protected override async void OnAppearing()
        {
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

        private async void RecipeListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            RecipeItems selectedItem = (e.SelectedItem as RecipeItems);
            SingleRecipeData selected = new SingleRecipeData(selectedItem);
            selected.RecipeImage = await FirebaseHelper.GetImage(selectedItem.RecipeName);
            await Navigation.PushModalAsync(new RecipeTabbedViewPage(selected));
            selectedItem = null;
        }
        private async void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            if(mySwitch.IsToggled != true)
            {
                Application.Current.Properties["isToggled"] = false;
                await Shell.Current.Navigation.PopAsync();

            }
          
        }
    }
}