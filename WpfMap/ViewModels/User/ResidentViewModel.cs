using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfMap.Model.Entities;

namespace WpfMap.ViewModels.User
{
    public class ResidentViewModel : INotifyPropertyChanged
    {
        public Resident Resident { get; private set; }

        public ResidentViewModel(Resident resident)
        {
            Resident = resident;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
