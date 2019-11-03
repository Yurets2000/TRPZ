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
using WpfMap.Model;
using WpfMap.Model.Entities;
using WpfMap.ViewModels.Admin;

namespace WpfMap
{
    public partial class RoomManager : Window
    {
        public RoomManager(House house)
        {
            InitializeComponent();
            RoomManagerViewModel viewModel = new RoomManagerViewModel(house);
            DataContext = viewModel;
        }
    }
}
