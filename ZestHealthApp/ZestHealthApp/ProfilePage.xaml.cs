using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZestHealthApp.newViews;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZestHealthApp.Models;
using ZestHealthApp.Pages;
using ZestHealthApp.ViewModel;
using System.ComponentModel;
using Firebase.Auth;
using Firebase;

namespace ZestHealthApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        FacebookVM vm;
        GoogleVM vm2;
        
        public ProfilePage()
        {
            InitializeComponent();
            vm = new FacebookVM();
            vm2 = new GoogleVM();

            Login();

        }

        //async private void Button_Clicked(object sender, EventArgs e)
        //{

        //    // Logs the user out
        //    await Navigation.PushModalAsync(new FacebookLoginPage());


        //}


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

 
                if (Application.Current.Properties["ProfilePicture"] != null)
                    imgProfilePicture.Source = Application.Current.Properties["ProfilePicture"].ToString();


                if (Application.Current.Properties["FirstName"] != null)
                    lblFirstNameValue.Text = lblFirstNameValue.Text + Application.Current.Properties["FirstName"].ToString();

                if (Application.Current.Properties["LastName"] != null)
                    lblLastNameValue.Text = lblLastNameValue.Text + Application.Current.Properties["LastName"].ToString();

                if (Application.Current.Properties["EmailAddress"] != null)
                    lblEmailAddressValue.Text = lblEmailAddressValue.Text + Application.Current.Properties["EmailAddress"].ToString();












        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Application.Current.Properties["IsLoggedIn"] = Boolean.FalseString;
            Application.Current.Properties.Remove("Id");
            Application.Current.Properties.Remove("FirstName");
            Application.Current.Properties.Remove("LastName");
            Application.Current.Properties.Remove("DisplayName");
            Application.Current.Properties.Remove("EmailAddress");
            Application.Current.Properties.Remove("ProfilePicture");
            await Navigation.PushModalAsync(new FacebookLoginPage());
            
        }
    }
}