using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZestHealthApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ZestHealthApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddtoListPage : ContentPage
    {
        public AddtoListPage()
        {
            InitializeComponent();
            BindingContext = new ShoppingListView();
        }

        private async void TaskEntry_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(ItemEntry.Text) || !string.IsNullOrEmpty(Amount.Text))
            {

                TaskButton.IsEnabled = true;
            

               
            }
            else if (string.IsNullOrEmpty(ItemEntry.Text) || string.IsNullOrEmpty(Amount.Text))
                TaskButton.IsEnabled = false;
        }

        private async void TaskButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}