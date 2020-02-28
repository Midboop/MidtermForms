
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;

using ZestHealthApp.Models;
using ZestHealthApp.ViewModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ZestHealthApp.Pages;
using ZestHealthApp.ViewModel;

namespace ZestHealthApp
{   
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PantryPage : ContentPage
    {
        FirebaseHelper helper;
        PantryItems items;
        PantryView view;
        PantryItems selectedItem;
        int CurrentFrame;
        bool CartAnimComplete;
        bool EditAnimComplete;
        public PantryPage()
        {
            
            helper = new FirebaseHelper();
            items = new PantryItems();
            view = new PantryView();
            InitializeComponent();
            BindingContext = new PantryView();
            CurrentFrame = 0;
            CartAnimComplete = false;
            EditAnimComplete = false;
          
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            ToggleCartAnimState(false, false);
            ToggleEditAnimState(false, false);
            await (BindingContext as PantryView).RefreshPantry();
            AnimButton.PlayFrameSegment(0, 25);
            CurrentFrame = 25;
            selectedItem = null;

        }

        private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedItem = (e.CurrentSelection.FirstOrDefault() as PantryItems);
            ToggleCartAnimState(true, false);
            CartAnimButton.PlayFrameSegment(0, 50);

            ToggleEditAnimState(true, false);
            EditAnimButton.PlayFrameSegment(0, 14);


            if (CurrentFrame == 25)
            {
                AnimButton.PlayFrameSegment(25, 45);
                CurrentFrame = 45;
            }
        }

        private async void AddDeleteButton_Clicked(object sender, EventArgs e)
        {
            if (CurrentFrame == 25)
                await Shell.Current.GoToAsync("PantryAddItem");
            else if (CurrentFrame == 45)
            {
               (BindingContext as PantryView).Delete.Execute(selectedItem);
                AnimButton.PlayFrameSegment(45, 125);
                CartAnimButton.PlayFrameSegment(50, 102); // use frame 160 if item is added to shopping cart
                CartAnimComplete = true;
                EditAnimButton.PlayFrameSegment(14,48);
                EditAnimComplete = true;
                AnimButton.PlayFrameSegment(0, 25);
                CurrentFrame = 25;
                selectedItem = null;
            }
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
    }
}