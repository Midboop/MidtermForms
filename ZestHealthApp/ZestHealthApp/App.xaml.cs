using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SkiaSharp.Views.Forms;
using ZestHealthApp.Services;
using ZestHealthApp.Pages;

namespace ZestHealthApp
{
    public partial class App : Application
    {
      

        public App()
        {
            InitializeComponent();


            MainPage = new AppShell();
            //MainPage = new FacebookLoginPage();
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
