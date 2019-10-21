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
    public partial class RoomPage : Window
    {
        private Room _room;
        private List<Resident> _selection;

        public RoomPage(Room room)
        {
            this.InitializeComponent();
            this.DataContext = _room = room;
            _selection = _room.Residents;
            _selection.Sort((r1, r2) => r1.Name.CompareTo(r2.Name));
            residents.ItemsSource = _selection;
        }

        private void FindResident_Click(object sender, EventArgs e)
        {
            Resident resident = (Resident)residents.SelectedValue;
            if (resident != null)
            {
                ResidentPage residentPage = new ResidentPage(resident);
                residentPage.Show();
            }
            else
            {
                MessageBox.Show("Resident is not selected");
            }
        }

        public void SearchByName_Click(object sender, EventArgs e)
        {
            string name = residentName.Text?.Trim();
            if (string.IsNullOrEmpty(name))
            {
                _selection = _room.Residents;
            }
            else
            {
                _selection = _room.Residents.Where(r => r.Name.Contains(name)).ToList();
            }
            _selection.Sort((r1, r2) => r1.Name.CompareTo(r2.Name));
            residents.ItemsSource = _selection;
        }
    }
}
