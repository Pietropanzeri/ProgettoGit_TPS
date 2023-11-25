using Client.Controller;
namespace Client.View;

public partial class UtentePage : ContentPage
{
	public UtentePage()
	{
		BindingContext = new UserPageController();
		InitializeComponent();
	}
}