﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZestHealthApp.ViewModel;

namespace ZestHealthApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddtoPantryPage : ContentPage
    {
        PantryPage page;
        public AddtoPantryPage()
        {
            InitializeComponent();
           // page = new PantryPage();
        }

        private void TaskEntry_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TaskEntry.Text) || !string.IsNullOrEmpty(TaskEntry1.Text) || !string.IsNullOrEmpty(TaskEntry2.Text))
            {

                TaskButton.IsEnabled = true;




            }
            else if (string.IsNullOrEmpty(TaskEntry.Text) || string.IsNullOrEmpty(TaskEntry1.Text) || string.IsNullOrEmpty(TaskEntry2.Text))
                TaskButton.IsEnabled = false;
        }

        private async void TaskButton_Clicked(object sender, EventArgs e)
        {
            await FirebaseHelper.AddPantryItem(TaskEntry.Text, TaskEntry1.Text, TaskEntry2.Text);

            // Sends it back to the main page
            await Shell.Current.Navigation.PopAsync();




        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PopAsync();
        }
    }
}