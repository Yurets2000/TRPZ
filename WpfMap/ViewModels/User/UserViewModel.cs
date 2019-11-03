using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfMap.Model.Entities;
using WpfMap.Model.Repositories;

namespace WpfMap.ViewModels.User
{
    public class UserViewModel : INotifyPropertyChanged
    {
        private string _countryName;
        private Country _selectedCountry;
        private List<Country> _selection;

        private RelayCommand _findCountryCommand;
        private RelayCommand _searchByNameCommand;
        private RelayCommand _openLoginPageCommand;

        public event EventHandler ClosingRequest;

        public string CountryName
        {
            get { return _countryName; }
            set
            {
                _countryName = value;
                OnPropertyChanged("CountryName"); 
            }
        }
        public Country SelectedCountry
        {
            get { return _selectedCountry; }
            set
            {
                _selectedCountry = value;
                OnPropertyChanged("SelectedCountry");
            }
        }
        public List<Country> Selection {
            get { return _selection;  }
            set
            {
                _selection = value;
                OnPropertyChanged("Selection");
            }
        }

        public RelayCommand FindCountryCommand
        {
            get
            {
                return _findCountryCommand ??
                    (_findCountryCommand = new RelayCommand(obj =>
                    {
                        if (SelectedCountry != null)
                        {
                            CountryPage countryPage = new CountryPage(SelectedCountry);
                            countryPage.Show();
                        }
                        else
                        {
                            MessageBox.Show("Country is not selected");
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
                        string name = CountryName?.Trim();
                        if (string.IsNullOrEmpty(name))
                        {
                            Selection = ModelRepository.GetInstance().Countries;
                        }
                        else
                        {
                            Selection = ModelRepository.GetInstance().Countries.Where(c => c.Name.Contains(name)).ToList();
                        }
                        Selection.Sort((c1, c2) => c1.Name.CompareTo(c2.Name));
                    }));
            }
        }
        public RelayCommand OpenLoginPageCommand
        {
            get
            {
                return _openLoginPageCommand ??
                    (_openLoginPageCommand = new RelayCommand(obj =>
                    {
                        Login mainWindow = new Login();
                        mainWindow.Show();
                        ClosingRequest?.Invoke(this, EventArgs.Empty);
                    }));
            }
        }

        public UserViewModel()
        {
            Selection = ModelRepository.GetInstance().Countries;
            Selection.Sort((c1, c2) => c1.Name.CompareTo(c2.Name));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
