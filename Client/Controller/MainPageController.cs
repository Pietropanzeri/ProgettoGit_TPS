using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.Json;
using Client.Model;


namespace Client.Controller;

public partial class MainPageController : ObservableObject
{
    [ObservableProperty] 
    private string tst;

    public MainPageController()
    {
        GetDataFromApi();
    }
    
    public async Task GetDataFromApi()
    {
        // Oggetto HttpClient per effettuare la richiesta all'API, con BaseAddress
        HttpClient _client = new HttpClient()
        {
            BaseAddress = new Uri("https://127.0.0.1:7198")
        };
            HttpResponseMessage response = await _client.GetAsync("/");
            
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                
                List<Ingrediente> data = JsonSerializer.Deserialize<List<Ingrediente>>(content);
                
                Tst = data[0].Nome;
            }
            else
            {
                // La richiesta non è andata a buon fine, gestisci l'errore di conseguenza
                Tst = "Fallito";
            }
        
    }
}