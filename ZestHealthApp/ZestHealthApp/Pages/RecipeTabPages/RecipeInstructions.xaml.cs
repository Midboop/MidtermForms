using Sharpnado.Presentation.Forms.CustomViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ZestHealthApp.Pages.RecipeTabPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeInstructions : ContentView, IAnimatableReveal // Tab 2
    {
        public RecipeInstructions()
        {
            InitializeComponent();
        }

        public bool Animate { get; set; }
    }
}