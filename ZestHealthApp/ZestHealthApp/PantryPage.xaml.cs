using MagicGradients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZestHealthApp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Rg.Plugins.Popup.Services;
using ZestHealthApp.newViews;

namespace ZestHealthApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PantryPage : CarouselPage
    {
        public PantryPage()
        {
            InitializeComponent();
          
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.PushAsync(new PopupNewTaskView());
        }
    }
}