using Client.Model;
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
    public partial class CercaPageController : ObservableObject
    {
        [ObservableProperty]
        bool cercaRicetta = true;

        [ObservableProperty]
        bool cercaIngrediente = true;

        [ObservableProperty]
        string contenutoEntry;

        public ObservableCollection<Ingrediente> Listaingredienti { get; set; } = new ObservableCollection<Ingrediente>();
        public ObservableCollection<RicettaFoto> ListaRicette { get; set; } = new ObservableCollection<RicettaFoto>();

        [RelayCommand]
        public async Task Ricerca()
        {
            Listaingredienti.Clear();
            ListaRicette.Clear();
            if(CercaIngrediente)
            {
                string baseUri = App.BaseRootHttps;
                HttpClientHandler handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
                HttpClient _client = new HttpClient(handler)
                {
                    BaseAddress = new Uri(baseUri)
                };

                List<Ingrediente> content = new List<Ingrediente>();
                HttpResponseMessage response = new HttpResponseMessage();
                try
                {
                    response = await _client.GetAsync($"ingredienti/nome/{ContenutoEntry}");

                    if (response.IsSuccessStatusCode)
                    {
                        content = await response.Content.ReadFromJsonAsync<List<Ingrediente>>();
                    }

                }
                catch (Exception e)
                {
                }

                foreach (var item in content)
                {
                    Listaingredienti.Add(item);
                }
            }
            if (CercaRicetta)
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
                    response = await _client.GetAsync($"ricette/nome/{ContenutoEntry}");

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
                    ListaRicette.Add(elemento);
                }
            }
        }
    }
}
