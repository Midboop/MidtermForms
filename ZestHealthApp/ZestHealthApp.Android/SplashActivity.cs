﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Animation;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Com.Airbnb.Lottie;

namespace ZestHealthApp.Droid
{
    
    [Activity(Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : AppCompatActivity, Animator.IAnimatorListener
    {
        LottieAnimationView animationView;
        public void OnAnimationCancel(Animator animation)
        {
        }

        public void OnAnimationEnd(Animator animation)
        {
            StartActivity(new Intent(this, typeof(MainActivity)));
        }

        public void OnAnimationRepeat(Animator animation)
        {
        }

        public void OnAnimationStart(Animator animation)
        {
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.splash);
            animationView = FindViewById<LottieAnimationView>(Resource.Id.lottie_animationView);
            animationView.AddAnimatorListener(this);
            animationView.RepeatCount = 0;
            animationView.PlayAnimation();
        }

    }
}
