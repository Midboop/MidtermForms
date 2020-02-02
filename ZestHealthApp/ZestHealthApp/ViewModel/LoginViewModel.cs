using System;
using System.Collections.Generic;
using System.Text;
using ZestHealthApp.Models;

namespace ZestHealthApp.ViewModel
{
   public class LoginViewModel : Users
    {
        public async void Login(string email, string password)
        {
            if(string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                await App.Current.MainPage.DisplayAlert("Empty Values", "Please enter Email and Password", "OK");
            }
            else
            {
                var user = await FirebaseHelper.GetUser(email);

                if(user!=null)
                    if(email == user.Email && password == user.Password)
                    {
                        await App.Current.MainPage.DisplayAlert("Login Success", "", "OK");
                        await App.Current.MainPage.Navigation.PushModalAsync(new ProfilePage());
                    }
                    else
                        await App.Current.MainPage.DisplayAlert("Login Fail", "Please enter correct Email and Password", "OK");
                    else
                        await App.Current.MainPage.DisplayAlert("Login Fail", "User not found", "OK");
                
            }

        }
    }
}
