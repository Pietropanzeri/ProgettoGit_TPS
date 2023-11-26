using Client.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Controller
{
    public partial class DetailsPageController : ObservableObject
    {
        [ObservableProperty]
        RicettaFoto ricetta;
        public DetailsPageController(RicettaFoto ricetta ) 
        {
            Ricetta = ricetta;
        }
    }
}
