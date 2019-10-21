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
    public partial class CityPage : Window
    {
        private City _city;
        private List<Street> _selection;

        public CityPage(City city)
        {
            this.InitializeComponent();
            this.DataContext = _city = city;
            _selection = _city.Streets;
            _selection.Sort((s1, s2) => s1.Name.CompareTo(s2.Name));
            streets.ItemsSource = _selection;
        }

        public void FindStreet_Click(object sender, EventArgs e)
        {
            Street street = (Street)streets.SelectedValue;
            if (street != null)
            {
                StreetPage streetPage = new StreetPage(street);
                streetPage.Show();
            }
            else
            {
                MessageBox.Show("Street is not selected");
            }
        }

        public void SearchByName_Click(object sender, EventArgs e)
        {
            string name = streetName.Text?.Trim();
            if (string.IsNullOrEmpty(name))
            {
                _selection = _city.Streets;
            }
            else
            {
                _selection = _city.Streets.Where(s => s.Name.Contains(name)).ToList();
            }
            _selection.Sort((s1, s2) => s1.Name.CompareTo(s2.Name));
            streets.ItemsSource = _selection;
        }
    }
}
