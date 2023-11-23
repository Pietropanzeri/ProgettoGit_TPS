using System;
using System.Collections.Generic;
using Client.Model;
using Client.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Client.Controller
{
    public partial class HomePageController : ObservableObject
    {
        public ObservableCollection<RicettaFoto> ListaNovità { get; set; } = new ObservableCollection<RicettaFoto>();

        [ObservableProperty]
        string error;

        List<Ricetta> listRicette = new List<Ricetta>();
        List<Foto> listFoto = new List<Foto>();

        //TODO: spostare baseUrl in modo tale che sia visibile da tutti
        string endpoint = "/ricette/novita/{indicePartenza}/{countRicette}";
        int indicePartenza = 0; 
        int countRicette = 10; 
        string apiUrl = string.Empty;
        public HomePageController()
        {
            apiUrl = $"{endpoint}"
                .Replace("{indicePartenza}", indicePartenza.ToString())
                .Replace("{countRicette}", countRicette.ToString());
        }
        [RelayCommand]
        public async Task Appearing()
        {
            ListaNovità.Clear();
            await RichiestaHttp();
        }

        public async Task RichiestaHttp()
        {
            string baseUri = Preferences.Get("BaseRoot", "https://192.168.1.56:5001");
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            HttpClient _client = new HttpClient(handler)
            {
                BaseAddress = new Uri(baseUri)
            };

            List<Ricetta> content = new List<Ricetta>();

            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                response = await _client.GetAsync("/ricette/novita/0/11");

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
                RicettaFoto elemento = new RicettaFoto(item, $"{baseUri}/foto/ricetta/{item.RicettaId}/primaimmagine");
                ListaNovità.Add(elemento);
            }
            
        }
    }
}
