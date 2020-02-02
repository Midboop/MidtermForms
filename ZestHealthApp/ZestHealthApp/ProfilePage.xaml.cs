using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZestHealthApp.newViews;
using ZestHealthApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZestHealthApp.ViewModel;


namespace ZestHealthApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {


      
        public ProfilePage()
        {
            
            InitializeComponent();
            BindingContext = this;


           
         
 
        }

        async private void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new LoginPage());
        }


    }
}