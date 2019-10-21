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
using WpfMap.Model.DTO;
using WpfMap.Model.Repositories;
using WpfMap.Views.Common;

namespace WpfMap
{
    public partial class MainWindow : Window
    {
        private UserRepository _repository = UserRepository.GetInstance();

        public MainWindow()
        {
            this.InitializeComponent();
        }

        public void Login_Click(object sender, EventArgs e)
        {
            if (!IsDataValid())
            {
                MessageBox.Show("Some fields are empty or entered data is not valid");
                return;
            }
            User user = _repository.SearchUser(login.Text, password.Password.ToString());
            if (user == null)
            {
                MessageBox.Show("There is no such user!");
            }
            else
            {
                ApplicationContext.CurrentUser = user;
                if (user.Admin)
                {
                    AdminPage adminPage = new AdminPage();
                    adminPage.Show();
                }
                else
                {
                    UserPage userPage = new UserPage();
                    userPage.Show();
                }
                Close();
            }
        }

        public void Registration_Click(object sender, EventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
        }

        private bool IsDataValid()
        {
            return !(string.IsNullOrEmpty(login.Text) ||
                   password == null || 
                   string.IsNullOrEmpty(password.Password.ToString()));
        }
    }
}
