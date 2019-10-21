﻿using System;
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
    public partial class CityManager : Window
    {
        private Country _country;
        private CityManagerValidator _validator = new CityManagerValidator();

        public CityManager(Country country)
        {
            InitializeComponent();
            _country = country;
            this.DataContext = _country;
        }

        public void AddCity_Click(object sender, EventArgs e)
        {
            if (_validator.IsDataValidForAddition(this))
            {
                City city = new City
                {
                    Name = addCityName.Text,
                    Area = float.Parse(addCityArea.Text),
                    Streets = new List<Street>()
                };
                _country.Cities.Add(city);
                Utils.RefreshComboBox(citySelector);
            }
            else
            {
                MessageBox.Show("Some fields are empty or entered data is not valid");
            }
        }

        public void EditCity_Click(object sender, EventArgs e)
        {
            City city = (City)citySelector.SelectedItem;
            if (city != null)
            {
                if (_validator.IsDataValidForEdition(this))
                {
                    city.Name = editCityName.Text;
                    city.Area = float.Parse(editCityArea.Text);
                    Utils.RefreshComboBox(citySelector);
                }
                else
                {
                    MessageBox.Show("Some fields are empty or entered data is not valid");
                }
            }
            else
            {
                MessageBox.Show("City is not selected");
            }
        }

        public void DeleteCity_Click(object sender, EventArgs e)
        {
            City city = (City)citySelector.SelectedItem;
            if (city != null)
            {
                _country.Cities.Remove(city);
                Utils.RefreshComboBox(citySelector);
            }
            else
            {
                MessageBox.Show("City is not selected");
            }
        }

        public void OpenStreetManager_Click(object sender, EventArgs e)
        {
            City city = (City)citySelector.SelectedItem;
            if (city != null)
            {
                StreetManager streetManager = new StreetManager(city);
                streetManager.ShowDialog();
            }
            else
            {
                MessageBox.Show("City is not selected");
            }
        }

        public void CitySelector_SelectionChanged(object sender, EventArgs e)
        {
            City city = (City)citySelector.SelectedItem;
            editCityName.Text = city?.Name;
            editCityArea.Text = city?.Area.ToString("0.00");
        }
    }
}
