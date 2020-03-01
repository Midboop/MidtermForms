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
using ZestHealthApp.Pages;

namespace ZestHealthApp.ViewModel
{
    public class FirebaseHelper : ContentPage
    {
        // Connects to the Firebase DataBase
        public static FirebaseClient firebase = new FirebaseClient("https://zesthealth-1f666.firebaseio.com/");

        // adds googleusers to firebase
        public static async Task<bool> AddUser(string email, string picture, string name, string id)
        {
            try
            {
                await firebase
                    .Child("GoogleUsers")
                    .PostAsync(new GoogleUsers() { Email = email, Picture = picture, Name = name, Id = id});
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }

        // add facebook users to firebase
        public static async Task<bool> AddFacebookUser(string id, string name, string firstname, string lastname, string email,Picture picture)
        {
            try
            {
                await firebase
                    .Child("FacebookUsers")
                    .PostAsync(new FacebookEmail() { Id = id, Name = name, First_Name = firstname, Last_Name = lastname, Email = email, Picture = picture });
                return true;
            }
            catch(Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }

        // Get's specific Google User
        public static async Task<GoogleUsers> GetUser(string email)
        {
            try
            {
                var allUsers = await GetAllUser();
                await firebase
                    .Child("GoogleUsers")
                    .OnceAsync<GoogleUsers>();
                return allUsers.Where(a => a.Email == email).FirstOrDefault();
            }

            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }

        // get specific Facebook user
        public static async Task<FacebookEmail> GetFacebookUser(string name)
        {
            try
            {
                var allUsers = await GetAllFacebookUsers();
                await firebase
                    .Child("FacebookUsers")
                    .OnceAsync<FacebookEmail>();
                return allUsers.Where(a => a.Name == name).FirstOrDefault();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }

        // Gets all of the GoogleUsers
        public static async Task<List<GoogleUsers>> GetAllUser()
        {

            try
            {
                var userlist = (await firebase
                .Child("GoogleUsers")
                .OnceAsync<GoogleUsers>()).Select(item =>
                new GoogleUsers
                {
                    Email = item.Object.Email,
                    Picture = item.Object.Picture,
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

        public static async Task<bool> CheckEmail(string email)
        {
            bool accountFound = false;
            await FirebaseHelper.GetAllUser().ContinueWith(t =>
            {
                List<GoogleUsers> userCheck = (t.Result);
                if (userCheck.Find(x => x.Email.Contains(email)) != null)
                    accountFound = true;
            });

            return accountFound;
        }

        public static async Task<bool> CheckFacebookEmail(string email)
        {
            bool accountFound = false;
            await FirebaseHelper.GetAllFacebookUsers().ContinueWith(t =>
            {
                List<FacebookEmail> userCheck = (t.Result);
                if (userCheck.Find(x => x.Email.Contains(email)) != null)
                    accountFound = true;
            });

            return accountFound;
        }

        public static async Task<List<FacebookEmail>> GetAllFacebookUsers()
        {
            try
            {
                var userlist = (await firebase
                    .Child("FacebookUsers")
                    .OnceAsync<FacebookEmail>()).Select(item =>
                    new FacebookEmail
                    {
                        Email = item.Object.Email,
                        Id = item.Object.Id,
                        Name = item.Object.Name,
                        First_Name = item.Object.First_Name,
                        Last_Name = item.Object.Last_Name,
                        Picture = item.Object.Picture
                    }).ToList();
                return userlist;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
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
                    .PostAsync(new RecipeItems { IngredientsList = list, RecipeName = name });
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
        public static async Task<List<RecipeItems>> GetRecipes()
        {
            try
            {
                var recipeList = (await firebase
                .Child(Application.Current.Properties["Id"].ToString()).Child("Recipes")
                .OnceAsync<RecipeItems>()).Select(item =>
                 new RecipeItems
                 {
                     RecipeName = item.Object.RecipeName,
                     IngredientsList = item.Object.IngredientsList
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

        // Updates the pantry item info
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



        // Deletes the Pantry Items from the database
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

        // Deletes Recipe
        public static async Task<bool> DeleteRecipe(string name)
        {
            try
            {
                var toDeleteItem = (await firebase
                     .Child(Application.Current.Properties["Id"].ToString()).Child("Recipes")
                    .OnceAsync<RecipeItems>()).Where(a => a.Object.RecipeName == name).FirstOrDefault();
                await firebase.Child(Application.Current.Properties["Id"].ToString()).Child("Recipes").Child(toDeleteItem.Key).DeleteAsync();
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
