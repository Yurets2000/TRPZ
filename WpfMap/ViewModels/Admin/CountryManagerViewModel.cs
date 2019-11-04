using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfMap.Models.Entities;
using System.ComponentModel.DataAnnotations;
using WpfMap.Models.Repositories;
using System.Collections.ObjectModel;
using WpfMap.Models.Contexts;

namespace WpfMap.ViewModels.Admin
{
    public class CountryManagerViewModel : INotifyPropertyChanged
    {     
        private string _addCountryName;

        private Country _selectedCountry;
        private ObservableCollection<Country> _countries;
        private string _editCountryName;

        private RelayCommand _selectCountryCommand;
        private RelayCommand _addCountryCommand;
        private RelayCommand _editCountryCommand;
        private RelayCommand _deleteCountryCommand;
        private RelayCommand _openCityManagerCommand;

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
                        using (MainContext context = new MainContext())
                        {
                            var countryRep = new CountryRepository(context);
                            Country country = new Country
                            {
                                Name = AddCountryName,
                                Cities = new List<City>()
                            };
                            countryRep.Add(country);
                            context.SaveChanges();
                            Countries = new ObservableCollection<Country>(countryRep.List());
                        }
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
                        using (MainContext context = new MainContext())
                        {
                            var countryRep = new CountryRepository(context);
                            SelectedCountry.Name = EditCountryName;
                            countryRep.Edit(SelectedCountry);
                            context.SaveChanges();
                            Countries = new ObservableCollection<Country>(countryRep.List());
                            SelectedCountry = SelectedCountry;
                        }
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
                        using (MainContext context = new MainContext())
                        {
                            var countryRep = new CountryRepository(context);
                            countryRep.Delete(SelectedCountry);
                            context.SaveChanges();
                            Countries = new ObservableCollection<Country>(countryRep.List());
                        }
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
            using (MainContext context = new MainContext())
            {
                var countryRep = new CountryRepository(context);
                Countries = new ObservableCollection<Country>(countryRep.List());
            }
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
