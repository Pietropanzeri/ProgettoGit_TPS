using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.Json;
using System.Threading.Tasks;
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
        HttpClientHandler handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
        HttpClient _client = new HttpClient(handler)
        {
             BaseAddress = new Uri("https://192.168.1.56:5001")
        };

        HttpResponseMessage response = new HttpResponseMessage();
        
        try
        {
            response = await _client.GetAsync("/ingredienti");
        }
        catch (Exception e)
        {
            Tst = e.ToString();
        }
            
        if (response.IsSuccessStatusCode)
        {
            //TODO:sistema deserializzazione
            List<Ingrediente> content = await response.Content.ReadFromJsonAsync<List<Ingrediente>>();

            Tst = content[0].Nome;
        }
        else
        {
            // La richiesta non è andata a buon fine, gestisci l'errore di conseguenza
            Tst = "Fallito";
        }
        
    }
}