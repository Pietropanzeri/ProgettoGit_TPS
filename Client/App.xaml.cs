using Client.View;
using Microsoft.Maui.Controls;

namespace Client;

public partial class App : Application
{
    public App()
    {
        Preferences.Set("Username", string.Empty);
        Preferences.Set("Password", string.Empty);
        Preferences.Set("BaseRoot", "https://192.168.1.56:5001");

        InitializeComponent();
        MainPage = new MainPage();
    }
}