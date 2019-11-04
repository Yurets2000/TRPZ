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
using WpfMap.Models.Entities;
using WpfMap.Models.Repositories;
using WpfMap.ViewModels.Common;

namespace WpfMap.Views.Common
{
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
            RegistrationViewModel viewModel = new RegistrationViewModel();
            DataContext = viewModel;
            viewModel.ClosingRequest += (sender, e) => Close();
        }
    }
}
