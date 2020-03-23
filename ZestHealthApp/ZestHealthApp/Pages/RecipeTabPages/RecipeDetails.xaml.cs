using Sharpnado.Presentation.Forms.CustomViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xam.Forms.Markdown;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZestHealthApp.Models;
using ZestHealthApp.ViewModel;

namespace ZestHealthApp.Pages.RecipeTabPages 
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeDetails : ContentView, IAnimatableReveal // Tab 1
    {
        SingleRecipeData thisRecipe;
        IngredientItem singleItem;
        IngredientItem previousItem;
        IngredientItem selectedItem;
        RecipeItems items;
        List<IngredientItem> ingredients;
        int CurrentFrame;
        public RecipeDetails()
        {
            items = new RecipeItems();
            InitializeComponent();
            ingredients = new List<IngredientItem>();
            CurrentFrame = 0;
            AddButton.Speed = 4.0f;
            selectedItem = null;
        }

        public bool Animate { get; set; }

        private void Button_Clicked(object sender, EventArgs e)
        {
            // Add item
            if(CurrentFrame == 25)
            {

                OKButton.IsVisible = true;
                OKButton.IsEnabled = true;
                NewItemEntry.IsVisible = true;
                IngredientsList.IsEnabled = false;
               
                QuantityEntry.Focus();
                singleItem = new IngredientItem();
                AddButton.PlayFrameSegment(25, 45);
                CurrentFrame = 45;
                return;
            }

            // Cancel
            if (CurrentFrame == 45)
            {
                ResetNewItemFrame();
                AddButton.PlayFrameSegment(45, 125);
                AddButton.PlayFrameSegment(0, 25);
                CurrentFrame = 25;
            }
        }

        private async void newItemEntry_Completed()
        {
            if (AddButton.IsVisible)
            {
                ingredients.Add(singleItem);
                items.IngredientsList = ingredients;
                
                NewItemEntry.IsVisible = false;
                thisRecipe = (BindingContext as SingleRecipeData);
                thisRecipe.Items.Add(singleItem);
                thisRecipe.UpdateCalories();
                ResetNewItemFrame();
                
                // DYLAN: this needs a firebase method to push the item to the database.
            }
            else
            {
                SaveEdit();
            }
        }

        private void IngredientsList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (previousItem == IngredientsList.SelectedItem)
            {
                IngredientsList.SelectedItem = null;
                AddButton.IsVisible = true;
                EditButtonFrame.IsVisible = false;
            }
            else
            {
                previousItem = selectedItem;
                selectedItem = e.Item as IngredientItem;
                if (!EditButtonFrame.IsVisible)
                {
                    AddButton.IsVisible = false;
                    EditButtonFrame.IsVisible = true;
                }
            }
        }

        private void QuantityEntry_Completed(object sender, EventArgs e)
        {
            if (QuantityEntry.Text.Length.ToString() != string.Empty)
            {
                //Go to Unit
                singleItem.Quantity = Convert.ToDouble(QuantityEntry.Text.ToString());
                QuantityEntry.IsEnabled = false;
                QuantityLabel.IsVisible = false;
                UnitEntry.IsVisible = true;
                UnitEntry.Focus();
                UnitLabel.IsVisible = true;
            }
            
        }

        private void UnitEntry_Completed(object sender, EventArgs e)
        {
            if(UnitEntry.Text.ToString() != string.Empty)
            {
                // Go to Weight
                singleItem.Unit = UnitEntry.Text.ToString();
                UnitEntry.IsEnabled = false;
                UnitLabel.IsVisible = false;
                WeightEntry.IsVisible = true;
                WeightEntry.Focus();
            }
        }

        private void WeightEntry_Completed(object sender, EventArgs e)
        {
            if(WeightEntry.Text.ToString() != string.Empty)
            {
                // Go to Name
                singleItem.Weight = Convert.ToInt32(WeightEntry.Text.ToString());
                WeightEntry.IsEnabled = false;
                NameEntry.IsVisible = true;
                NameEntry.Focus();
            }
        }

        private void NameEntry_Completed(object sender, EventArgs e)
        {
            if(NameEntry.Text.ToString() != string.Empty)
            {
                // Go to Calories
                singleItem.Name = NameEntry.Text.ToString();
                NameEntry.IsEnabled = false;
                CaloriesEntry.IsVisible = true;
                CaloriesEntry.Focus();
            }
        }

        private void CaloriesEntry_Completed(object sender, EventArgs e)
        {
            if(CaloriesEntry.Text.ToString() != string.Empty)
            {
                // Completed Item
                singleItem.Calories =Convert.ToInt32 (CaloriesEntry.Text.ToString());
                newItemEntry_Completed();
            }
        }

        private void ResetNewItemFrame()
        {
            NewItemEntry.IsVisible = false;

            // Re-enable all entries
            QuantityEntry.IsEnabled = true;
            UnitEntry.IsEnabled = true;
            WeightEntry.IsEnabled = true;
            NameEntry.IsEnabled = true;
            CaloriesEntry.IsEnabled = true;

            // Set Visibility back to default
            QuantityLabel.IsVisible = true;
            UnitLabel.IsVisible = false;
            UnitEntry.IsVisible = false;
            WeightEntry.IsVisible = false;
            NameEntry.IsVisible = false;
            CaloriesEntry.IsVisible = false;

            // Empty stored values
            QuantityEntry.Text = string.Empty;
            UnitEntry.Text = string.Empty;
            WeightEntry.Text = string.Empty;
            NameEntry.Text = string.Empty;
            CaloriesEntry.Text = string.Empty;

            IngredientsList.IsEnabled = true;
        }

        private void AddButton_HaltPlay(object sender, EventArgs e)
        {
            // Start 
            if (CurrentFrame == 0)
            {
                AddButton.Speed = 1.0f;
                AddButton.IsPlaying = false;
                AddButton.PlayFrameSegment(0, 25);
                CurrentFrame = 25;
            }

        }

        private void EditButton_OnClick(object sender, EventArgs e)
        {
            // Save
            if (CurrentFrame == 44)
            {
                SaveEdit();
            }
            else
            {
                singleItem = selectedItem;
                SetEditFrame(singleItem);
                EditButton.PlayFrameSegment(0, 44);
                CurrentFrame = 44;
            }
        }

        private void SetEditFrame(IngredientItem item)
        {

            // Set stored values
            QuantityEntry.Text = item.Quantity.ToString();
            UnitEntry.Text = item.Unit.ToString();
            WeightEntry.Text = item.Weight.ToString();
            NameEntry.Text = item.Name.ToString();
            CaloriesEntry.Text = item.Calories.ToString();

            NewItemEntry.IsVisible = true;
        }

        private void SaveEdit()
        {
            thisRecipe = (BindingContext as SingleRecipeData);
            for (int i = 0; i < thisRecipe.Items.Count; i++)
            {
                if (selectedItem == thisRecipe.Items.ElementAt(i))
                {
                    thisRecipe.Items.ElementAt(i).Equals(singleItem);
                    IngredientsList.SelectedItem = null;
                    previousItem = null;
                    selectedItem = null;
                    EditButton.PlayFrameSegment(44, 63);
                    EditButton.PlayFrameSegment(63, 2);
                    ResetNewItemFrame();
                    EditButtonFrame.IsVisible = false;
                    AddButton.IsVisible = true;
                    AddButton.PlayFrameSegment(0, 25);
                    CurrentFrame = 25;
                }
                    
            }
        }



        private void TitleEntry_Completed(object sender, EventArgs e)
        {
            items.RecipeTitle = TitleEntry.Text;
        }

        private async void OKButton_Clicked(object sender, EventArgs e)
        {
            await FirebaseHelper.AddRecipe(items, items.RecipeTitle);
            OKButton.IsVisible = false;
            OKButton.IsEnabled = false;
            App.Current.MainPage = new AppShell();
        }
    }
}
