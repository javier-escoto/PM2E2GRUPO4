using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Examen_2pPII
{
    
    public partial class MainPage : ContentPage
    {
        byte[] Image;

        private MediaFile _photo;
        public MainPage()
        {
            InitializeComponent();
        }

        private async void TomePhoto_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            _photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                SaveToAlbum = true,
                Directory = "Sample",
                Name = "test.jpg",
                CompressionQuality = 50,
                PhotoSize = PhotoSize.Medium
            });

            if (_photo == null)
                return;

            var bytes = File.ReadAllBytes(_photo.Path);
            var base64 = Convert.ToBase64String(bytes);

        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            bool response = await Application.Current.MainPage.DisplayAlert("Advertencia", "Seleccione el tipo de imagen que desea", "Camara", "Galeria");

            if (response)
                Camera();
            else
                Gallery();

        }

        private async void Gallery()
        {
            try
            {
                if (CrossMedia.Current.IsPickPhotoSupported)
                {
                    var FileFoto = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                    {
                        PhotoSize = PhotoSize.Medium,
                    });
                    if (FileFoto == null)
                        return;

                    Photo.Source = ImageSource.FromStream(() => { return FileFoto.GetStream(); });
                    Image = File.ReadAllBytes(FileFoto.Path);
                }
                else
                {
                    await DisplayAlert("Error", "se produjo un error al cargar archivo", "Aceptar");
                }
            }
            catch (Exception)
            {
                await DisplayAlert("Error", "se produjo un error al cargar archivo", "Aceptar");
            }

        }


        private async void Camera()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            _photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                SaveToAlbum = true,
                Directory = "Sample",
                Name = "test.jpg",
                CompressionQuality = 50,
                PhotoSize = PhotoSize.Medium
            });

            if (_photo == null)
                return;

            var bytes = File.ReadAllBytes(_photo.Path);
            var base64 = Convert.ToBase64String(bytes);


    }


    }
}
 