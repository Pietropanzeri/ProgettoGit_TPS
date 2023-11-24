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
using Newtonsoft.Json;
using System.Text;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;

namespace Client.Controller
{
    public partial class HomePageController : ObservableObject
    {
        public ObservableCollection<RicettaFoto> ListaNovità { get; set; } = new ObservableCollection<RicettaFoto>();
        public ObservableCollection<RicettaFoto> RicettaDelGiorno { get; set; } = new ObservableCollection<RicettaFoto>();

        List<Ricetta> listRicette = new List<Ricetta>();
        List<Foto> listFoto = new List<Foto>();

        string endpoint = "/ricette/novita/{indicePartenza}/{countRicette}";
        int indicePartenza = 0; 
        int countRicette = 10; 
        string apiUrl = string.Empty;

        [RelayCommand]
        public async Task Appearing()
        {
            endpoint = "/ricette/novita/{indicePartenza}/{countRicette}";
            apiUrl = $"{endpoint}"
                .Replace("{indicePartenza}", indicePartenza.ToString())
                .Replace("{countRicette}", countRicette.ToString());
            ListaNovità.Clear();
            RicettaDelGiorno.Clear();
            await RichiestaHttp();
        }
        //FARE METODO CHE ALLO SCORRIMENTO DELLA LISTA AUMENTA L'INDICE DI PARTENZA
        public async Task RichiestaHttp()
        {
            string baseUri = App.BaseRootHttps;
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            HttpClient _client = new HttpClient(handler)
            {
                BaseAddress = new Uri(baseUri)
            };

            List<Ricetta> content = new List<Ricetta>();
            Ricetta contentGiorno = new Ricetta();

            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                //TODO : fare che quando scorre alla fine vede le ricette dopo
                response = await _client.GetAsync("/ricette/novita/0/11");

                if (response.IsSuccessStatusCode)
                {
                    content = await response.Content.ReadFromJsonAsync<List<Ricetta>>();
                }

                response = await _client.GetAsync("/ricette/ricettadelgiorno");
                if (response.IsSuccessStatusCode)
                {
                    contentGiorno = await response.Content.ReadFromJsonAsync<Ricetta>();
                }
            }
            catch (Exception e)
            {
            }
            RicettaDelGiorno.Add(new RicettaFoto(contentGiorno, $"{App.BaseRootHttp}/foto/ricetta/{contentGiorno.RicettaId}/primaimmagine", $"{App.BaseRootHttp}/fotoUtente/{contentGiorno.UtenteId}"));

            foreach (var item in content)
            {
                RicettaFoto elemento = new RicettaFoto(item, $"{App.BaseRootHttp}/foto/ricetta/{item.RicettaId}/primaimmagine", $"{App.BaseRootHttp}/fotoUtente/{item.UtenteId}");
                ListaNovità.Add(elemento);
            }
        }

        [RelayCommand]
        public async Task Save(int ricettaId)
        {
            string baseUri = App.BaseRootHttps;
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            HttpClient _client = new HttpClient(handler)
            {
                BaseAddress = new Uri(baseUri)
            };



            string jsonUtente = JsonConvert.SerializeObject(App.utente);
            StringContent content = new StringContent(jsonUtente, Encoding.UTF8, "application/json");

            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                response = await _client.PostAsync($"/salvaricetta/{ricettaId}", content);
            }
            catch (Exception e)
            {
            }
        }
    }
}
