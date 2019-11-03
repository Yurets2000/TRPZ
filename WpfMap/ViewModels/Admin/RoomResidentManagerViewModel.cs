using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfMap.Model.Entities;
using WpfMap.Model.Repositories;

namespace WpfMap.ViewModels.Admin
{
    public class RoomResidentManagerViewModel : INotifyPropertyChanged
    {
        private string _addResidentUID;
        private string _addRoomUID;
        private string _deleteResidentUID;
        private string _deleteRoomUID;

        private RelayCommand _addResidentCommand;
        private RelayCommand _deleteResidentCommand;

        private ModelRepository _repository = ModelRepository.GetInstance();

        [Required(ErrorMessage = "should not be empty")]
        private string AddResidentUID
        {
            get { return _addResidentUID; }
            set
            {
                ValidateProperty(value, "AddResidentUID");
                _addResidentUID = value;
                OnPropertyChanged("AddResidentUID");
            }
        }
        [Required(ErrorMessage = "should not be empty")]
        private string AddRoomUID
        {
            get { return _addRoomUID; }
            set
            {
                ValidateProperty(value, "AddRoomUID");
                _addRoomUID = value;
                OnPropertyChanged("AddRoomUID");
            }
        }
        [Required(ErrorMessage = "should not be empty")]
        private string DeleteResidentUID
        {
            get { return _deleteResidentUID; }
            set
            {
                ValidateProperty(value, "DeleteResidentUID");
                _deleteResidentUID = value;
                OnPropertyChanged("DeleteResidentUID");
            }
        }
        [Required(ErrorMessage = "should not be empty")]
        private string DeleteRoomUID
        {
            get { return _deleteRoomUID; }
            set
            {
                ValidateProperty(value, "DeleteRoomUID");
                _deleteRoomUID = value;
                OnPropertyChanged("DeleteRoomUID");
            }
        }

        public RelayCommand AddResidentCommand
        {
            get
            {
                return _addResidentCommand ??
                    (_addResidentCommand = new RelayCommand(obj => 
                    {
                        Room room = _repository.Rooms.Find(r => r.UID == AddRoomUID);
                        Resident resident = _repository.Residents.Find(r => r.UID == AddResidentUID);
                        if (room != null && resident != null)
                        {
                            if (room.RoomResidents.Find(r => r.Resident.Equals(resident)) == null)
                            {
                                RoomResident roomResident = new RoomResident
                                {
                                    Room = room,
                                    Resident = resident
                                };
                                room.RoomResidents.Add(roomResident);
                            }
                            else
                            {
                                MessageBox.Show("Room already contains such resident");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Room or Resident not found");
                        }
                    }));
            }
        }
        public RelayCommand DeleteResidentCommand
        {
            get
            {
                return _deleteResidentCommand ??
                    (_deleteResidentCommand = new RelayCommand(obj =>
                    {
                        Room room = _repository.Rooms.Find(r => r.UID == DeleteRoomUID);
                        Resident resident = _repository.Residents.Find(r => r.UID == DeleteResidentUID);
                        if (room != null && resident != null)
                        {
                            if (room.RoomResidents.Find(r => r.Resident.Equals(resident)) != null)
                            {
                                RoomResident roomResident = new RoomResident
                                {
                                    Room = room,
                                    Resident = resident
                                };
                                room.RoomResidents.Remove(roomResident);
                            }
                            else
                            {
                                MessageBox.Show("Room doesn't contains such resident");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Room or Resident not found");
                        }
                    }));
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
