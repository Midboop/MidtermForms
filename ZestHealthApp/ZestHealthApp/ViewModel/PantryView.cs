using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;
using System.Threading.Tasks;
using ZestHealthApp.Models;
using System.Collections.ObjectModel;

namespace ZestHealthApp.ViewModel
{
   public class PantryView : BaseFodyObservable
    {

        //public event PropertyChangedEventHandler PropertyChanged;

        PantryItems items; 
        public ObservableCollection<PantryItems> PantryList { get; set; } = new ObservableCollection<PantryItems>
        {
         new PantryItems
         { 
           ItemName = "Example Item",
           Calories = 130.ToString(),
           Quantity = 1.ToString()}
        };

        private string itemname;
        public string ItemName
        {
            get { return itemname; }
            set
            {
                itemname = value;
                items = ItemName;
               // PropertyChanged(this, new PropertyChangedEventArgs("ItemName"));
            }
        }

        private string cals;
        public string Calories
        {
            get { return cals; }
            set
            {
                cals = value;
                //PropertyChanged(this, new PropertyChangedEventArgs("Calories"));
            }
        }

        private string quantity;
        public string Quantity
        {
            get { return quantity; }
            set
            {
                quantity = value;
               //PropertyChanged(this, new PropertyChangedEventArgs("Quantity"));
            }
        }

        public Command AddPantryCommand
        {
            get
            {
                return new Command(() =>
                {
                    AddPantry();
                });
            }
        }

        private async void AddPantry()
        {
            GoogleUsers users;
            users = new GoogleUsers();
            if (string.IsNullOrEmpty(ItemName) || string.IsNullOrEmpty(Quantity) || string.IsNullOrEmpty(Calories))
                await App.Current.MainPage.DisplayAlert("Empty Values", "Please enter Item Name, Calories, and Quantity.", "OK");
            else
            {
                var user = await FirebaseHelper.AddPantryItem(itemname, quantity, cals);
                if(user)
                {
                    await App.Current.MainPage.DisplayAlert("Item Added!", "", "OK");
                }
                else
                    await App.Current.MainPage.DisplayAlert("Couldn't Add Item", "Please Try Again", "OK");
            }
        }
    }
}
