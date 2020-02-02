﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZestHealthApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ZestHealthApp.newViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class XF_SignUpPage : ContentPage
    {
        SignUpVM signUpVM;
        public XF_SignUpPage()
        {
            InitializeComponent();
            signUpVM = new SignUpVM();
            BindingContext = signUpVM;
        }
    }
}