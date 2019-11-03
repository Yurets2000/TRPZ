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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfMap.Model;
using WpfMap.Model.Entities;
using WpfMap.Model.Repositories;
using WpfMap.ViewModels.Common;
using WpfMap.Views.Common;

namespace WpfMap
{
    public partial class Login : Window
    {
        private UserRepository _repository = UserRepository.GetInstance();

        public Login()
        {
            InitializeComponent();
            LoginViewModel viewModel = new LoginViewModel();
            DataContext = viewModel;
            viewModel.ClosingRequest += (sender, e) => Close();
        }
    }
}
