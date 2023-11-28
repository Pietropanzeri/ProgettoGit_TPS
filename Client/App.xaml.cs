using Client.Model;
using Client.View;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;

namespace Client;

public partial class App : Application
{
    public static string BaseRootHttp = "http://192.168.1.56:5000";
    public static string BaseRootHttps = "https://192.168.1.56:5001";

    public static Utente utente = new Utente();
    public App()
    {

        string username = Preferences.Get("Username", string.Empty);
        string password = Preferences.Get("Password", string.Empty);

        if(username == "" || password == "")
        {
            Preferences.Set("Username", string.Empty);
            Preferences.Set("Password", string.Empty);
        }


        InitializeComponent();
        MainPage = new LoginPage();
    }
}