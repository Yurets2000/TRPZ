using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfMap.Model.DTO;

namespace WpfMap
{
    public partial class ResidentPage : Window
    {
        private Resident _resident;

        public ResidentPage(Resident resident)
        {
            this.InitializeComponent();
            DataContext = _resident = resident;
        }
    }
}
