using Client.Controller;
using Client.Model;
using Microsoft.Maui.Controls;


namespace Client.View;

public partial class DetagliUtente : ContentPage
{
	public DetagliUtente(UtenteFoto utente)
	{
		BindingContext = new DettagliUtenteController(utente);
		InitializeComponent();
	}
}