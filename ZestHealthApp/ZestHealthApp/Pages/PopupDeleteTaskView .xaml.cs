using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZestHealthApp.ViewModel;

namespace ZestHealthApp.newViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupDeleteTaskView
    {
      
       
        public PopupDeleteTaskView()
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
            await FirebaseHelper.DeletePantryItem(TaskEntry.Text);
            


            await PopupNavigation.PopAsync();
            
        }
    }
}