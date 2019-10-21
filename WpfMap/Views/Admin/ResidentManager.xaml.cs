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
    public partial class ResidentManager : Window
    {
        private Room _room;
        private ResidentManagerValidator _validator = new ResidentManagerValidator();

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
            if (_validator.IsDataValidForAddition(this))
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
                if (_validator.IsDataValidForEdition(this))
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
    }
}
