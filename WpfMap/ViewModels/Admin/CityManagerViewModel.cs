using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfMap.Model.Entities;
using WpfMap.Model.Repositories;

namespace WpfMap.ViewModels.Admin
{
    public class CityManagerViewModel : INotifyPropertyChanged
    {
        private string _addCityName;
        private string _addCityArea;

        private City _selectedCity;
        private ObservableCollection<City> _cities;
        private string _editCityName;
        private string _editCityArea;

        private RelayCommand _selectCityCommand;
        private RelayCommand _addCityCommand;
        private RelayCommand _editCityCommand;
        private RelayCommand _deleteCityCommand;
        private RelayCommand _openStreetManagerCommand;

        public ObservableCollection<City> Cities
        {
            get { return _cities; }
            set
            {
                _cities = value;
                OnPropertyChanged("Cities");
            }
        }

        [Required(ErrorMessage = "should not be empty")]
        public string AddCityName
        {
            get { return _addCityName; }
            set
            {
                ValidateProperty(value, "AddCityName");
                _addCityName = value;
                OnPropertyChanged("AddCityName");
            }
        }
        [Required(ErrorMessage = "should not be empty")]
        [RegularExpression(@"^[1-9][0-9]{0,3},[0-9]{2}$", ErrorMessage = "should be numeric")]
        public string AddCityArea
        {
            get { return _addCityArea; }
            set
            {
                ValidateProperty(value, "AddCityArea");
                _addCityArea = value;
                OnPropertyChanged("AddCityArea");
            }
        }
        [Required(ErrorMessage = "should not be empty")]
        public string EditCityName
        {
            get { return _editCityName; }
            set
            {
                ValidateProperty(value, "EditCityName");
                _editCityName = value;
                OnPropertyChanged("EditCityName");
            }
        }
        [Required(ErrorMessage = "should not be empty")]
        [RegularExpression(@"^[1-9][0-9]{0,3},[0-9]{2}$", ErrorMessage = "should be numeric")]
        public string EditCityArea
        {
            get { return _editCityArea; }
            set
            {
                ValidateProperty(value, "EditCityArea");
                _editCityArea = value;
                OnPropertyChanged("EditCityArea");
            }
        }
        [Required(ErrorMessage = "should be selected")]
        public City SelectedCity
        {
            get { return _selectedCity; }
            set
            {
                ValidateProperty(value, "SelectedCity");
                _selectedCity = value;
                OnPropertyChanged("SelectedCity");
            }
        }

        public RelayCommand AddCityCommand
        {
            get
            {
                return _addCityCommand ??
                    (_addCityCommand = new RelayCommand(obj =>
                    {
                        City city = new City
                        {
                            Name = AddCityName,
                            Area = float.Parse(AddCityArea)
                        };
                        Country.Cities.Add(city);
                        Cities = new ObservableCollection<City>(Country.Cities);
                    }));
            }
        }
        public RelayCommand SelectCityCommand
        {
            get
            {
                return _selectCityCommand ??
                    (_selectCityCommand = new RelayCommand(obj =>
                    {
                        EditCityName = SelectedCity?.Name;
                        EditCityArea = SelectedCity?.Area.ToString("0.00");
                    }));
            }
        }
        public RelayCommand EditCityCommand
        {
            get
            {
                return _editCityCommand ??
                    (_editCityCommand = new RelayCommand(obj =>
                    {
                        SelectedCity.Name = EditCityName;
                        SelectedCity.Area = float.Parse(EditCityArea);
                        Cities = new ObservableCollection<City>(Country.Cities);
                        SelectedCity = SelectedCity;
                    }));
            }
        }
        public RelayCommand DeleteCityCommand
        {
            get
            {
                return _deleteCityCommand ??
                    (_deleteCityCommand = new RelayCommand(obj =>
                    {
                        Country.Cities.Remove(SelectedCity);
                        Cities = new ObservableCollection<City>(Country.Cities);
                    }));
            }
        }
        public RelayCommand OpenStreetManagerCommand
        {
            get
            {
                return _openStreetManagerCommand ??
                    (_openStreetManagerCommand = new RelayCommand(obj =>
                    {
                        StreetManager streetManager = new StreetManager(SelectedCity);
                        streetManager.ShowDialog();
                    }));
            }
        }

        public Country Country { get; private set; }

        private void ValidateProperty<T>(T value, string name)
        {
            Validator.ValidateProperty(value, new ValidationContext(this, null, null)
            {
                MemberName = name
            });
        }

        public CityManagerViewModel(Country country)
        {
            Country = country;
            Cities = new ObservableCollection<City>(Country.Cities);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
