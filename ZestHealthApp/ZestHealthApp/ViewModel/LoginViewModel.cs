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
                // If there is a blank input
                await App.Current.MainPage.DisplayAlert("Empty Values", "Please enter Email and Password", "OK");
            }
            else
            {
                var user = await FirebaseHelper.GetUser(email);

                if(user!=null)
                    if(email == user.Email && password == user.Password)
                    {
                        // if login is successful
                        await App.Current.MainPage.DisplayAlert("Login Success", "", "OK");
                         App.Current.MainPage = new AppShell(); // This is why the navigation bar didn't work. The main page MUST be an AppShell
                        
                    }
                    else
                        await App.Current.MainPage.DisplayAlert("Login Fail", "Please enter correct Email and Password", "OK"); // one way for unsuccessful login
                    else
                        await App.Current.MainPage.DisplayAlert("Login Fail", "User not found", "OK");
                
            }

        }
    }
}
