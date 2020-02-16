using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZestHealthApp.ViewModel;


namespace ZestHealthApp.newViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupEditQuantityView
    {
     
       
        public PopupEditQuantityView()
        {
            InitializeComponent();
            BindingContext = new PantryView();
          
        }
    
        private async void TaskEntry_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TaskEntry.Text))
            {
                
                TaskButton.IsEnabled = true;

               
            }
            else if (string.IsNullOrEmpty(TaskEntry.Text))
                TaskButton.IsEnabled = false;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await FirebaseHelper.UpdateQuantity(TaskEntry.Text, NameEntry.Text, ExpEntry.Text);
            


            await PopupNavigation.PopAsync();
            
        }
    }
}