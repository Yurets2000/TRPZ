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

namespace WpfMap
{
    public partial class AdminPage : Window
    {
        public AdminPage()
        {
            this.InitializeComponent();
        }

        public void OpenCountryManager_Click(object sender, EventArgs e)
        {
            CountryManager countryManager = new CountryManager();
            countryManager.ShowDialog();
        }

        public void OpenUserPage_Click(object sender, EventArgs e)
        {
            UserPage userPage = new UserPage();
            userPage.ShowDialog();
        }
    }
}
