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
            // Binds the context to the variables stored in ShoppingListView
            BindingContext = new ShoppingListView();
        }

        private void TaskEntry_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(ItemEntry.Text) || !string.IsNullOrEmpty(Amount.Text))
            {
                // If all of the entries have information it them, then it enables the add button
                TaskButton.IsEnabled = true;
            

               
            }
            else if (string.IsNullOrEmpty(ItemEntry.Text) || string.IsNullOrEmpty(Amount.Text))
                TaskButton.IsEnabled = false;
        }

        private async void TaskButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PopAsync();
        }

        private async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PopAsync();
        }


    }
}