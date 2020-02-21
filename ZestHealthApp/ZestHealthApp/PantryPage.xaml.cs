
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

        //private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    selectedItemName = (e.CurrentSelection.FirstOrDefault() as PantryItems)?.ItemName;
        //}
    }
}