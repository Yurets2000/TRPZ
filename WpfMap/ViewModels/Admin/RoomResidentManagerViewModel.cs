using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfMap.Models.Contexts;
using WpfMap.Models.Entities;
using WpfMap.Models.Repositories;

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

        [Required(ErrorMessage = "should not be empty")]
        public string AddResidentUID
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
        public string AddRoomUID
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
        public string DeleteResidentUID
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
        public string DeleteRoomUID
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
                        using (MainContext context = new MainContext())
                        {
                            var roomResidentRep = new RoomResidentRepository(context);
                            var roomRep = new RoomRepository(context);
                            var residentRep = new ResidentRepository(context);
                            Room room = roomRep.List(r => r.UID == DeleteRoomUID).FirstOrDefault();
                            Resident resident = residentRep.List(r => r.UID == DeleteResidentUID).FirstOrDefault();
                            if (room != null && resident != null)
                            {
                                if (room.RoomResidents.Find(r => r.Resident.Equals(resident)) == null)
                                {
                                    RoomResident roomResident = new RoomResident
                                    {
                                        Room = room,
                                        Resident = resident
                                    };
                                    roomResidentRep.Add(roomResident);
                                    roomRep.Edit(room);
                                    residentRep.Edit(resident);
                                    context.SaveChanges();
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
                        using (MainContext context = new MainContext())
                        {
                            var roomResidentRep = new RoomResidentRepository(context);
                            var roomRep = new RoomRepository(context);
                            var residentRep = new ResidentRepository(context);
                            Room room = roomRep.List(r => r.UID == DeleteRoomUID).FirstOrDefault();
                            Resident resident = residentRep.List(r => r.UID == DeleteResidentUID).FirstOrDefault();
                            if (room != null && resident != null)
                            {
                                RoomResident roomResident = room.RoomResidents.Find(r => r.Resident.Equals(resident));
                                if (roomResident != null)
                                {
                                    roomResidentRep.Delete(roomResident);
                                    roomRep.Edit(room);
                                    residentRep.Edit(resident);
                                    context.SaveChanges();
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
