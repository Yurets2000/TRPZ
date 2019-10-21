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
    public partial class CountryPage : Window
    {
        private Country _country;
        private List<City> _selection;

        public CountryPage(Country country)
        {
            this.InitializeComponent();
            this.DataContext = _country = country; 
            _selection = _country.Cities;
            _selection.Sort((c1, c2) => c1.Name.CompareTo(c2.Name));
            cities.ItemsSource = _selection;
        }

        public void FindCity_Click(object sender, EventArgs e)
        {
            City city= (City)cities.SelectedValue;
            if (city != null)
            {
                CityPage cityPage = new CityPage(city);
                cityPage.Show();
            }
            else
            {
                MessageBox.Show("City is not selected");
            }
        }

        public void SearchByName_Click(object sender, EventArgs e)
        {
            string name = cityName.Text?.Trim();
            if (string.IsNullOrEmpty(name))
            {
                _selection = _country.Cities;
            }
            else
            {
                _selection = _country.Cities.Where(c => c.Name.Contains(name)).ToList();
            }
            _selection.Sort((c1, c2) => c1.Name.CompareTo(c2.Name));
            cities.ItemsSource = _selection;
        }
    }
}
