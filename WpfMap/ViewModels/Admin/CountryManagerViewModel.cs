using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfMap.Model.Entities;
using System.ComponentModel.DataAnnotations;
using WpfMap.Model.Repositories;
using System.Collections.ObjectModel;

namespace WpfMap.ViewModels.Admin
{
    public class CountryManagerViewModel : INotifyPropertyChanged
    {     
        private string _addCountryName;

        private Country _selectedCountry;
        private ObservableCollection<Country> _countries;
        private string _editCountryName;
        private City _editCountryCapital;

        private RelayCommand _selectCountryCommand;
        private RelayCommand _addCountryCommand;
        private RelayCommand _editCountryCommand;
        private RelayCommand _deleteCountryCommand;
        private RelayCommand _openCityManagerCommand;

        private ModelRepository _repository = ModelRepository.GetInstance();
        public ObservableCollection<Country> Countries
        {
            get { return _countries; }
            set
            {
                ValidateProperty(value, "Countries");
                _countries = value;
                OnPropertyChanged("Countries");
            }
        }

        [Required(ErrorMessage = "should not be empty")]
        public string AddCountryName
        {
            get { return _addCountryName; }
            set
            {
                ValidateProperty(value, "AddCountryName");
                _addCountryName = value;
                OnPropertyChanged("AddCountryName");
            }
        }
        [Required(ErrorMessage = "should not be empty")]
        public string EditCountryName
        {
            get { return _editCountryName; }
            set
            {
                ValidateProperty(value, "EditCountryName");
                _editCountryName = value;
                OnPropertyChanged("EditCountryName");
            }
        }
        [Required(ErrorMessage = "should be selected")]
        public City EditCountryCapital
        {
            get { return _editCountryCapital; }
            set
            {
                ValidateProperty(value, "EditCountryCapital");
                _editCountryCapital = value;
                OnPropertyChanged("EditCountryCapital");
            }
        }
        [Required(ErrorMessage = "should be selected")]
        public Country SelectedCountry
        {
            get { return _selectedCountry; }
            set
            {
                ValidateProperty(value, "SelectedCountry");
                _selectedCountry = value;
                OnPropertyChanged("SelectedCountry");
            }
        }

        public RelayCommand AddCountryCommand
        {
            get
            {
                return _addCountryCommand ??
                    (_addCountryCommand = new RelayCommand(obj =>
                    {
                        Country country = new Country
                        {
                            Name = AddCountryName,
                            Cities = new List<City>()
                        };
                        _repository.Countries.Add(country);
                        Countries = new ObservableCollection<Country>(_repository.Countries);
                    }));
            }
        }
        public RelayCommand SelectCountryCommand
        {
            get
            {
                return _selectCountryCommand ??
                    (_selectCountryCommand = new RelayCommand(obj =>
                    {
                        EditCountryName = SelectedCountry?.Name;
                        EditCountryCapital = SelectedCountry?.Capital;      
                    }));
            }
        }
        public RelayCommand EditCountryCommand
        {
            get
            {
                return _editCountryCommand ??
                    (_editCountryCommand = new RelayCommand(obj =>
                    {
                        SelectedCountry.Name = EditCountryName;
                        SelectedCountry.Capital = EditCountryCapital;
                        Countries = new ObservableCollection<Country>(_repository.Countries);
                        SelectedCountry = SelectedCountry;
                    }));
            }
        }
        public RelayCommand DeleteCountryCommand
        {
            get
            {
                return _deleteCountryCommand ??
                    (_deleteCountryCommand = new RelayCommand(obj =>
                    {
                        _repository.Countries.Remove(SelectedCountry);
                        Countries = new ObservableCollection<Country>(_repository.Countries);
                    }));
            }
        }
        public RelayCommand OpenCityManagerCommand
        {
            get
            {
                return _openCityManagerCommand ??
                    (_openCityManagerCommand = new RelayCommand(obj =>
                    {
                        CityManager cityManager = new CityManager(SelectedCountry);
                        cityManager.ShowDialog();
                    }));
            }
        }

        public CountryManagerViewModel()
        {
            Countries = new ObservableCollection<Country>(_repository.Countries);
        }

        private void ValidateProperty<T>(T value, string name)
        {
            Validator.ValidateProperty(value, new ValidationContext(this, null, null)
            {
                MemberName = name
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
