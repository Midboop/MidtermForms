using MagicGradients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZestHealthApp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Rg.Plugins.Popup.Services;
using ZestHealthApp.newViews;
using ZestHealthApp.Models;
using ZestHealthApp.ViewModel;

namespace ZestHealthApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PantryPage : ContentPage
    {
        FirebaseHelper helper;
        PantryItems items;
        public PantryPage()
        {
            helper = new FirebaseHelper();
            items = new PantryItems();
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

        private void DeleteItem_Invoked(object sender, EventArgs e)
        {
            //Dylan add delete item method
        }

        private void MinusItem_Invoked(object sender, EventArgs e)
        {
            //Dylan -1 item to .quantity
        }
    }
}