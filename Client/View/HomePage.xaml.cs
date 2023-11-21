using Client.Controller;
using System.Runtime.CompilerServices;

namespace Client.View;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		BindingContext = new HomePageController();
		InitializeComponent();
	}
}