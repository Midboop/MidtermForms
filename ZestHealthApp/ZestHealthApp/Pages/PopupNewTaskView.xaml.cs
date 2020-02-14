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
        GoogleUsers users;
        PantryView pantryView;
        public PopupNewTaskView()
        {
            InitializeComponent();
            pantryView = new PantryView();
            BindingContext = pantryView;
            users = new GoogleUsers();
        }
        List<Object> itemInfo = new List<object>();
        private async void TaskEntry_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TaskEntry.Text) || !string.IsNullOrEmpty(TaskEntry1.Text) || !string.IsNullOrEmpty(TaskEntry2.Text))
            {

                TaskButton.IsEnabled = true;
                //itemInfo.Add(TaskEntry.Text);
                //itemInfo.Add(TaskEntry1.Text);
                //itemInfo.Add(TaskEntry2.Text);
                //users.PantryList.Add(itemInfo);
                //users.PantryList.ForEach(Console.WriteLine);

                Debug.WriteLine("Items: " + users.PantryList);
               
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