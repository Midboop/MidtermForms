using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZestHealthApp.Tables;

namespace ZestHealthApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserDataBase.db");
            var db = new SQLiteConnection(dbPath);
            db.CreateTable<RegUserTable>();

            var item = new RegUserTable()
            {
                UserName = EntryUserName.Text,
                Password = EntryPassword.Text,
                Email = EntryEmail.Text

            };

            db.Insert(item);
            Device.BeginInvokeOnMainThread(async () =>
            {
                //if (EntryUserName.Text == item.UserName)
                //{
                //    var result1 = await this.DisplayAlert("Sorry, username in use", "Please try again", "Continue", "Cancel");
                //    if (result1)
                //        await Navigation.PushModalAsync(new RegistrationPage());
                //}
                //else

                //{
                    var result = await this.DisplayAlert("Congratulations!", "User Regitration Successful", "Continue", "Cancel");
                    if (result)
                    {
                        await Navigation.PushModalAsync(new LoginPage());
                    }

                
              
            });
        }

        async private void Button_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new LoginPage());
        }
    }
}