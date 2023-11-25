using Client.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Controller
{
    public partial class AddPageController : ObservableObject
    {
        [ObservableProperty]
        string nomeRicetta;
        [ObservableProperty]
        string preparazione;
        [ObservableProperty]
        int tempo;
        [ObservableProperty]
        int difficoltà;
        [ObservableProperty]
        TipoPiatto tipoPiatto;

        [RelayCommand]
        public async Task SaveRecipe()
        {
            string baseUri = App.BaseRootHttps;
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            HttpClient client = new HttpClient(handler)
            {
                BaseAddress = new Uri(baseUri)
            };

            try
            {
                // Creare un oggetto con i dati della ricetta
                var nuovaRicetta = new Ricetta
                {
                    Nome = NomeRicetta,
                    Preparazione = Preparazione,
                    Tempo = Tempo,
                    Difficolta = Difficoltà,
                    Piatto = 2,
                    UtenteId = App.utente.UtenteId
                };

                // Serializzare l'oggetto in formato JSON
                string jsonRicetta = JsonConvert.SerializeObject(nuovaRicetta);

                // Creare il contenuto della richiesta HTTP
                StringContent content = new StringContent(jsonRicetta, Encoding.UTF8, "application/json");

                // Eseguire la richiesta HTTP POST
                HttpResponseMessage response = await client.PostAsync("/ricetta", content);

                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    // La ricetta è stata salvata con successo
                    await App.Current.MainPage.DisplayAlert("Successo", "Ricetta salvata con successo", "OK");
                }
                else
                {
                    // Qualcosa è andato storto durante la richiesta POST
                    await App.Current.MainPage.DisplayAlert("Errore", "Si è verificato un errore durante il salvataggio della ricetta", "OK");
                }
            }
            catch (Exception ex)
            {
                // Gestisci eventuali eccezioni
                Console.WriteLine($"Errore durante la richiesta POST: {ex.Message}");
            }
        }
    }
}
