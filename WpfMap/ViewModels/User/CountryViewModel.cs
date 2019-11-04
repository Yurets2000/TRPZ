using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfMap.Models.Contexts;
using WpfMap.Models.Entities;
using WpfMap.Models.Repositories;

namespace WpfMap.ViewModels.User
{
    public class CountryViewModel : INotifyPropertyChanged
    {       
        private string _cityName;
        private City _selectedCity;
        private List<City> _selection;

        private RelayCommand _findCityCommand;
        private RelayCommand _searchByNameCommand;
   
        public string CityName
        {
            get { return _cityName; }
            set
            {
                _cityName = value;
                OnPropertyChanged("CityName");
            }
        }
        public City SelectedCity
        {
            get { return _selectedCity; }
            set
            {
                _selectedCity = value;
                OnPropertyChanged("SelectedCity");
            }
        }
        public List<City> Selection
        {
            get { return _selection; }
            set
            {
                _selection = value;
                OnPropertyChanged("Selection");
            }
        }
        public Country Country { get; private set; }

        public RelayCommand FindCityCommand
        {
            get
            {
                return _findCityCommand ??
                    (_findCityCommand = new RelayCommand(obj =>
                    {
                        if (SelectedCity != null)
                        {
                            CityPage cityPage = new CityPage(SelectedCity);
                            cityPage.Show();
                        }
                        else
                        {
                            MessageBox.Show("City is not selected");
                        }
                    }));
            }
        }
        public RelayCommand SearchByNameCommand
        {
            get
            {
                return _searchByNameCommand ??
                    (_searchByNameCommand = new RelayCommand(obj =>
                    {
                        string name = CityName?.Trim();
                        if (string.IsNullOrEmpty(name))
                        {
                            Selection = Country.Cities;
                        }
                        else
                        {
                            Selection = Country.Cities.Where(c => c.Name.Contains(name)).ToList();
                        }
                        Selection.Sort((c1, c2) => c1.Name.CompareTo(c2.Name));
                    }));
            }
        }

        public CountryViewModel(Country country)
        {
            using (MainContext context = new MainContext())
            {
                Country = country;
                Selection = Country.Cities;
                Selection.Sort((c1, c2) => c1.Name.CompareTo(c2.Name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
