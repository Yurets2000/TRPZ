using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfMap.Model.Entities;

namespace WpfMap.ViewModels.User
{
    public class CityViewModel : INotifyPropertyChanged
    {
        private string _streetName;
        private Street _selectedStreet;
        private List<Street> _selection;

        private RelayCommand _findStreetCommand;
        private RelayCommand _searchByNameCommand;

        public string StreetName
        {
            get { return _streetName; }
            set
            {
                _streetName = value;
                OnPropertyChanged("StreetName");
            }
        }
        public Street SelectedStreet
        {
            get { return _selectedStreet; }
            set
            {
                _selectedStreet = value;
                OnPropertyChanged("SelectedStreet");
            }
        }
        public List<Street> Selection
        {
            get { return _selection; }
            set
            {
                _selection = value;
                OnPropertyChanged("Selection");
            }
        }
        public City City { get; private set; }

        public RelayCommand FindStreetCommand
        {
            get
            {
                return _findStreetCommand ??
                    (_findStreetCommand = new RelayCommand(obj =>
                    {
                        if (SelectedStreet != null)
                        {
                            StreetPage streetPage = new StreetPage(SelectedStreet);
                            streetPage.Show();
                        }
                        else
                        {
                            MessageBox.Show("Street is not selected");
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
                        string name = StreetName?.Trim();
                        if (string.IsNullOrEmpty(name))
                        {
                            Selection = City.Streets;
                        }
                        else
                        {
                            Selection = City.Streets.Where(s => s.Name.Contains(name)).ToList();
                        }
                        Selection.Sort((s1, s2) => s1.Name.CompareTo(s2.Name));
                    }));
            }
        }

        public CityViewModel(City city)
        {
            City = city;
            Selection = City.Streets;
            Selection.Sort((s1, s2) => s1.Name.CompareTo(s2.Name));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}

