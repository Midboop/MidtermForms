
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;
using ZestHealthApp.newViews;
using ZestHealthApp.Models;
using ZestHealthApp.ViewModel;
using System.Diagnostics;

namespace ZestHealthApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PantryPage : ContentPage
    {
        FirebaseHelper helper;
        PantryItems items;
        PantryView view;
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


        private void Button_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.PushAsync(new PopupNewTaskView());
        }

        private async void DeleteItem_Invoked(object sender, EventArgs e)
        {
             await PopupNavigation.PushAsync(new PopupDeleteTaskView());
            

            //await FirebaseHelper.GetPantry();
            //string test = items.ItemName;
            //string name = view.ItemName;
            //Debug.WriteLine($"Item: {test}");

            //await FirebaseHelper.DeletePantryItem(test);
            //Dylan add delete item method

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

 


    }
}