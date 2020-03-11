using Sharpnado.Presentation.Forms.CustomViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xam.Forms.Markdown;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZestHealthApp.ViewModel;

namespace ZestHealthApp.Pages.RecipeTabPages 
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeDetails : ContentView, IAnimatableReveal // Tab 1
    {
        SingleRecipeData thisRecipe;
        public RecipeDetails()
        {
            InitializeComponent();
            // SetMarkdownTheme();
            
        }

        private void SetMarkdownTheme()
        {
           // MarkdownView.Theme = new BodyTheme();
        }
        public bool Animate { get; set; }

        public class BodyTheme : MarkdownTheme
        {
            public BodyTheme()
            {
                //heading 4 and 5 referes to how many #### are in front of the text. Keep this as a reference for later use
                //Heading4.FontFamily = "OpenSans-SemiBold"; 
                //Heading4.ForegroundColor = (Color)Application.Current.Resources["TextPrimaryColor"];

                //Heading5.FontFamily = Application.Current.Resources["FontRegular"] as string;
                //Heading5.ForegroundColor = (Color)Application.Current.Resources["TextPrimaryColor"];

                Paragraph.FontFamily = Application.Current.Resources["FontSemiBold"] as string;
                Paragraph.ForegroundColor = (Color)Application.Current.Resources["TextPrimaryColor"];
                Paragraph.FontSize = 16;
            }
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            newItemEntry.IsVisible = true;
            newItemEntry.Focus();
        }

        private void newItemEntry_Completed(object sender, EventArgs e)
        {
            newItemEntry.IsVisible = false;
            thisRecipe = (BindingContext as SingleRecipeData);
            if (newItemEntry.Text != "") 
                thisRecipe.Items.Add(newItemEntry.Text);
            newItemEntry.Text = "";
            // DYLAN: this needs a firebase method to push the item to the database.
        }
    }
}
