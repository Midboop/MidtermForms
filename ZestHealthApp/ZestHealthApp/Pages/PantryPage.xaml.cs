
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
        public PantryPage()
        {
            
            helper = new FirebaseHelper();
            items = new PantryItems();
            view = new PantryView();
            InitializeComponent();
            BindingContext = new PantryView();
            CurrentFrame = 0;
          
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await (BindingContext as PantryView).RefreshPantry();
            AnimButton.PlayFrameSegment(0, 25);
            CurrentFrame = 25;
            selectedItem = null;
        }

        private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedItem = (e.CurrentSelection.FirstOrDefault() as PantryItems);

            if (CurrentFrame == 25)
            {
                AnimButton.PlayFrameSegment(25, 45);
                CurrentFrame = 45;
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (CurrentFrame == 25)
                await Shell.Current.GoToAsync("PantryAddItem");
            else if (CurrentFrame == 45)
            {
               (BindingContext as PantryView).Delete.Execute(selectedItem);
                AnimButton.PlayFrameSegment(45, 25);
                CurrentFrame = 25;
                selectedItem = null;
            }
        }

    }
}