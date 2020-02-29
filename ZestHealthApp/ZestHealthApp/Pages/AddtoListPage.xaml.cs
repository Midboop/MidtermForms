using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZestHealthApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZestHealthApp.Services;

namespace ZestHealthApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddtoListPage : ContentPage
    {
        public AddtoListPage()
        {
            InitializeComponent();
            Shell.SetTabBarIsVisible(this, false);
            // Binds the context to the variables stored in ShoppingListView
            BindingContext = new ShoppingListView();
        }

        private void TaskEntry_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(ItemEntry.Text) || !string.IsNullOrEmpty(Amount.Text))
            {
                // If all of the entries have information it them, then it enables the add button
                SubmitButton.IsEnabled = true;
            }
            else if (string.IsNullOrEmpty(ItemEntry.Text) || string.IsNullOrEmpty(Amount.Text))
                SubmitButton.IsEnabled = false;
        }
        private async void Handle_OnFinish(object sender, System.EventArgs e)
        {
            await Shell.Current.Navigation.PopAsync();

        }
        private void SubmitAddItem(object sender, EventArgs e)
        {
            DependencyService.Get<IKeyboardHelper>().HideKeyboard();
            if (!string.IsNullOrEmpty(ItemEntry.Text) || !string.IsNullOrEmpty(Amount.Text))
                SubmitAnim.Play();
            
        }

        private async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PopAsync();
        }


    }
}