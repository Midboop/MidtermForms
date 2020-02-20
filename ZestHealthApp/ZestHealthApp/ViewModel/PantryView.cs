using System;
using System.Collections.Generic;           // This Code Has been Cleaned and Tidy-ed up - Tk<3
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
        // This is the Binding Source List for PantryPage.xaml's CollectionView Template items
        public ObservableCollection<PantryItems> PantryList { get; set; } = new ObservableCollection<PantryItems> {};
        public string ItemName { get; set; }
        public string ExpirationDate { get; set; }
        public string Quantity { get; set; }

        // Constructor 
        public PantryView()
        {  
            GetPantryItems().ContinueWith(t => { PantryList = new ObservableCollection<PantryItems>(t.Result); });
        }

        public async Task RefreshPantry()
        {
             await  GetPantryItems().ContinueWith(t => { PantryList = new ObservableCollection<PantryItems>(t.Result);});
        }
        private async Task<List<PantryItems>> GetPantryItems()
        {
            return (await FirebaseHelper.GetPantry());
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
                var user = await FirebaseHelper.AddPantryItem(ItemName, Quantity, ExpirationDate);
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
