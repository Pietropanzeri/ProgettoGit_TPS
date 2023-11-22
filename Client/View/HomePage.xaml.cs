using Client.Controller;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;

namespace Client.View;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		BindingContext = new HomePageController();
		InitializeComponent();
	}
}