//using System;
//using System.Collections.Generic;
//using System.Text;
//using ZestHealthApp.Models;
//using ZestHealthApp.Services;
//using Xamarin.Forms;
//using System.Windows.Input;

//namespace ZestHealthApp.ViewModel
//{
//    public class LoginViewModel
//    {
//        //readonly IFacebookLoginService facebookLoginService;

//        public ICommand OnFacebookLoginSuccessCmd { get; }
//        public ICommand OnFacebookLoginErrorCmd { get; }
//        public ICommand OnFacebookLoginCancelCmd { get; }
//        public Command FacebookLogoutCmd { get; }

//        public LoginViewModel()
//        {
//            facebookLoginService = (Application.Current as App).FacebookLoginService;
//            facebookLoginService.AccessTokenChanged = (string oldToken, string newToken) => FacebookLogoutCmd.ChangeCanExecute();

//            FacebookLogoutCmd = new Command(() =>
//                facebookLoginService.Logout(),
//                () => !string.IsNullOrEmpty(facebookLoginService.AccessToken));

//            OnFacebookLoginSuccessCmd = new Command<string>(
//                (authToken) => DisplayAlert("Success", $"Authentication succeed: {authToken}"));

//            OnFacebookLoginErrorCmd = new Command<string>(
//                (err) => DisplayAlert("Error", $"Authentication failed: {err}"));

//            OnFacebookLoginCancelCmd = new Command(
//                () => DisplayAlert("Cancel", "Authentication cancelled by the user."));
//        }

//        void DisplayAlert(string title, string msg) =>
//            (Application.Current as App).MainPage.DisplayAlert(title, msg, "OK");
//    }
//}
