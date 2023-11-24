using Microsoft.Maui.Controls;
using Client.Controller;

namespace Client.View;

public partial class CercaPage : ContentPage
{
	public CercaPage()
	{
        BindingContext = new CercaPageController();
        InitializeComponent();
	}
}