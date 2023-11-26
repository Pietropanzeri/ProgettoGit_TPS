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
        public ObservableCollection<TipoPiatto> tipiPiatti { get; set; } = new ObservableCollection<TipoPiatto>(Enum.GetValues(typeof(TipoPiatto)).Cast<TipoPiatto>());
        [ObservableProperty]
        int tipoPiattoSel;

        byte[] imageBytes;
        string base64String = null;

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
                var nuovaRicetta = new Ricetta
                {
                    Nome = NomeRicetta,
                    Preparazione = Preparazione,
                    Tempo = Tempo,
                    Difficolta = Difficoltà,
                    Piatto = TipoPiattoSel,
                    UtenteId = App.utente.UtenteId
                };

                string jsonRicetta = JsonConvert.SerializeObject(nuovaRicetta);

                StringContent content = new StringContent(jsonRicetta, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("/ricetta", content);

                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    int nuovaRicettaId = (await response.Content.ReadFromJsonAsync<Ricetta>()).RicettaId;

                    var nuovaFoto = new Foto
                    {
                        FotoData = base64String,
                        RicettaId = nuovaRicettaId,
                        FotoId = 0,
                        Descrizione = nuovaRicetta.Nome
                        
                    };

                    string jsonFoto = JsonConvert.SerializeObject(nuovaFoto);

                    StringContent contentFoto = new StringContent(jsonFoto, Encoding.UTF8, "application/json");
                    HttpResponseMessage responseFoto = await client.PostAsync("/foto", contentFoto);

                    if (responseFoto.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        await App.Current.MainPage.DisplayAlert("Successo", "Ricetta e foto salvate con successo", "OK");
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Errore", "Si è verificato un errore durante il salvataggio della foto", "OK");
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Errore", "Si è verificato un errore durante il salvataggio della ricetta", "OK");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore durante la richiesta POST: {ex.Message}");
            }
        }

        [RelayCommand]
        public async Task ImpostaImmagine()
        {
            var media = await MediaPicker.PickPhotoAsync();
            if (media != null)
            {
                using (var stream = await media.OpenReadAsync())
                {
                    imageBytes = new byte[stream.Length];
                    await stream.ReadAsync(imageBytes, 0, (int)stream.Length);
                    base64String = Convert.ToBase64String(imageBytes);
                }
            }
        }
        
    }
}
