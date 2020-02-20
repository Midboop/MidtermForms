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
			Users users = null;
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
				if (await CheckEmail(user.Email) == false)
				{
					await AddUser(user.Email, user.Picture, user.Name, user.Id, user.PantryList);
					newuser = true;
				}


				Application.Current.Properties.Remove("Id");
				Application.Current.Properties.Remove("FirstName");
				Application.Current.Properties.Remove("LastName");
				Application.Current.Properties.Remove("DisplayName");
				Application.Current.Properties.Remove("EmailAddress");
				Application.Current.Properties.Remove("ProfilePicture");
			//	Application.Current.Properties.Remove("IsLoggedIn");
					

				Application.Current.Properties.Add("Id", user.Id);
				Application.Current.Properties.Add("FirstName", user.GivenName);
				Application.Current.Properties.Add("LastName", user.FamilyName);
				Application.Current.Properties.Add("DisplayName", user.Name);
				Application.Current.Properties.Add("EmailAddress", user.Email);
				Application.Current.Properties.Add("ProfilePicture", user.Picture);
				//Application.Current.Properties.Add("IsLoggedIn", user.isLoggedIn);
				//await GetUser(users.Email);
				
				if (newuser)
					await FirebaseHelper.AddPantryItem("Example Item", "5", "12/15");




				//await Navigation.PushModalAsync(new AppShell());
				App.Current.MainPage = new AppShell();
				//await DisplayAlert("Name ", user.GivenName, "OK");
				Application.Current.Properties["IsLoggedIn"] = Boolean.TrueString;
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

		public static async Task<bool> AddUser(string email, string picture, string name, string id, List<Object> list)
		{
			try
			{
				await firebase
					.Child("GoogleUsers")
					.PostAsync(new GoogleUsers() { Email = email, Picture = picture, Name = name, Id = id, PantryList = list });	
					return true;
			}
			catch (Exception e)
			{
				Debug.WriteLine($"Error:{e}");
				return false;
			}
		}
		public static async Task<bool> CheckEmail(string email)
		{
			bool accountFound = false;
			await GetAllUser().ContinueWith(t =>
			{
				List<GoogleUsers> userCheck = (t.Result);
				if (userCheck.Find(x => x.Email.Contains(email)) != null)
					accountFound = true;
			});

			return accountFound;
		}
		public static async Task<GoogleUsers> GetUser(string email)
		{
			try
			{
				var allUsers = await GetAllUser();
				await firebase
					.Child("GoogleUsers")
					.OnceAsync<GoogleUsers>();
				return allUsers.Where(a => a.Email == email).FirstOrDefault();
			}

			catch (Exception e)
			{
				Debug.WriteLine($"Error:{e}");
				return null;
			}
		}

		public static async Task<List<GoogleUsers>> GetAllUser()
		{

			try
			{
				var userlist = (await firebase
				.Child("GoogleUsers")
				.OnceAsync<GoogleUsers>()).Select(item =>
				new GoogleUsers
				{
					Email = item.Object.Email,
					Picture = item.Object.Picture,
					Name = item.Object.Name
				}).ToList();
				return userlist;
			}
			catch (Exception e)
			{
				Debug.WriteLine($"Error:{e}");
				return null;
			}
		}








	}
}

