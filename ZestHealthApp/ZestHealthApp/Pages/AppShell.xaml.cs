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
            Routing.RegisterRoute("RecipeAddItem", typeof(AddRecipeItems));
            Routing.RegisterRoute("PantryPage", typeof(PantryPage));
            Routing.RegisterRoute("ShoppingPage", typeof(ShoppingListPage));
            Routing.RegisterRoute("PantryEditItem", typeof(EditPantryPage));
        }
    }
}