using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZestHealthApp.ViewModel;
using ZestHealthApp.Models;
using System.Diagnostics;

namespace ZestHealthApp.newViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupNewTaskView
    {
     
        PantryView pantryView;
        
        public PopupNewTaskView()
        {
            InitializeComponent();
            pantryView = new PantryView();
            BindingContext = pantryView;
       
        }

        private async void TaskEntry_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TaskEntry.Text) || !string.IsNullOrEmpty(TaskEntry1.Text) || !string.IsNullOrEmpty(TaskEntry2.Text))
            {

                TaskButton.IsEnabled = true;


               
               
            }
            else if (string.IsNullOrEmpty(TaskEntry.Text) || string.IsNullOrEmpty(TaskEntry1.Text) || string.IsNullOrEmpty(TaskEntry2.Text))
                TaskButton.IsEnabled = false;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            
            
            await PopupNavigation.PopAsync();
            
        }
    }
}