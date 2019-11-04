using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfMap.Helpers.Utils;
using WpfMap.Models.Contexts;
using WpfMap.Models.Entities;
using WpfMap.Models.Repositories;

namespace WpfMap.ViewModels.Admin
{
    public class RoomManagerViewModel : INotifyPropertyChanged
    {
        private string _addRoomNo;
        private string _addRoomArea;

        private Room _selectedRoom;
        private ObservableCollection<Room> _rooms;
        private string _editRoomNo;
        private string _editRoomArea;

        private RelayCommand _selectRoomCommand;
        private RelayCommand _addRoomCommand;
        private RelayCommand _editRoomCommand;
        private RelayCommand _deleteRoomCommand;

        public ObservableCollection<Room> Rooms
        {
            get { return _rooms; }
            set
            {
                _rooms = value;
                OnPropertyChanged("Rooms");
            }
        }

        [Required(ErrorMessage = "should not be empty")]
        [RegularExpression(@"^[1-9][0-9]{0,2}$", ErrorMessage = "should be numeric")]
        public string AddRoomNo
        {
            get { return _addRoomNo; }
            set
            {
                ValidateProperty(value, "AddRoomNo");
                _addRoomNo = value;
                OnPropertyChanged("AddRoomNo");
            }
        }
        [Required(ErrorMessage = "should not be empty")]
        [RegularExpression(@"^[1-9][0-9]{0,3}$", ErrorMessage = "should be numeric")]
        public string AddRoomArea
        {
            get { return _addRoomArea; }
            set
            {
                ValidateProperty(value, "AddRoomArea");
                _addRoomArea = value;
                OnPropertyChanged("AddRoomArea");
            }
        }
        [Required(ErrorMessage = "should not be empty")]
        [RegularExpression(@"^[1-9][0-9]{0,2}$", ErrorMessage = "should be numeric")]
        public string EditRoomNo
        {
            get { return _editRoomNo; }
            set
            {
                ValidateProperty(value, "EditRoomNo");
                _editRoomNo = value;
                OnPropertyChanged("EditRoomNo");
            }
        }
        [Required(ErrorMessage = "should not be empty")]
        [RegularExpression(@"^[1-9][0-9]{0,3}$", ErrorMessage = "should be numeric")]
        public string EditRoomArea
        {
            get { return _editRoomArea; }
            set
            {
                ValidateProperty(value, "EditRoomArea");
                _editRoomArea = value;
                OnPropertyChanged("EditRoomArea");
            }
        }
        [Required(ErrorMessage = "should be selected")]
        public Room SelectedRoom
        {
            get { return _selectedRoom; }
            set
            {
                ValidateProperty(value, "SelectedRoom");
                _selectedRoom = value;
                OnPropertyChanged("SelectedRoom");
            }
        }

        public RelayCommand AddRoomCommand
        {
            get
            {
                return _addRoomCommand ??
                    (_addRoomCommand = new RelayCommand(obj =>
                    {
                        using (MainContext context = new MainContext())
                        {
                            var roomRep = new RoomRepository(context);
                            var houseRep = new HouseRepository(context);
                            context.Houses.Attach(House);
                            Room room = new Room
                            {
                                UID = Generator.RandomString(7),
                                No = int.Parse(AddRoomNo),
                                Area = int.Parse(AddRoomArea),
                                House = House
                            };
                            roomRep.Add(room);
                            houseRep.Edit(House);
                            context.SaveChanges();
                            Rooms = new ObservableCollection<Room>(House.Rooms);
                        }
                    }));
            }
        }
        public RelayCommand SelectRoomCommand
        {
            get
            {
                return _selectRoomCommand ??
                    (_selectRoomCommand = new RelayCommand(obj =>
                    {
                        EditRoomNo = SelectedRoom?.No.ToString();
                        EditRoomArea = SelectedRoom?.Area.ToString();
                    }));
            }
        }
        public RelayCommand EditRoomCommand
        {
            get
            {
                return _editRoomCommand ??
                    (_editRoomCommand = new RelayCommand(obj =>
                    {
                        using (MainContext context = new MainContext())
                        {
                            var roomRep = new RoomRepository(context);
                            SelectedRoom.No = int.Parse(EditRoomNo);
                            SelectedRoom.Area = int.Parse(EditRoomArea);
                            roomRep.Edit(SelectedRoom);
                            context.SaveChanges();
                            Rooms = new ObservableCollection<Room>(House.Rooms);
                            SelectedRoom = SelectedRoom;
                        }
                    }));
            }
        }
        public RelayCommand DeleteRoomCommand
        {
            get
            {
                return _deleteRoomCommand ??
                    (_deleteRoomCommand = new RelayCommand(obj =>
                    {
                        using (MainContext context = new MainContext())
                        {
                            var roomRep = new RoomRepository(context);
                            var houseRep = new HouseRepository(context);
                            context.Houses.Attach(House);
                            roomRep.Delete(SelectedRoom);
                            houseRep.Edit(House);
                            context.SaveChanges();
                            Rooms = new ObservableCollection<Room>(House.Rooms);
                        }
                    }));
            }
        }

        public House House { get; private set; }

        private void ValidateProperty<T>(T value, string name)
        {
            Validator.ValidateProperty(value, new ValidationContext(this, null, null)
            {
                MemberName = name
            });
        }

        public RoomManagerViewModel(House house)
        {
            using (MainContext context = new MainContext())
            {
                House = house;
                context.Houses.Attach(House);
                Rooms = new ObservableCollection<Room>(House.Rooms);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
