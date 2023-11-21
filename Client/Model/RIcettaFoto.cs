using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model
{
    public class RicettaFoto
    {
        public int RicettaId { get; set; }
        public int UtenteId { get; set; }
        public string Nome { get; set; }
        public string Preparazione { get; set; }
        public int? Tempo { get; set; }
        public int? Difficolta { get; set; }
        public DateTime DataAggiunta { get; set; }
        public TipoPiatto Piatto { get; set; }
        public string URLFoto { get; set; }
        public RicettaFoto(Ricetta ricetta, string foto)
        {
            RicettaId = ricetta.RicettaId;
            UtenteId = ricetta.UtenteId;
            Nome = ricetta.Nome;
            Preparazione = ricetta.Preparazione;
            Tempo = ricetta.Tempo;
            DataAggiunta = ricetta.DataAggiunta;
            Difficolta = ricetta.Difficolta;
            Piatto = (TipoPiatto)ricetta.Piatto;
            URLFoto = foto;
        }
    }
}
