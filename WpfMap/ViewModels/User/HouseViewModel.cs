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
    public class HouseViewModel : INotifyPropertyChanged
    {
        private string _roomNumber;
        private Room _selectedRoom;
        private List<Room> _selection;

        private RelayCommand _findRoomCommand;
        private RelayCommand _searchByNumberCommand;

        public string RoomNumber
        {
            get { return _roomNumber; }
            set
            {
                _roomNumber = value;
                OnPropertyChanged("RoomNumber");
            }
        }
        public Room SelectedRoom
        {
            get { return _selectedRoom; }
            set
            {
                _selectedRoom = value;
                OnPropertyChanged("SelectedRoom");
            }
        }
        public List<Room> Selection
        {
            get { return _selection; }
            set
            {
                _selection = value;
                OnPropertyChanged("Selection");
            }
        }
        public House House { get; private set; }

        public RelayCommand FindRoomCommand
        {
            get
            {
                return _findRoomCommand ??
                    (_findRoomCommand = new RelayCommand(obj =>
                    {
                        if (SelectedRoom != null)
                        {
                            RoomPage roomPage = new RoomPage(SelectedRoom);
                            roomPage.Show();
                        }
                        else
                        {
                            MessageBox.Show("Room is not selected");
                        }
                    }));
            }
        }
        public RelayCommand SearchByNumberCommand
        {
            get
            {
                return _searchByNumberCommand ??
                    (_searchByNumberCommand = new RelayCommand(obj =>
                    {
                        string no = RoomNumber?.Trim();
                        if (string.IsNullOrEmpty(no))
                        {
                            Selection = House.Rooms;
                        }
                        else
                        {
                            Selection = House.Rooms.Where(r => r.No.ToString() == no).ToList();
                        }
                        Selection.Sort((r1, r2) => r1.No.CompareTo(r2.No));
                    }));
            }
        }

        public HouseViewModel(House house)
        {
            House = house;
            Selection = House.Rooms;
            Selection.Sort((r1, r2) => r1.No.CompareTo(r2.No));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
