using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SkiaSharp.Views.Forms;
using ZestHealthApp.Services;
using ZestHealthApp.Pages;
using ZestHealthApp.Models;
using ZestHealthApp.ViewModel;

namespace ZestHealthApp
{
    public partial class App : Application
    {
        bool isLoggedIn;
        GoogleUsers users;
        public App()
        {
            if (Application.Current.Properties.ContainsKey("Id"))
                isLoggedIn = true;
            else
                isLoggedIn = false;

            InitializeComponent();
            users = new GoogleUsers();
            Device.SetFlags(new[] {
                "SwipeView_Experimental"
            });

            if(isLoggedIn)
            {
                MainPage = new AppShell();
            }
            
            if(!isLoggedIn)
                MainPage = new FacebookLoginPage();
              
          
                
        }
        private async void GetPath()
        {
            isLoggedIn = await FirebaseHelper.GetLoggedIn();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
