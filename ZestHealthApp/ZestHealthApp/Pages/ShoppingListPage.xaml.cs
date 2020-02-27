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
        string selectedItemName;
        public ShoppingListPage()
        {
            InitializeComponent();
            BindingContext = new ShoppingListView();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await (BindingContext as ShoppingListView).RefreshList();
        }

        private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedItemName = (e.CurrentSelection.FirstOrDefault() as ShoppingListItems)?.ItemName;
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("ShoppingAddItem");
        }
    }
}