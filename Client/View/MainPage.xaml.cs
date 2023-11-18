using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Controller;

namespace Client.View;

public partial class MainPage : TabbedPage
{
    public MainPage()
    {
        BindingContext = new MainPageController();
        InitializeComponent(); 
        var homePage = new HomePage();
        var addPage = new AddPage();
        var cercaPage = new CercaPage();
        var novitàPage = new NovitàPage();
        var elementiSalvatiPage = new ElementiSalvatiPage();

        homePage.IconImageSource = "dotnet_bot.svg";
        addPage.IconImageSource = "dotnet_bot.svg";
        cercaPage.IconImageSource = "dotnet_bot.svg";
        novitàPage.IconImageSource = "dotnet_bot.svg";
        elementiSalvatiPage.IconImageSource = "dotnet_bot.svg";

        homePage.Title = "";
        addPage.Title = "";
        cercaPage.Title = "";
        novitàPage.Title = "";
        elementiSalvatiPage.Title = "";

        Children.Add(cercaPage);
        Children.Add(addPage);
        Children.Add(homePage);
        Children.Add(novitàPage);
        Children.Add(elementiSalvatiPage);

        CurrentPage = homePage;
    }

}