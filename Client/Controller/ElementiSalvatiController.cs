using Client.Model;
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
    public partial class ElementiSalvatiController : ObservableObject
    {
        public ObservableCollection<RicettaFoto> ListaSalvati { get; set; } = new ObservableCollection<RicettaFoto>();

        [RelayCommand]
        public async Task Appearing()
        {
            ListaSalvati.Clear();
            await RichiestaHttp();
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

            List<Ricetta> content = new List<Ricetta>();

            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                response = await _client.GetAsync($"/ricettesalvate/{App.utente.UtenteId}");

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
                RicettaFoto elemento = new RicettaFoto(item, $"{App.BaseRootHttp}/foto/ricetta/{item.RicettaId}/primaimmagine");
                ListaSalvati.Add(elemento);
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
                if (response.IsSuccessStatusCode)
                {
                    ListaSalvati.Clear();
                    await RichiestaHttp();
                }
            }
            catch (Exception e)
            {
            }
        }
    }
}
