using System;
using System.Linq;
using System.Diagnostics;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Auth;
using Xamarin.Forms.Xaml;
using ZestHealthApp.Services;
using ZestHealthApp.Models;
using Firebase.Database;
using System.Threading.Tasks;
using Firebase.Database.Query;
using System.ComponentModel;
using System.Collections.Generic;

namespace ZestHealthApp.ViewModel
{
    public class GoogleVM : ContentPage
    {

		public static FirebaseClient firebase = new FirebaseClient("https://zesthealth-1f666.firebaseio.com/");
		
		Account account;
		AccountStore store;
		GoogleSignUpHelperVM google;
		public GoogleVM()
		{
			

			store = AccountStore.Create();
			google = new GoogleSignUpHelperVM();
			BindingContext = google;
		}

		public void GoogleLogin()
		{
			string clientId = null;
			string redirectUri = null;

			switch (Device.RuntimePlatform)
			{
				case Device.iOS:
					clientId = Constants.iOSClientId;
					redirectUri = Constants.iOSRedirectUrl;
					break;

				case Device.Android:
					clientId = Constants.AndroidClientId;
					redirectUri = Constants.AndroidRedirectUrl;
					break;
			}


			account = store.FindAccountsForService(Constants.AppName).FirstOrDefault();

			var authenticator = new OAuth2Authenticator(
				clientId,
				null,
				Constants.Scope,
				new Uri(Constants.AuthorizeUrl),
				new Uri(redirectUri),
				new Uri(Constants.AccessTokenUrl),
				null,
				true);

			authenticator.Completed += OnAuthCompleted;
			authenticator.Error += OnAuthError;

			AuthenticationState.Authenticator = authenticator;

			var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
			presenter.Login(authenticator);
		}

		async void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
		{
			var authenticator = sender as OAuth2Authenticator;
			if (authenticator != null)
			{
				authenticator.Completed -= OnAuthCompleted;
				authenticator.Error -= OnAuthError;
			}
	
			GoogleUsers user = null;
			if (e.IsAuthenticated)
			{
				// If the user is authenticated, request their basic user data from Google
				// UserInfoUrl = https://www.googleapis.com/oauth2/v2/userinfo
				var request = new OAuth2Request("GET", new Uri(Constants.UserInfoUrl), null, e.Account);
				
				var response = await request.GetResponseAsync();
				
				if (response != null)
				{
					// Deserialize the data and store it in the account store
					// The users email address will be used to identify data in SimpleDB
					string userJson = await response.GetResponseTextAsync();
					user = JsonConvert.DeserializeObject<GoogleUsers>(userJson);
				}


				if (account != null)
				{
					store.Delete(account, Constants.AppName);
				}

			



				await store.SaveAsync(account = e.Account, Constants.AppName);


				bool newuser = false;
				if (await FirebaseHelper.CheckEmail(user.Email) == false)
				{
					await FirebaseHelper.AddUser(user.Email, user.Picture, user.Name, user.Id);
					newuser = true;
				}


				Application.Current.Properties.Remove("Id");
				Application.Current.Properties.Remove("FirstName");
				Application.Current.Properties.Remove("LastName");
				Application.Current.Properties.Remove("DisplayName");
				Application.Current.Properties.Remove("EmailAddress");
				Application.Current.Properties.Remove("ProfilePicture");
			
					

				Application.Current.Properties.Add("Id", user.Id);
				Application.Current.Properties.Add("FirstName", user.GivenName);
				Application.Current.Properties.Add("LastName", user.FamilyName);
				Application.Current.Properties.Add("DisplayName", user.Name);
				Application.Current.Properties.Add("EmailAddress", user.Email);
				Application.Current.Properties.Add("ProfilePicture", user.Picture);

				
				if (newuser)
					await FirebaseHelper.AddPantryItem("Example Item", "5", "12/15");





				App.Current.MainPage = new AppShell();


			}
		}

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

