using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZestHealthApp.Models;
using ZestHealthApp.ViewModel;
using ZestHealthApp.Pages;

namespace ZestHealthApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShoppingListPage : ContentPage
    {
        ShoppingListItems selectedItem;

        int CurrentFrame;
        bool CartAnimComplete;
        bool EditAnimComplete;
        public ShoppingListPage()
        {
            InitializeComponent();
            BindingContext =new ShoppingListView();
            selectedItem = null;
            Today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            ExpDate = Today.Date.ToString("MM/dd");
        }
        DateTime Today { get; set; }
        string ExpDate { get; set; }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            ToggleCartAnimState(false, false);
            ToggleEditAnimState(false, false);
            await (BindingContext as ShoppingListView).RefreshList();
            AnimButton.PlayFrameSegment(0, 25);
            CurrentFrame = 25;
            selectedItem = null;
        }

        private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedItem = (e.CurrentSelection.FirstOrDefault() as ShoppingListItems);
            ToggleCartAnimState(true, false);
            CartAnimButton.PlayFrameSegment(0, 23);

            ToggleEditAnimState(true, false);
            EditAnimButton.PlayFrameSegment(0, 14);


            if (CurrentFrame == 25)
            {
                AnimButton.PlayFrameSegment(25, 45);
                CurrentFrame = 45;
            }
        }
        private async void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            await FirebaseHelper.AddPantryItem(selectedItem.ItemName, selectedItem.Amount, ExpDate); DateSelect.Date.ToString("MM/dd");
            (BindingContext as ShoppingListView).Delete.Execute(selectedItem);
            AnimButton.PlayFrameSegment(45, 125);
            CartAnimButton.PlayFrameSegment(23, 120); // use in between frames when adding to pantry <3
            CartAnimComplete = true;
            EditAnimButton.PlayFrameSegment(14, 48);
            EditAnimComplete = true;
            AnimButton.PlayFrameSegment(0, 25);
            CurrentFrame = 25;
            selectedItem = null;
            DateFrame.IsVisible = false;
            DateSelect.IsVisible = false;

        }

        private async void AddDeleteButton_Clicked(object sender, EventArgs e)
        {
            if (CurrentFrame == 25)
                await Shell.Current.GoToAsync("ShoppingAddItem");
            else if (CurrentFrame == 45)
            {
                (BindingContext as ShoppingListView).Delete.Execute(selectedItem);
                AnimButton.PlayFrameSegment(45, 125);
                CartAnimButton.PlayFrameSegment(98, 120); // use in between frames when adding to pantry <3
                CartAnimComplete = true;
                EditAnimButton.PlayFrameSegment(14, 48);
                EditAnimComplete = true;
                AnimButton.PlayFrameSegment(0, 25);
                CurrentFrame = 25;
                selectedItem = null;
            }
        }
        private void CartAnimButton_OnPlay(object sender, EventArgs e)
        {
            if (CartAnimComplete)
                ToggleCartAnimState(false, false);
        }

        private void EditAnimButton_OnPlay(object sender, EventArgs e)
        {
            if (EditAnimComplete)
                ToggleEditAnimState(false, false);
        }
        private void ToggleCartAnimState(bool buttonstate, bool complete)
        {
            CartButton.IsEnabled = buttonstate;
            CartAnimButton.IsVisible = buttonstate;
            CartAnimComplete = complete;
        }

        private void ToggleEditAnimState(bool buttonstate, bool complete)
        {
            EditButton.IsEnabled = buttonstate;
            EditAnimButton.IsVisible = buttonstate;
            EditAnimComplete = complete;
        }

        private void CartButton_Clicked(object sender, EventArgs e)
        {
            DateFrame.IsVisible = true;
            DateSelect.IsVisible = true;
        }

        private void EditButton_Clicked(object sender, EventArgs e)
        {
            EditNumberFrame.IsVisible = true;
            EditNumberEntry.IsVisible = true;
            EditAnimButton.IsVisible = false;
            CancelAimButton.IsVisible = true;
            CancelButton.IsVisible = true;
            CancelButton.IsEnabled = true;
            CartAnimButton.IsVisible = false;
            EditNumberEntry.Text = selectedItem.Amount;
        }

        private async void EditNumberEntry_Completed(object sender, EventArgs e)
        {
            EditNumberFrame.IsVisible = false;
            EditNumberEntry.IsVisible = false;
            CancelAimButton.IsVisible = false;
            CancelAimButton.IsEnabled = false;
            CancelButton.IsEnabled = false;
            CancelButton.IsVisible = false;
            await FirebaseHelper.UpdateShoppingList(selectedItem.ItemName, EditNumberEntry.Text);
            OnAppearing();
        }

        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            EditNumberFrame.IsVisible = false;
            EditNumberEntry.IsVisible = false;
            CancelAimButton.IsVisible = false;
            CancelButton.IsEnabled = false;

            OnAppearing();
        }
    }
}