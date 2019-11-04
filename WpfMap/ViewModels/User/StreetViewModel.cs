using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfMap.Models.Entities;

namespace WpfMap.ViewModels.User
{
    public class StreetViewModel : INotifyPropertyChanged
    {
        private string _houseAddress;
        private House _selectedHouse;
        private List<House> _selection;

        private RelayCommand _findHouseCommand;
        private RelayCommand _searchByAddressCommand;

        public string HouseAddress
        {
            get { return _houseAddress; }
            set
            {
                _houseAddress = value;
                OnPropertyChanged("HouseAddress");
            }
        }
        public House SelectedHouse
        {
            get { return _selectedHouse; }
            set
            {
                _selectedHouse = value;
                OnPropertyChanged("SelectedHouse");
            }
        }
        public List<House> Selection
        {
            get { return _selection; }
            set
            {
                _selection = value;
                OnPropertyChanged("Selection");
            }
        }
        public Street Street { get; private set; }

        public RelayCommand FindHouseCommand
        {
            get
            {
                return _findHouseCommand ??
                    (_findHouseCommand = new RelayCommand(obj =>
                    {
                        if (SelectedHouse != null)
                        {
                            HousePage housePage = new HousePage(SelectedHouse);
                            housePage.Show();
                        }
                        else
                        {
                            MessageBox.Show("House is not selected");
                        }
                    }));
            }
        }
        public RelayCommand SearchByAddressCommand
        {
            get
            {
                return _searchByAddressCommand ??
                    (_searchByAddressCommand = new RelayCommand(obj =>
                    {
                        string address = HouseAddress?.Trim();
                        if (string.IsNullOrEmpty(address))
                        {
                            Selection = Street.Houses;
                        }
                        else
                        {
                            Selection = Street.Houses.Where(h => h.Address.Contains(address)).ToList();
                        }
                        Selection.Sort((h1, h2) => h1.Address.CompareTo(h2.Address));
                    }));
            }
        }

        public StreetViewModel(Street street)
        {
            Street = street;
            Selection = Street.Houses;
            Selection.Sort((h1, h2) => h1.Address.CompareTo(h2.Address));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
