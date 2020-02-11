using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SkiaSharp.Views.Forms;
using ZestHealthApp.Services;
using ZestHealthApp.Pages;
using ZestHealthApp.Models;

namespace ZestHealthApp
{
    public partial class App : Application
    {

        GoogleUsers users;
        public App()
        {
            InitializeComponent();
            users = new GoogleUsers();
           // Application.Current.Properties.Remove("IsLoggedIn");
            
            

              bool isLoggedIn = Current.Properties.ContainsKey("IsLoggedIn") ? Convert.ToBoolean(Current.Properties["IsLoggedIn"]) : false;
           
            if (isLoggedIn == false)
            {
                 
                MainPage = new FacebookLoginPage();
               // MainPage = new NavigationPage(new FacebookLoginPage());
            }
            else
            {
                MainPage = new AppShell();
               // MainPage = new NavigationPage(new AppShell());
            }
                
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
