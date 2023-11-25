using Client.Controller;
using Microsoft.Maui.Controls;

namespace Client.View;

public partial class AddPage : ContentPage
{
	public AddPage()
	{
        BindingContext = new AddPageController();
        InitializeComponent();
	}
}