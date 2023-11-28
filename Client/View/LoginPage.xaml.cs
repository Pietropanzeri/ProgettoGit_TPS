using Client.Controller;
namespace Client.View;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		BindingContext = new LoginPageController();
		InitializeComponent();
	}
}