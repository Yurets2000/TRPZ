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
    public partial class StreetPage : Window
    {
        private Street _street;
        private List<House> _selection;

        public StreetPage(Street street)
        {
            this.InitializeComponent();
            this.DataContext = _street = street;
            _selection = _street.Houses;
            _selection.Sort((h1, h2) => h1.Address.CompareTo(h2.Address));
            houses.ItemsSource = _selection;
        }

        public void FindHouse_Click(object sender, EventArgs e)
        {
            House house = (House)houses.SelectedValue;
            if (house != null)
            {
                HousePage housePage = new HousePage(house);
                housePage.Show();
            }
            else
            {
                MessageBox.Show("House is not selected");
            }
        }

        public void SearchByAddress_Click(object sender, EventArgs e)
        {
            string name = houseAddress.Text?.Trim();
            if (string.IsNullOrEmpty(name))
            {
                _selection = _street.Houses;
            }
            else
            {
                _selection = _street.Houses.Where(h => h.Address.Contains(name)).ToList();
            }
            _selection.Sort((h1, h2) => h1.Address.CompareTo(h2.Address));
            houses.ItemsSource = _selection;
        }
    }
}
