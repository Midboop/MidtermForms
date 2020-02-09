using System;
using System.Linq;
using System.Diagnostics;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Auth;
using Xamarin.Forms.Xaml;
using ZestHealthApp.Services;
using ZestHealthApp.Models;

namespace ZestHealthApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GooglePage : ContentPage
    {
		Account account;
		AccountStore store;
        public GooglePage()
        {
            InitializeComponent();

            store = AccountStore.Create();
            
        }

        private void Button_Clicked(object sender, EventArgs e)
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
			//var request2 = new OAuth2Request("GET", new Uri(Constants.UserProfileUrl), null, e.Account);
			var response = await request.GetResponseAsync();
				//var response2 = await request2.GetResponseAsync();
			if (response != null)
			{
				// Deserialize the data and store it in the account store
				// The users email address will be used to identify data in SimpleDB
				string userJson = await response.GetResponseTextAsync();
				user = JsonConvert.DeserializeObject<GoogleUsers>(userJson);
			}
				//if (response2 != null)
				//{
				//	// Deserialize the data and store it in the account store
				//	// The users email address will be used to identify data in SimpleDB
				//	string userJson2 = await response2.GetResponseTextAsync();
				//	user = JsonConvert.DeserializeObject<GoogleUsers>(userJson2);
				//}

			if (account != null)
			{
				store.Delete(account, Constants.AppName);
			}
				//await Navigation.PushModalAsync(new AppShell());
				

				Application.Current.Properties.Remove("Id");
				Application.Current.Properties.Remove("given_name");
				Application.Current.Properties.Remove("LastName");

				await store.SaveAsync(account = e.Account, Constants.AppName);
				//Application.Current.Properties.Add("Id", user.Id);
				//Application.Current.Properties.Add("name", user.Name);
				//Application.Current.Properties.Add("LastName", user.FamilyName);
				
				//await Navigation.PushModalAsync(new AppShell());
				App.Current.MainPage = new AppShell();
				await DisplayAlert( "Name ", user.GivenName, "OK");
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