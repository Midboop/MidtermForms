using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZestHealthApp.ViewModel;

namespace ZestHealthApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddRecipeItems : ContentPage
    {
        // local list to store the ingredients
        List<string> ingredients = new List<string>();
        
       
        public AddRecipeItems()
        {
            InitializeComponent();
            // is this working?? Should set the spacing of the lines
            items.Spacing = 10;
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {

            // Everytime the "+" button is clicked, it adds the ingredient to the list above, then creates a new Entry and place holder and then adds that following items
                ingredients.Add(ingredient.Text);
                ingredient = new Entry { VerticalOptions = LayoutOptions.Start };
                ingredient.Placeholder = "Ingredient";
                items.Children.Add(ingredient);
                items.Spacing = 10;
            

            


        }
        // If the user wishes to not save anything and cancels, it just redirects to the previous page
        private async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        // This adds the final ingredient to the list before pushing it to firebase
        private async void AddButton_Clicked(object sender, EventArgs e)
        {

            ingredients.Add(ingredient.Text);
            // this is taking the local list and the RecipeName entry text and stores it in Firebase
            await FirebaseHelper.AddRecipe(ingredients, RecipeName.Text);
          
            await Navigation.PopModalAsync();
        }
    }
}