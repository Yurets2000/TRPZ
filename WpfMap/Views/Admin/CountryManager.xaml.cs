using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WpfMap.Model.Repositories;

namespace WpfMap
{
    public partial class CountryManager : Window
    {
        private ModelRepository _repository = ModelRepository.GetInstance();
        private CountryManagerValidator _validator = new CountryManagerValidator();

        public CountryManager()
        {
            InitializeComponent();
            this.DataContext = _repository;
        }

        private void AddCountry_Click(object sender, EventArgs e)
        {
            if (_validator.IsDataValidForAddition(this))
            {
                Country country = new Country
                {
                    Name = addCountryName.Text,
                    Cities = new List<City>()
                };
                _repository.Countries.Add(country);
                Utils.RefreshComboBox(countrySelector);
            }
            else
            {
                MessageBox.Show("Some fields are empty or entered data is not valid");
            }
        }

        private void EditCountry_Click(object sender, EventArgs e)
        {
            Country country = (Country)countrySelector.SelectedItem;
            if (country != null)
            {
                if (_validator.IsDataValidForEdition(this))
                {
                    country.Name = editCountryName.Text;
                    country.Capital = (City)editCapital.SelectedItem;
                    Utils.RefreshComboBox(countrySelector);
                }
                else
                {
                    MessageBox.Show("Some fields are empty or entered data is not valid");
                }
            }
            else
            {
                MessageBox.Show("Country is not selected");
            }
        }

        private void DeleteCountry_Click(object sender, EventArgs e)
        {
            Country country = (Country)countrySelector.SelectedItem;
            if (country != null)
            {
                _repository.Countries.Remove(country);
                Utils.RefreshComboBox(countrySelector);
            }
            else
            {
                MessageBox.Show("Country is not selected");
            }
        }

        private void OpenCityManager_Click(object sender, EventArgs e)
        {
            Country country = (Country)countrySelector.SelectedItem;
            if (country != null)
            {
                CityManager cityManager = new CityManager(country);
                cityManager.ShowDialog();
            }
            else
            {
                MessageBox.Show("Country is not selected");
            }
        }

        private void CountrySelector_SelectionChanged(object sender, EventArgs e)
        {
            Country country = (Country)countrySelector.SelectedItem;
            editCountryName.Text = country?.Name;
            editCapital.ItemsSource = country?.Cities;
            editCapital.SelectedItem = country?.Capital;
        }
    }
}
