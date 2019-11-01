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
using WpfMap.Helpers.Validators;
using WpfMap.Model;
using WpfMap.Model.DTO;

namespace WpfMap
{
    public partial class RoomManager : Window
    {
        private House _house;
        private RoomManagerValidator _validator = new RoomManagerValidator();

        public RoomManager(House house)
        {
            InitializeComponent();
            _house = house;
            this.DataContext = _house;
        }

        public void AddRoom_Click(object sender, EventArgs e)
        {
            if (_validator.IsDataValidForAddition(this))
            {
                Room room = new Room
                {
                    No = int.Parse(addRoomNo.Text),
                    Area = int.Parse(addRoomArea.Text),
                    RoomResidents = new List<RoomResident>()
                };
                _house.Rooms.Add(room);
                Utils.RefreshComboBox(roomSelector);
            }
            else
            {
                MessageBox.Show("Some fields are empty or entered data is not valid");
            }
        }

        public void EditRoom_Click(object sender, EventArgs e)
        {
            Room room = (Room)roomSelector.SelectedItem;
            if (room != null)
            {
                if (_validator.IsDataValidForEdition(this))
                {
                    room.No = int.Parse(editRoomNo.Text);
                    room.Area = int.Parse(editRoomArea.Text);
                    Utils.RefreshComboBox(roomSelector);
                }
                else
                {
                    MessageBox.Show("Some fields are empty or entered data is not valid");
                }
            }
            else
            {
                MessageBox.Show("Room is not selected");
            }
        }

        public void DeleteRoom_Click(object sender, EventArgs e)
        {
            Room room = (Room)roomSelector.SelectedItem;
            if (room != null)
            {
                _house.Rooms.Remove(room);
                Utils.RefreshComboBox(roomSelector);
            }
            else
            {
                MessageBox.Show("Room is not selected");
            }
        }

        /*
        public void OpenResidentManager_Click(object sender, EventArgs e)
        {
            Room room = (Room)roomSelector.SelectedItem;
            if (room != null)
            {
                ResidentManager residentManager = new ResidentManager(room);
                residentManager.ShowDialog();
            }
            else
            {
                MessageBox.Show("Room is not selected");
            }
        }
        */

        public void RoomSelector_SelectionChanged(object sender, EventArgs e)
        {
            Room room = (Room)roomSelector.SelectedItem;
            editRoomNo.Text = room?.No.ToString();
            editRoomArea.Text = room?.Area.ToString();
        }
    }
}
