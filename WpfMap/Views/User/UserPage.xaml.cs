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

namespace WpfMap
{
    public partial class UserPage : Window
    {
        private List<Country> _selection;

        public UserPage()
        {
            this.InitializeComponent();
            this.DataContext = _selection = ModelRepository.GetInstance().Countries;
            _selection.Sort((c1, c2) => c1.Name.CompareTo(c2.Name));
            countries.ItemsSource = _selection;
        }

        public void FindCountry_Click(object sender, EventArgs e)
        {
            Country country = (Country)countries.SelectedValue;
            if (country != null)
            {
                CountryPage countryPage = new CountryPage(country);
                countryPage.Show();      
            }
            else
            {
                MessageBox.Show("Country is not selected");
            }
        }

        public void SearchByName_Click(object sender, EventArgs e)
        {
            string name = countryName.Text?.Trim();
            if (string.IsNullOrEmpty(name))
            {
                _selection = ModelRepository.GetInstance().Countries;
            }
            else
            {
                _selection = ModelRepository.GetInstance().Countries.Where(c => c.Name.Contains(name)).ToList();
            }
            _selection.Sort((c1, c2) => c1.Name.CompareTo(c2.Name));
            countries.ItemsSource = _selection;
        }

        public void OpenLoginPage_Click(object sender, EventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
