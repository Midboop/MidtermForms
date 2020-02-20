
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;
using ZestHealthApp.newViews;
using ZestHealthApp.Models;
using ZestHealthApp.ViewModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ZestHealthApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PantryPage : ContentPage
    {
        FirebaseHelper helper;
        PantryItems items;
        PantryView view;
        string selectedItemName;

        public PantryPage()
        {
            helper = new FirebaseHelper();
            items = new PantryItems();
            view = new PantryView();
            InitializeComponent();
            BindingContext = new PantryView();
          
         
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await (BindingContext as PantryView).RefreshPantry();
        }

      
        private async void Button_Clicked(object sender, EventArgs e)
        {
            var popUp = new PopupNewTaskView();
            await PopupNavigation.PushAsync(popUp);
           // popUp.Disappearing += () => { OnAppearing(); };
           // The reason the items do not update unless you swap pages is when we
           // pop the page for the popup it does not call on appearing. Dylan can you find
           // a workaround? From what I've read, it has to do with how the navigation is implemented
        }

        private void PopUp_Disappearing(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private async void DeleteItem_Invoked(object sender, EventArgs e)
        {
            await FirebaseHelper.DeletePantryItem(selectedItemName);
            await (BindingContext as PantryView).RefreshPantry();

        }

        private async void MinusItem_Invoked(object sender, EventArgs e)
        {
            //int newQuantity = Convert.ToInt32(items.Quantity);
            //newQuantity = newQuantity - 1;
            //items.Quantity = newQuantity.ToString();
            await PopupNavigation.PushAsync(new PopupEditQuantityView());
           //await FirebaseHelper.UpdateQuantity(newQuantity.ToString());
            //Dylan -1 item to .quantity
        }

        private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // string previous = (e.PreviousSelection.FirstOrDefault() as PantryItems)?.ItemName;
            selectedItemName = (e.CurrentSelection.FirstOrDefault() as PantryItems)?.ItemName;

        }
    }
}