using System.Net.Http.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using Client.Model;
using Client.View;

namespace Client.Controller;

public partial class MainPageController : ObservableObject
{
    [ObservableProperty] 
    private string tst;

    public MainPageController()
    {
    }
}