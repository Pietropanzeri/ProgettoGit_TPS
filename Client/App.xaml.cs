using Client.View;
using Microsoft.Maui.Controls;

namespace Client;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new MainPage();
    }
}