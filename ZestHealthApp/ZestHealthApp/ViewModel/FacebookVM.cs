using System;
using System.Linq;
using System.Diagnostics;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Auth;
using System.Net.Http;
using ZestHealthApp.Services;
using ZestHealthApp.Models;

namespace ZestHealthApp.ViewModel
{
    public class FacebookVM : ContentPage
    {
        Account account;
        AccountStore store;
        public bool faceBook = false;
        public FacebookVM()
        {


            store = AccountStore.Create();

        }
        public void FacebookLogin()
        {
           
            string clientId = null;
            string redirectUri = null;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    clientId = Constants.FacebookiOSClientId;
                    redirectUri = Constants.FacebookiOSRedirectUrl;
                    break;

                case Device.Android:
                    clientId = Constants.FacebookAndroidClientId;
                    redirectUri = Constants.FacebookAndroidRedirectUrl;
                    break;
            }

            account = store.FindAccountsForService(Constants.AppName).FirstOrDefault();

            var authenticator = new OAuth2Authenticator(
                clientId,
                Constants.FacebookScope,
                new Uri(Constants.FacebookAuthorizeUrl),
                new Uri(Constants.FacebookAccessTokenUrl),
                null);

            authenticator.Completed += OnAuthCompleted;
            authenticator.Error += OnAuthError;

            AuthenticationState.Authenticator = authenticator;

            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(authenticator);
        }

        [Obsolete]
        async void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }
           

            if (e.IsAuthenticated)
            {
                if (authenticator.AuthorizeUrl.Host == "www.facebook.com")
                {
                    FacebookEmail facebookEmail = null;
                   
                    

                    var httpClient = new HttpClient();

                    var json = await httpClient.GetStringAsync($"https://graph.facebook.com/me?fields=id,name,first_name,last_name,email,picture.type(large)&access_token=" + e.Account.Properties["access_token"]);

                    facebookEmail = JsonConvert.DeserializeObject<FacebookEmail>(json);

                    await store.SaveAsync(account = e.Account, Constants.AppName);
                    Application.Current.Properties["IsLoggedIn"] = Boolean.TrueString;

                    bool newuser = false;
                    if (await FirebaseHelper.CheckFacebookEmail(facebookEmail.Email) == false)
                    {
                        await FirebaseHelper.AddFacebookUser(facebookEmail.Id,facebookEmail.Name, facebookEmail.First_Name, facebookEmail.Last_Name, facebookEmail.Email, facebookEmail.Picture);
                        newuser = true;
                    }

                    Application.Current.Properties.Remove("Id");
                    Application.Current.Properties.Remove("FirstName");
                    Application.Current.Properties.Remove("LastName");
                    Application.Current.Properties.Remove("DisplayName");
                    Application.Current.Properties.Remove("EmailAddress");
                    Application.Current.Properties.Remove("ProfilePicture");

                    Application.Current.Properties.Add("Id", facebookEmail.Id);
                    Application.Current.Properties.Add("FirstName", facebookEmail.First_Name);
                    Application.Current.Properties.Add("LastName", facebookEmail.Last_Name);
                    Application.Current.Properties.Add("DisplayName", facebookEmail.Name);
                    Application.Current.Properties.Add("EmailAddress", facebookEmail.Email);
                    Application.Current.Properties.Add("ProfilePicture", facebookEmail.Picture.Data.Url);

                    if (newuser)
                        await FirebaseHelper.AddPantryItem("Example Item", "5", "12/15");
                    App.Current.MainPage = new AppShell();
                }
              
               
            }
        }

        [Obsolete]
        void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }

            Debug.WriteLine("Authentication error: " + e.Message);
        }
    
    }
}
