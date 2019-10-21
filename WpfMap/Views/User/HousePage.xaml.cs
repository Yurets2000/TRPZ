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
using WpfMap.Model.DTO;

namespace WpfMap
{
    public partial class HousePage : Window
    {
        private House _house;
        private List<Room> _selection;

        public HousePage(House house)
        {
            this.InitializeComponent();
            this.DataContext = _house = house;
            _selection = _house.Rooms;
            _selection.Sort((r1, r2) => r1.No.CompareTo(r2.No));
            rooms.ItemsSource = _selection;
        }

        private void FindRoom_Click(object sender, EventArgs e)
        {
            Room room = (Room)rooms.SelectedValue;
            if (room != null)
            {
                RoomPage roomPage = new RoomPage(room);
                roomPage.Show();
            }
            else
            {
                MessageBox.Show("Room is not selected");
            }
        }

        public void SearchByNo_Click(object sender, EventArgs e)
        {
            string no = roomNumber.Text?.Trim();
            if (string.IsNullOrEmpty(no))
            {
                _selection = _house.Rooms;
            }
            else
            {
                _selection = _house.Rooms.Where(r => r.No.ToString() == no).ToList();
            }
            _selection.Sort((r1, r2) => r1.No.CompareTo(r2.No));
            rooms.ItemsSource = _selection;
        }
    }
}
