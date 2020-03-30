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
        bool enable = false;
        public RecipeInstructions()
        {
            InitializeComponent();
            isSaved = true;

        }

        public bool Animate { get; set; }

        private void AddButton_OnClick(object sender, EventArgs e)
        {
            enable = true;
            var previousEditor = (stackLayout.Children.ElementAt(stackLayout.Children.Count - 2) as Editor);
            if (previousEditor.Text != null)
            {
                string stepNum = (stackLayout.Children.Count / 2 + 1).ToString();
                AddNewSet(stepNum, null);
                
            }
            else
                previousEditor.Placeholder = "Add Instructions Here First Before Adding Next Step";
        }

        private void Editor_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            if (enable == true)
            {
                if (isSaved)
                {
                    SaveAnim.IsVisible = true;
                    isSaved = false;
                }
                Editor editor = (sender as Editor);
                editor.BackgroundColor = Color.Accent;
            }

        }

        private void SaveAnim_OnClick(object sender, EventArgs e)
        {
            SaveAnim.IsVisible = false;
            isSaved = true;

            SavePage();
            for (int i = 0; i < stackLayout.Children.Count - 1; i++)
            {
                // all odd elements are of Editor Type
                if (i % 2 == 0)
                    i++;

                if (i < stackLayout.Children.Count - 1)
                {
                    var thisEditor = (stackLayout.Children.ElementAt(i) as Editor);
                    thisEditor.BackgroundColor = Color.Transparent;
                }
            }

        }
        public void SavePage()
        {
            while ((((Editor)stackLayout.Children.ElementAt(stackLayout.Children.Count - 2)).Text == null || ((Editor)stackLayout.Children.ElementAt(stackLayout.Children.Count - 2)).Text == string.Empty )&& stackLayout.Children.Count > 3)
            {
                // This is done twice to remove both the editor and Label (in that order)
                stackLayout.Children.Remove(stackLayout.Children.ElementAt(stackLayout.Children.Count - 2));
                stackLayout.Children.Remove(stackLayout.Children.ElementAt(stackLayout.Children.Count - 2));
            }
            ConvertInstructions();

        }
        public void ConvertInstructions()
        {
            int stepCounter = 2;
            List<InstructionItem> instructions = new List<InstructionItem>();
            InstructionItem newSet = new InstructionItem();
            newSet.Step = 1;
            newSet.Directive = firstEditor.Text;
            instructions.Add(newSet);
            if (stackLayout.Children.Count > 3)
            {
                for (int i = 2; i < stackLayout.Children.Count - 1; i++)
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
                        if (((Editor)stackLayout.Children.ElementAt(i)).Text != string.Empty && ((Editor)stackLayout.Children.ElementAt(i)).Text != null)
                        {
                            newSet.Directive = ((Editor)stackLayout.Children.ElementAt(i)).Text;
                            instructions.Add(newSet);
                        }
                    }

                }
            }
            preSavedRecipe = (BindingContext as SingleRecipeData);
            FirebaseHelper.SaveInstructions(preSavedRecipe, instructions);
            preSavedRecipe.Instructions = instructions;

        }
        private void AddNewSet(string stepNum, string directive)
        {

            var label = new Label { Text = "Step " + stepNum, TextColor = Color.White, Style = (Style)Application.Current.Resources["TextSubhead"] };
            var editor = new Editor { Text = directive, Placeholder = "Instructions", PlaceholderColor = Color.Accent, TextColor = Color.White, BackgroundColor = Color.Transparent, AutoSize = EditorAutoSizeOption.TextChanges, Style = (Style)Application.Current.Resources["TextBody"] };
            editor.TextChanged += Editor_TextChanged;
            stackLayout.Children.Insert((stackLayout.Children.Count - 1), label);
            stackLayout.Children.Insert((stackLayout.Children.Count - 1), editor);
        }
        public void InitializeInstructions()
        {
            preSavedRecipe = (BindingContext as SingleRecipeData);
            List<InstructionItem> list = preSavedRecipe.Instructions;
            if (list[0].Directive != null)
            {
                firstEditor.Text = list[0].Directive;
                firstEditor.BackgroundColor = Color.Transparent;
                for (int i = 1; i < list.Count; i++)
                {
                    AddNewSet(list[i].Step.ToString(), list[i].Directive);
                }
            }
        }
    }
}