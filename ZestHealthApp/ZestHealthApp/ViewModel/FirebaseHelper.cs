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

namespace ZestHealthApp.ViewModel
{
    public class FirebaseHelper : ContentPage
    {
        // Connects to the Firebase DataBase
        public static FirebaseClient firebase = new FirebaseClient("https://zest-app-d7dee.firebaseio.com/");
        

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
    }

 
    
}
