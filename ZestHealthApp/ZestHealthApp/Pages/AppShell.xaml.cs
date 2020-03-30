using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZestHealthApp.Pages;

namespace ZestHealthApp
{
    public partial class AppShell : Shell
    {
        public DataTemplate PagePreference;
        public AppShell()
        {
            BindingContext = this;
            bool istoggled;
            try
            {
              istoggled  = (bool)Application.Current.Properties["isToggled"];

            }
            catch(Exception e)
            {
                istoggled = false;
                
            }
           
            if (!istoggled)
                PagePreference = new DataTemplate(typeof(MainPage));
            else
                PagePreference = new DataTemplate(typeof(AltMainPage));

            InitializeComponent();
            Routing.RegisterRoute("PantryAddItem", typeof(AddtoPantryPage));
            Routing.RegisterRoute("ShoppingAddItem", typeof(AddtoListPage));
            Routing.RegisterRoute("AltMainPage", typeof(AltMainPage));

        }

        public void ToggleContext(bool isToggled)
        {
            if (isToggled)
            {
                ToggleTab.ContentTemplate = new DataTemplate(typeof(MainPage));
                Application.Current.Properties["isToggled"] = false;
            }
            else
            {
                ToggleTab.ContentTemplate = new DataTemplate(typeof(AltMainPage));
                Application.Current.Properties["isToggled"] = true;
            }
        }
    }
}