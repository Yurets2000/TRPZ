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
using WpfMap.Model.Repositories;

namespace WpfMap.Views.Common
{
    public partial class Registration : Window
    {
        private UserRepository _repository = UserRepository.GetInstance();

        public Registration()
        {
            InitializeComponent();
        }

        public void Register_Click(object sender, EventArgs e)
        {
            if (!IsDataValid())
            {
                MessageBox.Show("Some fields are empty or entered data is not valid");
                return;
            }
            User user = _repository.SearchUser(login.Text, password.Password.ToString());
            if(user == null)
            {
                User newUser = new User(login.Text, password.Password.ToString(), false);
                _repository.Users.Add(newUser);
                Close();
            }
            else
            {
                MessageBox.Show("User with such login and password already registered");
            }
        }

        private bool IsDataValid()
        {
            return !(string.IsNullOrEmpty(login.Text) ||
                   password == null ||
                   string.IsNullOrEmpty(password.Password.ToString()));
        }
    }
}
