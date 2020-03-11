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
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("PantryAddItem", typeof(AddtoPantryPage));
            Routing.RegisterRoute("ShoppingAddItem", typeof(AddtoListPage));
        }
    }
}