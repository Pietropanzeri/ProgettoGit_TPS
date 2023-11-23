using Client.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Controller
{
    public partial class CercaPageController : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Ingrediente))]
        bool ricetta;
        public bool Ingrediente => !Ricetta;

        [RelayCommand]
        private void ChangeIcona()
        {
            Ricetta=!Ricetta;
        }
    }
}
