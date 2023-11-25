using Client.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Client.Controller
{
    public partial class UserPageController : ObservableObject
    {
        [ObservableProperty]
        UriImageSource userImage;

        public UserPageController() 
        {
            UserImage = new UriImageSource
            {
                Uri = new Uri($"{App.BaseRootHttp}/fotoUtente/{App.utente.UtenteId}"),
                CachingEnabled = false
            };
        }

        [RelayCommand]
        public async Task Appearing()
        {
            UserImage = new UriImageSource
            {
                Uri = new Uri($"{App.BaseRootHttp}/fotoUtente/{App.utente.UtenteId}"),
                CachingEnabled = false
            };

            string folderCache = FileSystem.Current.CacheDirectory;
            if (Directory.Exists(folderCache))
            {
                string[] files = Directory.GetFiles(folderCache);

                foreach (string file in files)
                {
                    File.Delete(file);
                }
            }
        }
        [RelayCommand]
        public async Task ChangePassword()
        {
            string baseUri = App.BaseRootHttps;
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            HttpClient _client = new HttpClient(handler)
            {
                BaseAddress = new Uri(baseUri)
            };

            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                string oldPassword = await App.Current.MainPage.DisplayPromptAsync("Old Password", "inserisci la vecchia password");
                string newPassword = await App.Current.MainPage.DisplayPromptAsync("New Password", "inserisci la nuova password");
                Utente utente = App.utente;
                utente.Password = newPassword;
                string jsonUtente = JsonConvert.SerializeObject(utente);
                StringContent content = new StringContent(jsonUtente, Encoding.UTF8, "application/json");
                response = await _client.PostAsync($"/changepassword/{oldPassword}", content);

                if (response.IsSuccessStatusCode)
                {
                    await App.Current.MainPage.DisplayAlert("password cambiata", "", "OK");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Errore", "", "OK");
                }

            }
            catch (Exception e)
            {
            }
        }

        [RelayCommand]
        public async Task ChangeImage()
        {
            string baseUri = App.BaseRootHttps;
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            HttpClient _client = new HttpClient(handler)
            {
                BaseAddress = new Uri(baseUri)
            };

            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                var media = await MediaPicker.PickPhotoAsync();
                byte[] imageBytes = null;
                string base64String = null;
                if (media != null)
                {
                    using (var stream = await media.OpenReadAsync())
                    {
                        imageBytes = new byte[stream.Length];
                        await stream.ReadAsync(imageBytes, 0, (int)stream.Length);
                        base64String = Convert.ToBase64String(imageBytes);
                    }
                }
                string jsonUtente = JsonConvert.SerializeObject(new FotoUtente() { FotoData = base64String, UtenteId = App.utente.UtenteId, FotoId = 0});
                StringContent content = new StringContent(jsonUtente, Encoding.UTF8, "application/json");

                response = await _client.PutAsync($"/changeimage", content);
                //cancellare cache oer vedere immagine
                string folderCache = FileSystem.Current.CacheDirectory;
                if (Directory.Exists(folderCache))
                {                  
                    string[] files = Directory.GetFiles(folderCache);

                    foreach (string file in files)
                    {
                        File.Delete(file);
                    }
                }
                if (response.IsSuccessStatusCode)
                {
                    await App.Current.MainPage.DisplayAlert("immagine cambiata", "", "OK");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Errore", "", "OK");
                }

            }
            catch (Exception e)
            {
            }
            UserImage = new UriImageSource
            {
                Uri = new Uri($"{App.BaseRootHttp}/fotoUtente/{App.utente.UtenteId}"),
                CachingEnabled = false
            };
        }
    }
}
