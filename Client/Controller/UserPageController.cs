using Client.Model;
using Client.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        [ObservableProperty]
        string username = App.utente.Username;

        public ObservableCollection<RicettaFoto> RicetteSalvate { get; set; } = new ObservableCollection<RicettaFoto>();
        public ObservableCollection<UtenteFoto> UtentiSeguiti { get; set; } = new ObservableCollection<UtenteFoto>();

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
            RicetteSalvate.Clear();
            UtentiSeguiti.Clear();
            RichiestaHttp();
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

        public async Task RichiestaHttp()
        {

            string baseUri = App.BaseRootHttps;
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            HttpClient _client = new HttpClient(handler)
            {
                BaseAddress = new Uri(baseUri)
            };

            HttpResponseMessage response = new HttpResponseMessage();

            List<Ricetta> content = new List<Ricetta>();
            try
            {
                response = await _client.GetAsync($"/ricette/{App.utente.UtenteId}");

                if (response.IsSuccessStatusCode)
                {
                    content = await response.Content.ReadFromJsonAsync<List<Ricetta>>();
                }
            }
            catch (Exception e)
            {
            }

            foreach (var item in content)
            {
                RicettaFoto elemento = new RicettaFoto(item, $"{App.BaseRootHttp}/foto/ricetta/{item.RicettaId}/primaimmagine", $"{App.BaseRootHttp}/fotoUtente/{item.UtenteId}");
                RicetteSalvate.Add(elemento);
            }



            //// Utentei seguiti
             List<Utente> utentes = new List<Utente>();

            try
            {
                response = await _client.GetAsync($"/utente/utentiseguiti/{App.utente.UtenteId}");

                if (response.IsSuccessStatusCode)
                {
                    utentes = await response.Content.ReadFromJsonAsync<List<Utente>>();
                    foreach (var item in utentes)
                    {
                        UtentiSeguiti.Add(new UtenteFoto(item, $"{App.BaseRootHttp}/fotoUtente/{item.UtenteId}"));
                    }
                }
            }
            catch (Exception e)
            {
            }
        }

        [RelayCommand]
        public async Task OpenDescription(RicettaFoto ricetta)
        {
            await App.Current.MainPage.Navigation.PushModalAsync(new DetailsPage(ricetta));
        }

        [RelayCommand]
        public async Task OpenDescriptionUser(UtenteFoto utente)
        {
            await App.Current.MainPage.Navigation.PushModalAsync(new DetagliUtente(utente));
        }
    }
}
