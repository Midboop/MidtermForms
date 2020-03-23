using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZestHealthApp.ViewModel;

namespace ZestHealthApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestingPicturePage : ContentPage
    {
        MediaFile file;
        public TestingPicturePage()
        {
            InitializeComponent();
            //imgBanner.Source = ImageSource.FromResource("XamarinFirebase.images.banner.png");  
            imgChoosed.Source = ImageSource.FromResource("XamarinFirebase.images.default.jpg");  
        }

        private async void buttonPicker_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            try
            {
                file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                });
                if (file == null)
                    return;
                imgChoosed.Source = ImageSource.FromStream(() =>
                {
                    var imageStram = file.GetStream();
                    return imageStram;
                });
                
                //await StoreImages(file.GetStream());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async void buttonStore_Clicked(object sender, EventArgs e)
        {
            //await FirebaseHelper.RecipeImage(file.GetStream(),txtFileName.Text);
            //Path.GetFileName(file.Path)
        }

        private async void btnDownload_Clicked(object sender, EventArgs e)
        {
            var path = await FirebaseHelper.GetImage(txtFileName.Text);

            if(path != null)
            {
                imgChoosed.Source = path;
            }

        }
    }
}