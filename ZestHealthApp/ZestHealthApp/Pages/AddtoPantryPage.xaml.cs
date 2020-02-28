using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZestHealthApp.Services;
using ZestHealthApp.ViewModel;

namespace ZestHealthApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddtoPantryPage : ContentPage
    {
        public AddtoPantryPage()
        {
            InitializeComponent();
            Shell.SetTabBarIsVisible(this, false);
            CancelButton.Clicked += (sender, e) => CancelAnim.Play();
            MinDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            MaxDate = new DateTime(DateTime.Now.Year + 2, DateTime.Now.Month, DateTime.Now.Day);
            Today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            ExpDate = Today.Date.ToString("MM/dd");
            DatePicker datePicker = new DatePicker
            {
                MinimumDate = MinDate,
                MaximumDate = MaxDate,
                Date = Today
            };
        }
        DateTime MinDate { get; set; }
        DateTime MaxDate { get; set; }
        DateTime Today { get; set; }
        string ExpDate { get; set; }
        private void TaskEntry_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Item.Text) || !string.IsNullOrEmpty(Amount.Text))
            {
                Item.IsEnabled = true;
            }
            else if (string.IsNullOrEmpty(Item.Text) || string.IsNullOrEmpty(Amount.Text))
                Item.IsEnabled = false;
        }

        private async void Handle_OnFinish(object sender, System.EventArgs e) 
        {
            await Shell.Current.Navigation.PopAsync();

        }

        private async void SubmitAddItem(object sender, EventArgs e)
        {
            DependencyService.Get<IKeyboardHelper>().HideKeyboard();

            if (string.IsNullOrEmpty(Item.Text) || string.IsNullOrEmpty(Amount.Text) || string.IsNullOrEmpty(ExpDate))
                await App.Current.MainPage.DisplayAlert("Empty Values", "Please enter Item Name, Expiration Date, and Quantity.", "OK");
            else
            {
                await FirebaseHelper.AddPantryItem(Item.Text, Amount.Text, ExpDate);
                SubmitAnim.Play();
            }
          
        }

        private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            ExpDate = DateSelect.Date.ToString("MM/dd");
        }

    }
}