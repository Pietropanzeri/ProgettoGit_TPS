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
        var elementiSalvatiPage = new ElementiSalvatiPage();
        var utentePage = new NovitàPage();

        homePage.IconImageSource = "news.svg";
        addPage.IconImageSource = "add.svg";
        cercaPage.IconImageSource = "find.svg";
        elementiSalvatiPage.IconImageSource = "saved.svg";
        utentePage.IconImageSource = "user.svg";

        homePage.Title = "";
        addPage.Title = "";
        cercaPage.Title = "";
        elementiSalvatiPage.Title = "";
        utentePage.Title = "";

        Children.Add(cercaPage);
        Children.Add(addPage);
        Children.Add(homePage);
        Children.Add(elementiSalvatiPage);
        Children.Add(utentePage);

        CurrentPage = homePage;
    }

}