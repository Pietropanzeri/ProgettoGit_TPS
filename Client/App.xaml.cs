using Client.Model;
using Client.View;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;

namespace Client;

public partial class App : Application
{
    public App()
    {
        Preferences.Set("Username", string.Empty);
        Preferences.Set("Password", string.Empty);
        Preferences.Set("BaseRoot", "https://192.168.1.12:5001");
        Utente utente = new Utente();
        string jsonUtente = JsonConvert.SerializeObject(utente);
        Preferences.Set("Utente", jsonUtente);

        InitializeComponent();
        MainPage = new LoginPage();
    }
}