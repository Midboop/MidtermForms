using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using firebasesample.Services.FirebaseAuth;
using firebasesample.ViewModels.Base;
using firebasesample.ViewModels.Main;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZestHealthApp.Tables;

namespace ZestHealthApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

    }
}