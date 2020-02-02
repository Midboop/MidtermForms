
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;
using System.Threading.Tasks;



namespace ZestHealthApp.ViewModel
{
    public class SignUpVM : INotifyPropertyChanged
    {
        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Email"));
            }
        }

        private string password;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }

        //private string confirmpassword;

        //public string ConfirmPassowrd
        //{
        //    get { return confirmpassword; }
        //    set
        //    {
        //        confirmpassword = value;
        //        PropertyChanged(this, new PropertyChangedEventArgs("ConfirmPassword"));
        //    }
        //}

        public Command SignUpCommand
        {
            get
            {
                return new Command(() =>
                {
                   // if (Password == ConfirmPassowrd)
                        SignUp();
                    //else
                    //    App.Current.MainPage.DisplayAlert("", "Password Must be the same", "OK");
                });
            }
        }

        private async void SignUp()
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Name))
                await App.Current.MainPage.DisplayAlert("Empty Values", "Please enter Username, Name, and Password", "OK");
            else
            {
                var user = await FirebaseHelper.AddUser(Email, Password, Name);
                if(user)
                {
                    await App.Current.MainPage.DisplayAlert("SignUp Success!", "", "OK");

                    await App.Current.MainPage.Navigation.PushModalAsync(new ProfilePage());
                }
                else
                    await App.Current.MainPage.DisplayAlert("Error", "SignUp Fail", "OK");
            }
           
        }
    }
}
