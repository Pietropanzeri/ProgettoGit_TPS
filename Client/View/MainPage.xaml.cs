using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Controller;

namespace Client.View;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        BindingContext = new MainPageController();
        InitializeComponent();
    }
}