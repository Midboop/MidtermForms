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
            Device.SetFlags(new[] {
                "SwipeView_Experimental"
            });
           

                 
                MainPage = new FacebookLoginPage();
              
          
                
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
