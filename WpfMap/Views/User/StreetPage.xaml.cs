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
using WpfMap.Model.Entities;
using WpfMap.ViewModels.User;

namespace WpfMap
{
    public partial class StreetPage : Window
    {
        public StreetPage(Street street)
        {
            InitializeComponent();
            StreetViewModel viewModel = new StreetViewModel(street);
            DataContext = viewModel;
        } 
    }
}
