using Client.Controller;
using Client.Model;

namespace Client.View;

public partial class DetailsPage : ContentPage
{
	public DetailsPage(RicettaFoto ricetta)
	{
		BindingContext = new DetailsPageController(ricetta);
		InitializeComponent();
	}
}