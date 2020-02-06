using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZestHealthApp.newViews;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZestHealthApp.Models;
using ZestHealthApp.ViewModel;
using System.ComponentModel;
using Firebase.Auth;
using Firebase;

namespace ZestHealthApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        
        
        public ProfilePage()
        {
            InitializeComponent();
            
            
            Login();
            
        }

      async  private void Button_Clicked(object sender, EventArgs e)
        {
           
            // Logs the user out
            await Navigation.PushModalAsync(new LoginPage());
            
            
        }

       
        /// Don't worry about this stuff, it's me trying to get it to display the username from the firebase data base
       // public event PropertyChangedEventHandler PropertyChanged;

        //private string displayName;
        //public string DisplayName
        //{
        //    get { return displayName; }
        //    set
        //    {
        //        displayName = value;
        //        OnPropertyChanged(nameof(DisplayName));

        //    }
        //}

        public async void Login()
        {
            // same here, maybe one day it will work
           // var user = await FirebaseHelper.GetName();


            DisplayName.Text = $"Welcome to Profile!";

                
            






        }






    }
}