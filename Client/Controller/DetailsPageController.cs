using Client.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Client.Controller
{
    public partial class DetailsPageController : ObservableObject
    {
        [ObservableProperty]
        RicettaFoto ricettaVisualizzata;

        int numero;

        public ObservableCollection<string> FotoImages { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<Ingrediente> IngredientiRicetta { get; set; } = new ObservableCollection<Ingrediente>();
        public DetailsPageController(RicettaFoto ricetta ) 
        {
            RicettaVisualizzata = ricetta;
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

            try
            {
                response = await _client.GetAsync($"/foto/ricetta/{RicettaVisualizzata.RicettaId}/numero");

                if (response.IsSuccessStatusCode)
                {
                    numero = await response.Content.ReadFromJsonAsync<int>();
                }
            }
            catch (Exception e)
            {
            }

            for (int i = 1; i <= numero; i++)
            {
                FotoImages.Add(App.BaseRootHttp + $"/foto/ricetta/{RicettaVisualizzata.RicettaId}/{i}");
            }
            List<Ingrediente> ingredientes = new List<Ingrediente>();
            try
            {
                response = await _client.GetAsync($"/ingrediente/ricetta/{RicettaVisualizzata.RicettaId}");

                if (response.IsSuccessStatusCode)
                {
                    ingredientes = await response.Content.ReadFromJsonAsync<List<Ingrediente>>();
                    foreach (var item in ingredientes)
                    {
                        IngredientiRicetta.Add(item);
                    }
                }
            }
            catch (Exception e)
            {
            }
        }
    }
}
