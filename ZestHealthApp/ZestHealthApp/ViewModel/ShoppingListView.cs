using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZestHealthApp.Models;
using ZestHealthApp.Pages;

namespace ZestHealthApp.ViewModel
{
    class ShoppingListView : ContentPage
    {
        public ObservableCollection<ShoppingListItems> ShoppingList { get; set; }
        public string ItemName { get; set; }
        public string Amount { get; set; }

        public Command<ShoppingListItems> Delete { get; set; }
        public Command Popup { get; set; }

        public ShoppingListView()
        {
            GetShoppingItems().ContinueWith(t => { ShoppingList = new ObservableCollection<ShoppingListItems>(t.Result); });
            Delete = new Command<ShoppingListItems>(HandleDelete);
            Popup = new Command(LaunchAddItemPage);
        }

        public async Task RefreshList()
        {
            await GetShoppingItems().ContinueWith(t => { ShoppingList = new ObservableCollection<ShoppingListItems>(t.Result); });
        }

        private async Task<List<ShoppingListItems>> GetShoppingItems()
        {
            return (await FirebaseHelper.GetShoppingList());
        }


        public async void HandleDelete(ShoppingListItems items)
        {
            await FirebaseHelper.DeleteShoppingList(items.ItemName);
            await RefreshList();
        }

        public async void LaunchAddItemPage()
        {
            /// Add new shopping list page
            await Shell.Current.GoToAsync("ShoppingAddItem");
        }

        public Command AddShoppingListCommand
        {
            get
            {
                return new Command(() =>
                {
                    AddShoppingList();
                });
            }
        }

        private async void AddShoppingList()
        {
            if (string.IsNullOrEmpty(ItemName) || string.IsNullOrEmpty(Amount.ToString()))
                await App.Current.MainPage.DisplayAlert("Empty Values", "Please enter Item Name and Quantity.", "Ok");
            else
            {
                var shoppingList = await FirebaseHelper.AddShoppingList(ItemName, Amount);
                if(shoppingList)
                {
                    await RefreshList();
                }
                else
                    await App.Current.MainPage.DisplayAlert("Couldn't Add Item", "Please Try Again", "OK");
            }
        }
    }
}
