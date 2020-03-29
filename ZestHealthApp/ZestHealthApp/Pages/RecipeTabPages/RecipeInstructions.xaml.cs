using Sharpnado.Presentation.Forms.CustomViews;
using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZestHealthApp.Models;
using ZestHealthApp.ViewModel;

namespace ZestHealthApp.Pages.RecipeTabPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeInstructions : ContentView, IAnimatableReveal // Tab 2
    {
        public SingleRecipeData preSavedRecipe { get; set; }
        public bool isSaved { get; private set; }
        public RecipeInstructions()
        {
            InitializeComponent();
            isSaved = true;
          
        }

        public bool Animate { get; set; }

        private void AddButton_OnClick(object sender, EventArgs e)
        {
            
            var previousEditor = (stackLayout.Children.ElementAt(stackLayout.Children.Count -2) as Editor);
            if (previousEditor.Text != string.Empty)
            {
                string stepNum = (stackLayout.Children.Count / 2 + 1).ToString();
                AddNewSet(stepNum, string.Empty);
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
            for (int i = 0; i < stackLayout.Children.Count -1; i++)
            {
                // all odd elements are of Editor Type
                if (i % 2 == 0)
                    i++;
               else if (i < stackLayout.Children.Count -1)
                {
                    var thisEditor = (stackLayout.Children.ElementAt(i) as Editor);
                    thisEditor.BackgroundColor = Color.Transparent;
                }
            }

        }
        public void SavePage()
        {
            if (((Editor)stackLayout.Children.Last()).Text == null && stackLayout.Children.Count > 3)
            {
                // This is done twice to remove both the editor and Label (in that order)
                stackLayout.Children.Remove(stackLayout.Children.ElementAt(stackLayout.Children.Count-2));
                stackLayout.Children.Remove(stackLayout.Children.ElementAt(stackLayout.Children.Count - 2));
            }
            ConvertInstructions();

        }
        public void ConvertInstructions()
        {
            int stepCounter = 1;
            List<InstructionItem> instructions = new List<InstructionItem>();
            InstructionItem newSet = new InstructionItem();
           
            for(int i = 0; i < stackLayout.Children.Count -1; i++)
            {
                // Even elements are labels
                if (i % 2 == 0)
                {
                    newSet = new InstructionItem();
                    newSet.Step = stepCounter;
                    stepCounter++;
                      
                }
                else
                {
                    newSet.Directive = ((Editor)stackLayout.Children.ElementAt(i)).Text;
                    instructions.Add(newSet);
                }
            }
            preSavedRecipe = (BindingContext as SingleRecipeData);
            FirebaseHelper.SaveInstructions(preSavedRecipe,instructions);
            preSavedRecipe.Instructions = instructions;
           
        }
        private void AddNewSet(string stepNum, string directive)
        {

            var label = new Label { Text = "Step " + stepNum, TextColor = Color.White, Style = (Style)Application.Current.Resources["TextSubhead"] };
            var editor = new Editor {Text = directive, Placeholder = "Instructions", PlaceholderColor = Color.Accent, TextColor = Color.White, BackgroundColor = Color.Transparent, AutoSize = EditorAutoSizeOption.TextChanges, Style = (Style)Application.Current.Resources["TextBody"] };
            editor.TextChanged += Editor_TextChanged;
            stackLayout.Children.Insert((stackLayout.Children.Count - 1), label);
            stackLayout.Children.Insert((stackLayout.Children.Count - 1), editor);
        }
        public void InitializeInstructions()
        {
            preSavedRecipe = (BindingContext as SingleRecipeData);
            List<InstructionItem> list = preSavedRecipe.Instructions;
            for(int i = 0; i < list.Count; i++)
            {
                AddNewSet(list[i].Step.ToString(), list[i].Directive);
            }
        }
    }
}