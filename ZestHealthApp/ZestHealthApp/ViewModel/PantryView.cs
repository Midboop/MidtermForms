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

        //async await
        public ObservableCollection<PantryItems> PantryList { get; set; } = new ObservableCollection<PantryItems>
        {
         new PantryItems
         {
           ItemName = "Example Item",
           ExpirationDate = "12/15/2015",
           Quantity = 1.ToString()}

    };
    public PantryView()
        {
            GetPantryItems().ContinueWith(t =>
            {
                PantryList = new ObservableCollection<PantryItems>(t.Result);
            });
        }

        public async Task RefreshPantry()
        {
             await  GetPantryItems().ContinueWith(t =>
            {
                PantryList = new ObservableCollection<PantryItems>(t.Result);
            });
        }
        private async Task<List<PantryItems>> GetPantryItems()
        {
            return (await FirebaseHelper.GetPantry());
        }

        private string itemname;
        public string ItemName
        {
            get { return itemname; }
            set
            {
                itemname = value;
               // items = ItemName;
               // PropertyChanged(this, new PropertyChangedEventArgs("ItemName"));
            }
        }

        private string exp;
        public string ExpirationDate
        {
            get { return exp; }
            set
            {
                exp = value;
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
            if (string.IsNullOrEmpty(ItemName) || string.IsNullOrEmpty(Quantity) || string.IsNullOrEmpty(ExpirationDate))
                await App.Current.MainPage.DisplayAlert("Empty Values", "Please enter Item Name, Calories, and Quantity.", "OK");
            else
            {
                var user = await FirebaseHelper.AddPantryItem(itemname, quantity, exp);
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
