using Sharpnado.Presentation.Forms.CustomViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xam.Forms.Markdown;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ZestHealthApp.Pages.RecipeTabPages 
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeDetails : ContentView, IAnimatableReveal // Tab 1
    {
        public RecipeDetails()
        {
            InitializeComponent();
            SetMarkdownTheme();
        }

        private void SetMarkdownTheme()
        {
            MarkdownView.Theme = new BodyTheme();
        }
        public bool Animate { get; set; }

        public class BodyTheme : MarkdownTheme
        {
            public BodyTheme()
            {
                Heading4.FontFamily = "OpenSans-SemiBold";
                Heading4.ForegroundColor = (Color)Application.Current.Resources["TextPrimaryColor"];

                Heading5.FontFamily = Application.Current.Resources["FontRegular"] as string;
                Heading5.ForegroundColor = (Color)Application.Current.Resources["TextPrimaryColor"];

                Paragraph.FontFamily = Application.Current.Resources["FontSemiBold"] as string;
                Paragraph.ForegroundColor = (Color)Application.Current.Resources["TextPrimaryColor"];
                Paragraph.FontSize = 16;
            }
        }

    }
}
