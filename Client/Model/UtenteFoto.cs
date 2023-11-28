using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model
{
    public class UtenteFoto
    {
        public int UtenteId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string URLFoto { get; set; }

        public UtenteFoto(Utente utente, string foto) 
        { 
            UtenteId = utente.UtenteId;
            Username = utente.Username;
            Password = utente.Password;
            URLFoto = foto;
        }
    }
}
