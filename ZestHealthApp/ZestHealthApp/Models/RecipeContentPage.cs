using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ZestHealthApp.Models
{
   public class RecipeContentPage : ContentPage, IBindablePage
    {
        public RecipeContentPage()
        {
            Padding = 0;
            Shell.SetTabBarIsVisible(this, false);
        }
    }
}
