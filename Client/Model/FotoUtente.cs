using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model
{
    public class FotoUtente
    {
        public int FotoId { get; set; }
        public byte[] FotoData { get; set; }
        public int? UtenteId { get; set; }

    }
}
