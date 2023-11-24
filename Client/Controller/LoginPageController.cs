using Client.Model;
using Client.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Client.Controller
{
    public partial class LoginPageController :ObservableObject
    {
        [ObservableProperty]
        bool salvaCredenziali = false;

        [ObservableProperty]
        bool creaUtente = false;

        [ObservableProperty]
        string viewUsername;

        [ObservableProperty]
        string viewPassword;

        [ObservableProperty]
        string message;

        string username;
        string password;

        public LoginPageController() 
        {
            username = Preferences.Get("Username", string.Empty);
            password = Preferences.Get("Password", string.Empty);

            TestPreference();

        }

        public async Task TestPreference()
        {
            if (string.IsNullOrEmpty(username) && string.IsNullOrEmpty(password))
            {
                return;
            }
            ViewUsername = username;
            ViewPassword = password;

            await RichiestaHttpLogin();
        }

        [RelayCommand]
        public async Task Login()
        {
            await RichiestaHttpLogin();
        }

        public async Task RichiestaHttpLogin()
        {

            string baseUri = App.BaseRootHttps;
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            HttpClient _client = new HttpClient(handler)
            {
                BaseAddress = new Uri(baseUri)
            };

            HttpResponseMessage response = new HttpResponseMessage();

            Utente utente = new Utente() { Username = ViewUsername, Password = ViewPassword, UtenteId = 0};
            string jsonUtente = JsonConvert.SerializeObject(utente);
            StringContent content = new StringContent(jsonUtente, Encoding.UTF8, "application/json");
            try
            {
                response = await _client.PostAsync("/login", content);

                if (response.IsSuccessStatusCode)
                {
                    
                    utente = await response.Content.ReadFromJsonAsync<Utente>();
                    jsonUtente = JsonConvert.SerializeObject(utente);
                    App.utente = utente;
                    if (SalvaCredenziali)
                    {
                        Preferences.Set("Username", ViewUsername);
                        Preferences.Set("Password", ViewPassword);
                    }
                     App.Current.MainPage = new MainPage();
                }
                else if (CreaUtente)
                {
                    utente = new Utente() { Username = ViewUsername, Password = ViewPassword, UtenteId = 0 };
                    jsonUtente = JsonConvert.SerializeObject(utente);
                    content = new StringContent(jsonUtente, Encoding.UTF8, "application/json");
                    response = await _client.PostAsync("/utente", content);
                    if (response.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        utente = await response.Content.ReadFromJsonAsync<Utente>();
                        jsonUtente = JsonConvert.SerializeObject(utente);
                        App.utente = utente;
                        if (SalvaCredenziali)
                        {
                            Preferences.Set("Username", ViewUsername);
                            Preferences.Set("Password", ViewPassword);
                        }
                        App.Current.MainPage = new MainPage();
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Errore", "", "ok");
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Errore", "", "ok");
                }
            }
            catch (Exception e)
            {
            }
        }

    }
}
