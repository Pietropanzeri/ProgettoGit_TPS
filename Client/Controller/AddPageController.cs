using Client.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http.Json;
using System.Text;

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
        List<string> base64Images = new List<string>();

        #region Ingredienti
        [ObservableProperty]
        string contenutoEntry;
        public ObservableCollection<Ingrediente> Listaingredienti { get; set; } = new ObservableCollection<Ingrediente>();
        private List<int> IngredientiSel = new List<int>();
        #endregion


        [RelayCommand]
        public async Task Appearing ()
        {
            Listaingredienti.Clear();
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
                response = await _client.GetAsync($"ingredienti/");

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

        [RelayCommand]
        public async Task Ricerca()
        {
            Listaingredienti.Clear();
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
                if (ContenutoEntry == null || ContenutoEntry == "")
                {
                    response = await _client.GetAsync($"ingredienti/");
                }
                else
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
        [RelayCommand]
        public async Task SelIng(int ingId)
        {
            IngredientiSel.Add(ingId);
        }

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

                    foreach (var imm in base64Images)
                    {
                        var nuovaFoto = new Foto
                        {
                            FotoData = imm,
                            RicettaId = nuovaRicettaId,
                            FotoId = 0,
                            Descrizione = nuovaRicetta.Nome

                        };

                        string jsonFoto = JsonConvert.SerializeObject(nuovaFoto);

                        StringContent contentFoto = new StringContent(jsonFoto, Encoding.UTF8, "application/json");
                        HttpResponseMessage responseFoto = await client.PostAsync("/foto", contentFoto);
                        if (responseFoto.StatusCode != System.Net.HttpStatusCode.Created)
                        {
                            await App.Current.MainPage.DisplayAlert("Errore", "Si è verificato un errore durante il salvataggio della foto", "OK");
                            break;
                        }
                    }
                    foreach (var ingr in IngredientiSel)
                    {
                        HttpResponseMessage responseFoto = await client.GetAsync($"/ricettaInd/{nuovaRicettaId}/{ingr}");
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
            base64Images.Clear();
            IngredientiSel.Clear();
        }

        [RelayCommand]
        public async Task ImpostaImmagine()
        {
            byte[] imageBytes;
            var media = await MediaPicker.PickPhotoAsync();
            if (media != null)
            {
                using (var stream = await media.OpenReadAsync())
                {
                    imageBytes = new byte[stream.Length];
                    await stream.ReadAsync(imageBytes, 0, (int)stream.Length);
                    base64Images.Add(Convert.ToBase64String(imageBytes));
                }
            }
        }

    }
}
