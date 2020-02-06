using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZestHealthApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ZestHealthApp.newViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        LoginViewModel login;
        public LoginPage()
        {
            InitializeComponent();
            login = new LoginViewModel();
        }

        void LoginClicked(object sender, EventArgs e)
        {
            // logs the user in if the user name and password match
            login.Login(EmailInput.Text, PasswordInput.Text);
        }

        async void SignUpClicked(object sender, EventArgs e)
        {
            // Creates new signup page if the signup button is clocked
            await Navigation.PushModalAsync(new XF_SignUpPage());
        }
    }


}