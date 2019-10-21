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
    public partial class ResidentManager : Window
    {
        private Room _room;

        public ResidentManager(Room room)
        {
            InitializeComponent();
            _room = room;
            this.DataContext = _room;
            addResidentGender.ItemsSource =
            editResidentGender.ItemsSource = Enum.GetValues(typeof(Resident.Sex)).Cast<Resident.Sex>();         
        }

        public void AddResident_Click(object sender, EventArgs e)
        {
            if (IsDataValidForAddition())
            {
                Resident resident = new Resident
                {
                    Name = addResidentName.Text,
                    Age = int.Parse(addResidentAge.Text),
                    Gender = (Resident.Sex)addResidentGender.SelectedItem,
                    Phone = addResidentPhone.Text,
                    ResidenceTime = int.Parse(addResidentTime.Text)
                };
                _room.Residents.Add(resident);
                Utils.RefreshComboBox(residentSelector);
            }
            else
            {
                MessageBox.Show("Some fields are empty or entered data is not valid");
            }
        }

        public void EditResident_Click(object sender, EventArgs e)
        {
            Resident resident = (Resident)residentSelector.SelectedItem;
            if (resident != null)
            {
                if (IsDataValidForEdition())
                {
                    resident.Name = editResidentName.Text;
                    resident.Age = int.Parse(editResidentAge.Text);
                    resident.Gender = (Resident.Sex)editResidentGender.SelectedItem;
                    resident.Phone = editResidentPhone.Text;
                    resident.ResidenceTime = int.Parse(editResidentTime.Text);
                    Utils.RefreshComboBox(residentSelector);
                }
                else
                {
                    MessageBox.Show("Some fields are empty or entered data is not valid");
                }
            }
            else
            {
                MessageBox.Show("Resident is not selected");
            }
        }

        public void DeleteResident_Click(object sender, EventArgs e)
        {
            Resident resident = (Resident)residentSelector.SelectedItem;
            if (resident != null)
            {
                _room.Residents.Remove(resident);
                Utils.RefreshComboBox(residentSelector);
            }
            else
            {
                MessageBox.Show("Resident is not selected");
            }
        }

        public void ResidentSelector_SelectionChanged(object sender, EventArgs e)
        {
            Resident resident = (Resident)residentSelector.SelectedItem;
            editResidentName.Text = resident?.Name;
            editResidentAge.Text = resident?.Age.ToString();
            editResidentGender.SelectedItem = resident?.Gender;
            editResidentPhone.Text = resident?.Phone;
            editResidentTime.Text = resident?.ResidenceTime.ToString();
        }

        public bool IsDataValidForAddition()
        {
            return !(string.IsNullOrEmpty(addResidentName.Text)||
                     string.IsNullOrEmpty(addResidentAge.Text) ||
                     !int.TryParse(addResidentAge.Text, out int n) ||
                     string.IsNullOrEmpty(addResidentGender.Text) ||
                     string.IsNullOrEmpty(addResidentPhone.Text) ||
                     string.IsNullOrEmpty(addResidentTime.Text) ||
                     !int.TryParse(addResidentTime.Text, out int n2));
        }

        public bool IsDataValidForEdition()
        {
            return !(string.IsNullOrEmpty(editResidentName.Text) ||
                     string.IsNullOrEmpty(editResidentAge.Text) ||
                     !int.TryParse(editResidentAge.Text, out int n) ||
                     string.IsNullOrEmpty(editResidentGender.Text) ||
                     string.IsNullOrEmpty(editResidentPhone.Text) ||
                     string.IsNullOrEmpty(editResidentTime.Text) ||
                     !int.TryParse(editResidentTime.Text, out int n2));
        }
    }
}
