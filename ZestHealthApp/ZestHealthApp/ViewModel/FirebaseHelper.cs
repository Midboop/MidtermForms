using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZestHealthApp.Models;
using ZestHealthApp.ViewModel;

namespace ZestHealthApp.ViewModel
{
    public class FirebaseHelper : ContentPage
    {
        // Connects to the Firebase DataBase
        public static FirebaseClient firebase = new FirebaseClient("https://zesthealth-1f666.firebaseio.com/");
        

        // Read All
        public static async Task<List<Users>> GetAllUser()
        {
            
            try
            {
                var userlist = (await firebase
                .Child("Users")
                .OnceAsync<Users>()).Select(item =>
                new Users
                {
                    Email = item.Object.Email,
                    Password = item.Object.Password,
                    Name = item.Object.Name
                }).ToList();
                return userlist;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }

        //Read email
        public static async Task<Users> GetUser(string email)
        {
            try
            {
                var allUsers = await GetAllUser();
                await firebase
                    .Child("Users")
                    .OnceAsync<Users>();
                return allUsers.Where(a => a.Email == email).FirstOrDefault();
            }

            catch(Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }

 
        // Read name
        public static async Task<Users> GetName(string name)
        {
            try
            {
                var allUsers = await GetAllUser();
                await firebase
                    .Child("Users")
                    .OnceAsync<Users>();
                return allUsers.Where(a => a.Name == name).FirstOrDefault();
                // This is me trying to make it display the name, but this way seems to only display the first name from the Users list on the database
                //return allUsers.Where(a => a.Name == a.Name).FirstOrDefault();

               
                
            }

            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }


        //Add user
        public static async Task<bool> AddUser(string email, string password, string name)
        {
            try
            {
                await firebase
                    .Child("Users")
                    .PostAsync(new Users() { Email = email, Password = password, Name = name });
                return true;
            }
            catch(Exception e)
            { 
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }

        // Add pantry items to the database
        public static async Task<bool> AddPantryItem(string name, string amount, string exp )
        {
            try
            {
                await firebase
                    .Child(Application.Current.Properties["Id"].ToString()).Child("PantryItems")
                    .PostAsync(new PantryItems { ItemName = name, ExpirationDate = exp, Quantity = amount
            });
                
                return true;
            }
            catch(Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }

        // Add recipe to database
        public static async Task<bool> AddRecipe(List<string> list, string name)
        {
            try
            {
                await firebase
                    .Child(Application.Current.Properties["Id"].ToString()).Child("Recipes")
                    .PostAsync(new Ingredients { ingredients = list, Name = name });
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }
   

        // Add Shopping items to the Firebase
        public static async Task<bool> AddShoppingList(string name, string amount)
        {
            try
            {
                await firebase
                    .Child(Application.Current.Properties["Id"].ToString()).Child("Shopping List")
                    .PostAsync(new ShoppingListItems { ItemName = name, Amount = amount });
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }

        // Read all pantry items
        public static async Task<List<PantryItems>> GetPantry()
        {
            try
            {
                var userlist = (await firebase
                .Child(Application.Current.Properties["Id"].ToString()).Child("PantryItems")
                .OnceAsync<PantryItems>()).Select(item =>
                new PantryItems
                {
                    ItemName = item.Object.ItemName,
                    ExpirationDate = item.Object.ExpirationDate,
                    Quantity = item.Object.Quantity
                }).ToList();
                return userlist;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }

        // Read all Recipes
        public static async Task<List<Ingredients>> GetRecipes()
        {
            try
            {
                var recipeList = (await firebase
                .Child(Application.Current.Properties["Id"].ToString()).Child("Recipes")
                .OnceAsync<Ingredients>()).Select(item =>
                 new Ingredients
                 {
                     ingredients = item.Object.ingredients,
                     Name = item.Object.Name
                 }).ToList();
                return recipeList;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }

        }

        // Read all Shopping List
        public static async Task<List<ShoppingListItems>> GetShoppingList()
        {
            try
            {
                var userlist = (await firebase
                 .Child(Application.Current.Properties["Id"].ToString()).Child("Shopping List")
                 .OnceAsync<ShoppingListItems>()).Select(item =>
                 new ShoppingListItems
                 {
                     ItemName = item.Object.ItemName,
                     Amount = item.Object.Amount
                 }).ToList();
                return userlist;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }

        // Update user info
        public static async Task<bool> UpdateUser(string email, string password, string name)
        {
            try
            {
                var toUpdateUser = (await firebase
                    .Child("Users")
                    .OnceAsync<Users>()).Where(a => a.Object.Email == email).FirstOrDefault();
                await firebase
                    .Child("Users")
                    .Child(toUpdateUser.Key)
                    .PutAsync(new Users() { Email = email, Password = password, Name = name });
                return true;
            }
            catch(Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }

        public static async Task<bool> UpdateQuantity(string quantity, string name, string exp)
        {
            try
            {
                var toUpdateQuantity = (await firebase.
                    Child("PantryItems")
                    .OnceAsync<PantryItems>()).Where(a => a.Object.ItemName == name).FirstOrDefault();
                await firebase
                    .Child("PantryItems")
                    .Child(toUpdateQuantity.Key)
                    .PutAsync(new PantryItems() { ItemName = name, Quantity = quantity, ExpirationDate = exp});
                return true;
            }
            catch(Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }


        // Delete user ---- Maybe use at a later time
        public static async Task<bool> DeleteUser(string email)
        {
            try
            {
                var toDeletePerson = (await firebase
                    .Child("Users")
                    .OnceAsync<Users>()).Where(a => a.Object.Email == email).FirstOrDefault();
                await firebase.Child("Users").Child(toDeletePerson.Key).DeleteAsync();
                return true;
                    
            }

            catch(Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }

        public static async Task<bool> DeletePantryItem(string name)
        {
            try
            {
                var toDeleteItem = (await firebase
                     .Child(Application.Current.Properties["Id"].ToString()).Child("PantryItems")
                    .OnceAsync<PantryItems>()).Where(a => a.Object.ItemName == name).FirstOrDefault();
                await firebase.Child(Application.Current.Properties["Id"].ToString()).Child("PantryItems").Child(toDeleteItem.Key).DeleteAsync();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }

        // Delete shopping list item
        public static async Task<bool> DeleteShoppingList(string name)
        {
            try
            {
                var toDeleteItem = (await firebase
                    .Child(Application.Current.Properties["Id"].ToString()).Child("Shopping List")
                    .OnceAsync<ShoppingListItems>()).Where(a => a.Object.ItemName == name).FirstOrDefault();
                await firebase.Child(Application.Current.Properties["Id"].ToString()).Child("Shopping List").Child(toDeleteItem.Key).DeleteAsync();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }



    }

 
    
}
