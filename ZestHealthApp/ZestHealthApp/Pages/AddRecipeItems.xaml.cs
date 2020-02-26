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
        List<string> ingredients = new List<string>();
        
       
        public AddRecipeItems()
        {
            InitializeComponent();
            
            items.Spacing = 10;
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {


           

            
                ingredients.Add(ingredient.Text);
                ingredient = new Entry { VerticalOptions = LayoutOptions.Start };
                ingredient.Placeholder = "Ingredient";

                items.Children.Add(ingredient);
                items.Spacing = 10;
            

            


        }

        private async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {

            ingredients.Add(ingredient.Text);

            await FirebaseHelper.AddRecipe(ingredients, RecipeName.Text);
          
            await Navigation.PopModalAsync();
        }
    }
}