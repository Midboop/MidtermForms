using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
//using Lottie.Forms.Droid;
//using Xamarin.Facebook;
using Android.Content;
//using ZestHealthApp.Droid.Services;

namespace ZestHealthApp.Droid
{
    [Activity(Label = "ZestHealthApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        //public static ICallbackManager CallbackManager;

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            // Create callback manager using CallbackManagerFactory
           // CallbackManager = CallbackManagerFactory.Create();
            base.OnCreate(bundle);
           // global::Xamarin.Forms.Forms.Init(this, bundle);
           // AnimationViewRenderer.Init();
            global::Xamarin.Forms.Forms.Init(this, bundle);
            global::Xamarin.Auth.Presenters.XamarinAndroid.AuthenticationConfiguration.Init(this, bundle);
            LoadApplication(new App());
        }
        //protected override void OnActivityResult(
        //    int requestCode,
        //    Result resultCode,
        //    Intent data)
        //{
        //    base.OnActivityResult(requestCode, resultCode, data);
        //   // CallbackManager.OnActivityResult(requestCode, Convert.ToInt32(resultCode),
        //        //data);
        //}
        //protected override void OnCreate(Bundle savedInstanceState)
        //{
        //    TabLayoutResource = Resource.Layout.Tabbar;
        //    ToolbarResource = Resource.Layout.Toolbar;
        //    Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
        //    base.OnCreate(savedInstanceState);
        //    Xamarin.Essentials.Platform.Init(this, savedInstanceState);
        //    global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
        //    AnimationViewRenderer.Init();
        //    LoadApplication(new App());
        //}
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}