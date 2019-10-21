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
using WpfMap.Model;
using WpfMap.Model.DTO;

namespace WpfMap
{
    public partial class HouseManager : Window
    {
        private Street _street;

        public HouseManager(Street street)
        {
            InitializeComponent();
            _street = street;
            this.DataContext = _street;
        }

        public void AddHouse_Click(object sender, EventArgs e)
        {
            if (IsDataValidForAddition())
            {
                House house = new House
                {
                    Address = addHouseAddress.Text,
                    Rooms = new List<Room>()
                };
                _street.Houses.Add(house);
                Utils.RefreshComboBox(houseSelector);
            }
            else
            {
                MessageBox.Show("Some fields are empty or entered data is not valid");
            }
        }

        public void EditHouse_Click(object sender, EventArgs e)
        {
            House house = (House)houseSelector.SelectedItem;
            if (house != null)
            {
                if (IsDataValidForEdition())
                {
                    house.Address = editHouseAddress.Text;
                    Utils.RefreshComboBox(houseSelector);
                }
                else
                {
                    MessageBox.Show("Some fields are empty or entered data is not valid");
                }
            }
            else
            {
                MessageBox.Show("House is not selected");
            }
        }

        public void DeleteHouse_Click(object sender, EventArgs e)
        {
            House house = (House)houseSelector.SelectedItem;
            if (house != null)
            {
                _street.Houses.Remove(house);
                Utils.RefreshComboBox(houseSelector);
            }
            else
            {
                MessageBox.Show("House is not selected");
            }
        }

        public void OpenRoomManager_Click(object sender, EventArgs e)
        {
            House house = (House)houseSelector.SelectedItem;
            if (house != null)
            {
                RoomManager roomManager = new RoomManager(house);
                roomManager.ShowDialog();
            }
            else
            {
                MessageBox.Show("House is not selected");
            }
        }

        public void HouseSelector_SelectionChanged(object sender, EventArgs e)
        {
            House house = (House)houseSelector.SelectedItem;
            editHouseAddress.Text = house?.Address;
        }

        public bool IsDataValidForAddition()
        {
            return !string.IsNullOrEmpty(addHouseAddress.Text);
        }

        public bool IsDataValidForEdition()
        {
            return !string.IsNullOrEmpty(editHouseAddress.Text);
        }
    }
}
