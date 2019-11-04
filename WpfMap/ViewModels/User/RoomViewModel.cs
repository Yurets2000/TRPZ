using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfMap.Models.Contexts;
using WpfMap.Models.Entities;
using WpfMap.Models.Repositories;

namespace WpfMap.ViewModels.User
{
    public class RoomViewModel : INotifyPropertyChanged
    {
        private string _residentName;
        private Resident _selectedResident;
        private List<Resident> _selection;

        private RelayCommand _findResidentCommand;
        private RelayCommand _searchByNameCommand;

        public string ResidentName
        {
            get { return _residentName; }
            set
            {
                _residentName = value;
                OnPropertyChanged("ResidentName");
            }
        }
        public Resident SelectedResident
        {
            get { return _selectedResident; }
            set
            {
                _selectedResident = value;
                OnPropertyChanged("SelectedResident");
            }
        }
        public List<Resident> Selection
        {
            get { return _selection; }
            set
            {
                _selection = value;
                OnPropertyChanged("Selection");
            }
        }
        public Room Room { get; private set; }

        public RelayCommand FindResidentCommand
        {
            get
            {
                return _findResidentCommand ??
                    (_findResidentCommand = new RelayCommand(obj =>
                    {
                        if (SelectedResident != null)
                        {
                            ResidentPage residentPage = new ResidentPage(SelectedResident);
                            residentPage.Show();
                        }
                        else
                        {
                            MessageBox.Show("Resident is not selected");
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
                        using (MainContext context = new MainContext())
                        {
                            var roomResidentRep = new RoomResidentRepository(context);
                            string name = ResidentName?.Trim();
                            if (string.IsNullOrEmpty(name))
                            {
                                Selection = context.RoomResidents.Include("Room").Include("Resident")
                                .Where(rr => rr.RoomId == Room.Id).Select(r => r.Resident).ToList();
                            }
                            else
                            {
                                Selection = context.RoomResidents.Include("Room").Include("Resident")
                                .Where(rr => rr.RoomId == Room.Id).Select(r => r.Resident)
                                .Where(s => s.Name.Contains(name)).ToList();
                            }
                            Selection.Sort((r1, r2) => r1.Name.CompareTo(r2.Name));
                        }
                    }));
            }
        }

        public RoomViewModel(Room room)
        {
            using (MainContext context = new MainContext())
            {
                var roomResidentRep = new RoomResidentRepository(context);
                Room = room;
                Selection = context.RoomResidents.Include("Room").Include("Resident")
                .Where(rr => rr.RoomId == Room.Id).Select(r => r.Resident).ToList();
                Selection.Sort((r1, r2) => r1.Name.CompareTo(r2.Name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
