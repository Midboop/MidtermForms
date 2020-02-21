using System;
using System.Collections.Generic;
using System.Text;

namespace ZestHealthApp.Models
{
    public class ShoppingListItems : BaseFodyObservable
    {
        public string ItemName { get; set; }
        public string Amount { get; set; }
    }
}
