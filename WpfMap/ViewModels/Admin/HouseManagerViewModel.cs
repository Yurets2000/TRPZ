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

namespace WpfMap.ViewModels.Admin
{
    public class HouseManagerViewModel : INotifyPropertyChanged
    {
        private string _addHouseAddress;

        private House _selectedHouse;
        private ObservableCollection<House> _houses;
        private string _editHouseAddress;

        private RelayCommand _selectHouseCommand;
        private RelayCommand _addHouseCommand;
        private RelayCommand _editHouseCommand;
        private RelayCommand _deleteHouseCommand;
        private RelayCommand _openRoomManagerCommand;

        public ObservableCollection<House> Houses
        {
            get { return _houses; }
            set
            {
                ValidateProperty(value, "Houses");
                _houses = value;
                OnPropertyChanged("Houses");
            }
        }

        [Required(ErrorMessage = "should not be empty")]
        public string AddHouseAddress
        {
            get { return _addHouseAddress; }
            set
            {
                ValidateProperty(value, "AddHouseAddress");
                _addHouseAddress = value;
                OnPropertyChanged("AddHouseAddress");
            }
        }
        [Required(ErrorMessage = "should not be empty")]
        public string EditHouseAddress
        {
            get { return _editHouseAddress; }
            set
            {
                ValidateProperty(value, "EditHouseAddress");
                _editHouseAddress = value;
                OnPropertyChanged("EditHouseAddress");
            }
        }
        [Required(ErrorMessage = "should be selected")]
        public House SelectedHouse
        {
            get { return _selectedHouse; }
            set
            {
                ValidateProperty(value, "SelectedHouse");
                _selectedHouse = value;
                OnPropertyChanged("SelectedHouse");
            }
        }

        public RelayCommand AddHouseCommand
        {
            get
            {
                return _addHouseCommand ??
                    (_addHouseCommand = new RelayCommand(obj =>
                    {
                        House house = new House
                        {
                            Address = AddHouseAddress
                        };
                        Street.Houses.Add(house);
                        Houses = new ObservableCollection<House>(Street.Houses);
                    }));
            }
        }
        public RelayCommand SelectHouseCommand
        {
            get
            {
                return _selectHouseCommand ??
                    (_selectHouseCommand = new RelayCommand(obj =>
                    {
                        EditHouseAddress = SelectedHouse?.Address;
                    }));
            }
        }
        public RelayCommand EditHouseCommand
        {
            get
            {
                return _editHouseCommand ??
                    (_editHouseCommand = new RelayCommand(obj =>
                    {
                        SelectedHouse.Address = EditHouseAddress;
                        Houses = new ObservableCollection<House>(Street.Houses);
                        SelectedHouse = SelectedHouse;
                    }));
            }
        }
        public RelayCommand DeleteHouseCommand
        {
            get
            {
                return _deleteHouseCommand ??
                    (_deleteHouseCommand = new RelayCommand(obj =>
                    {
                        Street.Houses.Remove(SelectedHouse);
                        Houses = new ObservableCollection<House>(Street.Houses);
                    }));
            }
        }
        public RelayCommand OpenRoomManagerCommand
        {
            get
            {
                return _openRoomManagerCommand ??
                    (_openRoomManagerCommand = new RelayCommand(obj =>
                    {
                        RoomManager roomManager = new RoomManager(SelectedHouse);
                        roomManager.ShowDialog();
                    }));
            }
        }

        public Street Street { get; private set; }

        private void ValidateProperty<T>(T value, string name)
        {
            Validator.ValidateProperty(value, new ValidationContext(this, null, null)
            {
                MemberName = name
            });
        }

        public HouseManagerViewModel(Street street)
        {
            Street = street;
            Houses = new ObservableCollection<House>(Street.Houses);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
