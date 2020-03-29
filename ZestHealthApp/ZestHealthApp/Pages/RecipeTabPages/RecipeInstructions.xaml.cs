using Sharpnado.Presentation.Forms.CustomViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ZestHealthApp.Pages.RecipeTabPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeInstructions : ContentView, IAnimatableReveal // Tab 2
    {
        public bool isSaved { get; private set; }
        public RecipeInstructions()
        {
            InitializeComponent();
            isSaved = true;
        }

        public bool Animate { get; set; }

        private void AddButton_OnClick(object sender, EventArgs e)
        {
            var previousEditor = (stackLayout.Children.ElementAt(stackLayout.Children.Count - 2) as Editor);
            if (previousEditor.Text != null)
            {
                string stepNum = (stackLayout.Children.Count() / 2 + 1).ToString();
                var label = new Label { Text = "Step " + stepNum, TextColor = Color.White, Style = (Style)Application.Current.Resources["TextSubhead"] };
                var editor = new Editor { Placeholder = "Instructions", PlaceholderColor = Color.Accent, TextColor = Color.White, BackgroundColor = Color.Transparent, AutoSize = EditorAutoSizeOption.TextChanges, Style = (Style)Application.Current.Resources["TextBody"] };
                editor.TextChanged += Editor_TextChanged;
                stackLayout.Children.Insert(stackLayout.Children.Count - 1, label);
                stackLayout.Children.Insert(stackLayout.Children.Count - 1, editor);
            }
            else
                previousEditor.Placeholder = "Add Instructions Here First Before Adding Next Step";
        }

        private void Editor_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (isSaved)
            {
                SaveAnim.IsVisible = true;
                isSaved = false;
            }
            Editor editor = (sender as Editor);
            editor.BackgroundColor = Color.Accent;
        }

        private void SaveAnim_OnClick(object sender, EventArgs e)
        {
            SaveAnim.IsVisible = false;
            isSaved = true;

            SavePage();
            for (int i = 1; i < stackLayout.Children.Count; i++)
            {
                // all odd elements are of the Editor Type
                if (i % 2 == 0)
                    i++;
                if (i < stackLayout.Children.Count)
                {
                    var thisEditor = (stackLayout.Children.ElementAt(i) as Editor);
                    thisEditor.BackgroundColor = Color.Transparent;
                }
            }

        }
        public void SavePage()
        {
            if (((Editor)stackLayout.Children.ElementAt(stackLayout.Children.Count - 2)).Text == null && stackLayout.Children.Count > 3)
            {
                Label stepLabel = (Label)stackLayout.Children.ElementAt(stackLayout.Children.Count - 3);
                Editor editor = (Editor)stackLayout.Children.ElementAt(stackLayout.Children.Count - 2);

                stackLayout.Children.Remove(stepLabel);
                stackLayout.Children.Remove(editor);

                //TODO: Save Stuff Here
            }
        }
    }
}