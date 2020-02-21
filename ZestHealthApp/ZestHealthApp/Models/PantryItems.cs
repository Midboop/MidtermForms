using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ZestHealthApp.Models
{
    public class PantryItems : BaseFodyObservable
    {
        public string ItemName { get; set; }
        public string Quantity { get; set; }
        public string ExpirationDate { get; set; }
    }
}
