using Sharpnado.Presentation.Forms.RenderedViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ZestHealthApp.ViewCells
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeCardCell : MaterialFrame
    {
        public RecipeCardCell()
        {
            InitializeComponent();
        }
    }
}