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
using WpfMap.Helpers.Validators;
using WpfMap.Model;
using WpfMap.Model.DTO;

namespace WpfMap
{
    public partial class StreetManager : Window
    {
        private City _city;
        private StreetManagerValidator _validator = new StreetManagerValidator();

        public StreetManager(City city)
        {
            InitializeComponent();
            _city = city;
            this.DataContext = _city;
        }

        public void AddStreet_Click(object sender, EventArgs e)
        {
            if (_validator.IsDataValidForAddition(this))
            {
                Street street = new Street
                {
                    Name = addStreetName.Text, 
                    Houses = new List<House>()
                };
                _city.Streets.Add(street);
                Utils.RefreshComboBox(streetSelector);
            }
            else
            {
                MessageBox.Show("Some fields are empty or entered data is not valid");
            }
        }

        public void EditStreet_Click(object sender, EventArgs e)
        {
            Street street = (Street)streetSelector.SelectedItem;
            if (street != null)
            {
                if (_validator.IsDataValidForEdition(this))
                {
                    street.Name = editStreetName.Text;
                    Utils.RefreshComboBox(streetSelector);
                }
                else
                {
                    MessageBox.Show("Some fields are empty or entered data is not valid");
                }
            }
            else
            {
                MessageBox.Show("Street is not selected");
            }
        }

        public void DeleteStreet_Click(object sender, EventArgs e)
        {
            Street street = (Street)streetSelector.SelectedItem;
            if (street != null)
            {
                _city.Streets.Remove(street);
                Utils.RefreshComboBox(streetSelector);
            }
            else
            {
                MessageBox.Show("Street is not selected");
            }
        }

        public void OpenHouseManager_Click(object sender, EventArgs e)
        {
            Street street = (Street)streetSelector.SelectedItem;
            if (street != null)
            {
                HouseManager houseManager = new HouseManager(street);
                houseManager.ShowDialog();
            }
            else
            {
                MessageBox.Show("Street is not selected");
            }
        }

        public void StreetSelector_SelectionChanged(object sender, EventArgs e)
        {
            Street street = (Street)streetSelector.SelectedItem;
            editStreetName.Text = street?.Name;
        }
    }
}
