using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZestHealthApp.Models;
using ZestHealthApp.ViewModel;

namespace ZestHealthApp.Pages.RecipeTabPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeTabbedViewPage : RecipeContentPage, IBindablePage
    {
        public RecipeTabbedViewPage(SingleRecipeData objectInstance)
        {
            Shell.SetTabBarIsVisible(this, false);
            InitializeComponent();
            BindingContext = objectInstance;
           
        }

      
    }
}