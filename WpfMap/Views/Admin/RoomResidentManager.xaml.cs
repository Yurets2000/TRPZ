using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfMap.Model.Entities;
using WpfMap.Model.Repositories;

namespace WpfMap
{
    public partial class RoomResidentManager : Window
    {
        private ModelRepository _repository = ModelRepository.GetInstance();
        private List<Room> _rooms;
        private List<Resident> _residents;

        public RoomResidentManager()
        {
            InitializeComponent();
            _rooms = _repository.Rooms;
            _residents = _repository.Residents;
        }

        public void AddResident_Click(object sender, EventArgs e)
        {
            Room room = _rooms.Find(r => r.UID == addRoomUID.Text);
            Resident resident = _residents.Find(r => r.UID == addResidentUID.Text);
            if (room != null && resident != null)
            {
                if (room.RoomResidents.Find(r => r.Resident.Equals(resident)) == null) { 
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
        }

        public void DeleteResident_Click(object sender, EventArgs e)
        {
            Room room = _rooms.Find(r => r.UID == deleteRoomUID.Text);
            Resident resident = _residents.Find(r => r.UID == deleteResidentUID.Text);
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
        }
    }
}
