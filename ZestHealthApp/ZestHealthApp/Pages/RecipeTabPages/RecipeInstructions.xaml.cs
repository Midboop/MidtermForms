using Sharpnado.Presentation.Forms.CustomViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZestHealthApp.ViewModel;

namespace ZestHealthApp.Pages.RecipeTabPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeInstructions : ContentView, IAnimatableReveal // Tab 2
    {
        SingleRecipeData thisRecipe;
        public RecipeInstructions()
        {
            InitializeComponent();
        }

        public bool Animate { get; set; }

        private void AddButton_OnClick(object sender, EventArgs e)
        {
            var previousEditor = (stackLayout.Children.ElementAt(stackLayout.Children.Count -2) as Editor);
            if (previousEditor.Text != null)
            {
                var label = new Label { Text = "Step #", TextColor = Color.White, Style = (Style)Application.Current.Resources["TextSubhead"] };
                var editor = new Editor { Placeholder = "Instructions", PlaceholderColor = Color.Accent, TextColor = Color.White, BackgroundColor = Color.Transparent, AutoSize = EditorAutoSizeOption.TextChanges, Style = (Style)Application.Current.Resources["TextBody"] };
                stackLayout.Children.Insert(stackLayout.Children.Count - 1, label);
                stackLayout.Children.Insert(stackLayout.Children.Count - 1, editor);
            }
            else
                previousEditor.Placeholder = "Add Instructions Here First Before Adding Next Step";
        }
    }
}