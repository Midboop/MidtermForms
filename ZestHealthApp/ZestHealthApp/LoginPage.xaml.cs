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
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

       async private void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new RegistrationPage());
        }

       async private void Button_Clicked_1(object sender, EventArgs e)
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserDataBase.db");
            var db = new SQLiteConnection(dbPath);
            var myquery = db.Table<RegUserTable>().Where(u => u.UserName.Equals(EntryUser.Text) && u.Password.Equals(EntryPassword.Text)).FirstOrDefault();

            if(myquery!=null)
            {
                App.Current.MainPage = new AppShell();
            }
            else
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await this.DisplayAlert("Error", "Username or Password incorrect", "Yes", "Cancel");

                    if (result)
                    {
                        await Navigation.PushModalAsync(new LoginPage());
                    }
                    else
                    {
                        await Navigation.PushModalAsync(new LoginPage());
                    }
                });
            }
        }
    }
}