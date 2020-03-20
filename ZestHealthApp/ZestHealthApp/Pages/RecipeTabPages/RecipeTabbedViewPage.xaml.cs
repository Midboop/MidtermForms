using Plugin.Media;
using Plugin.Media.Abstractions;
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

        private async void ChangePicture_Clicked(object sender, EventArgs e)
        {
            // Maybe I can make this way to add pictures work, it can use downloads, drive, etc
            //(sender as Button).IsEnabled = false;

            //Stream stream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();

            //if (stream != null)
            //{
            //    Image.Source = ImageSource.FromStream(() => stream);




            //}

            //await FirebaseHelper.RecipeImage(stream);

            //(sender as Button).IsEnabled = true;
            //////////
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
    }
}