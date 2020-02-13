using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZestHealthApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZestHealthApp.Models;


namespace ZestHealthApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FacebookLoginPage : ContentPage
    {
        GoogleVM vm;
        FacebookVM vm2;
        FirebaseHelper firebase;
        GoogleUsers gUsers;
        public FacebookLoginPage()
        {
            InitializeComponent();
            vm = new GoogleVM();
            vm2 = new FacebookVM();
            firebase = new FirebaseHelper();
            gUsers = new GoogleUsers();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            vm.GoogleLogin();
          
            
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            vm2.FacebookLogin();
        }
    }
}