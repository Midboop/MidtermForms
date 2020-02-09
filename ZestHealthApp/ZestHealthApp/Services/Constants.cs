using System;
using System.Collections.Generic;
using System.Text;

namespace ZestHealthApp.Services
{
    public static class Constants
    {
		public static string AppName = "OAuthNativeFlow";

		// OAuth
		// For Google login, configure at https://console.developers.google.com/
		public static string iOSClientId = "n/a";
		public static string AndroidClientId = "1021990125436-989arujvm9mtridepfvcfdk108cgrcdh.apps.googleusercontent.com";

		// These values do not need changing
		public static string Scope = "https://www.googleapis.com/auth/userinfo.email https://www.googleapis.com/auth/userinfo.profile";
		//public static string Scope2 = "https://www.googleapis.com/auth/userinfo.profile";
		public static string AuthorizeUrl = "https://accounts.google.com/o/oauth2/auth";
		public static string AccessTokenUrl = "https://www.googleapis.com/oauth2/v4/token";
		public static string UserInfoUrl = "https://www.googleapis.com/oauth2/v2/userinfo";
		//public static string UserProfileUrl = "https://www.googleapis.com/oauth2/v2/userinfo.profile";

		// Set these to reversed iOS/Android client ids, with :/oauth2redirect appended
		public static string iOSRedirectUrl = "<insert IOS redirect URL here>:/oauth2redirect";
		public static string AndroidRedirectUrl = "com.googleusercontent.apps.1021990125436-989arujvm9mtridepfvcfdk108cgrcdh:/oauth2redirect";


		// FaceBook Stuff ------------------------------------------------------------------------------


		// Facebook OAuth
		// For Facebook login, configure at https://developers.facebook.com
		public static string FacebookiOSClientId = "<insert IOS client ID here>";
		public static string FacebookAndroidClientId = "211317750007938";

		// These values do not need changing
		public static string FacebookScope = "email";
		public static string FacebookAuthorizeUrl = "https://www.facebook.com/dialog/oauth/";
		public static string FacebookAccessTokenUrl = "https://www.facebook.com/connect/login_success.html";
		public static string FacebookUserInfoUrl = "https://graph.facebook.com/me?fields=email&access_token={accessToken}";

		// Set these to reversed iOS/Android client ids, with :/oauth2redirect appended
		public static string FacebookiOSRedirectUrl = "<insert IOS redirect URL here>:/oauth2redirect";
		public static string FacebookAndroidRedirectUrl = "https://www.facebook.com/connect/login_success.html";
	}
}
