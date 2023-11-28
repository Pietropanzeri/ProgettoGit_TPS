using Client.Model;
using Client.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Client.Controller
{
    public partial class DettagliUtenteController : ObservableObject
    {

        [ObservableProperty]
        UtenteFoto utenteVisualizzato;

        [ObservableProperty]
        UriImageSource userImage;

        [ObservableProperty]
        string username;

        public ObservableCollection<RicettaFoto> RicetteSalvate { get; set; } = new ObservableCollection<RicettaFoto>();

        public DettagliUtenteController(UtenteFoto utente)
        {
            UtenteVisualizzato = utente;
            Username = utente.Username;
        }

        [RelayCommand]
        public async Task Appearing()
        {
            UserImage = new UriImageSource
            {
                Uri = new Uri(UtenteVisualizzato.URLFoto),
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
            RichiestaHttp();
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
                response = await _client.GetAsync($"/ricette/{UtenteVisualizzato.UtenteId}");

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
        }

        [RelayCommand]
        public async Task OpenDescription(RicettaFoto ricetta)
        {
            await App.Current.MainPage.Navigation.PushModalAsync(new DetailsPage(ricetta));
        }
    }
}
