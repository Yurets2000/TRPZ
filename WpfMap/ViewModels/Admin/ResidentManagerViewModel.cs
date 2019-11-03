using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfMap.Model.Entities;
using WpfMap.Model.Repositories;

namespace WpfMap.ViewModels.Admin
{
    public class ResidentManagerViewModel : INotifyPropertyChanged
    {
        private string _addResidentName;
        private string _addResidentAge;
        private Resident.Sex? _addResidentGender;
        private string _addResidentPhone;
        private string _addResidentResidenceTime;

        private string _residentUID;
        private Resident _selectedResident;
        private ObservableCollection<Resident> _residents;
        private string _editResidentName;
        private string _editResidentAge;
        private Resident.Sex? _editResidentGender;
        private string _editResidentPhone;
        private string _editResidentResidenceTime;

        private RelayCommand _selectResidentCommand;
        private RelayCommand _addResidentCommand;
        private RelayCommand _editResidentCommand;
        private RelayCommand _deleteResidentCommand;

        private ModelRepository _repository = ModelRepository.GetInstance();
        public IEnumerable<Resident.Sex> Genders
        {
            get
            {
                return Enum.GetValues(typeof(Resident.Sex)).Cast<Resident.Sex>();
            }
        }
        public ObservableCollection<Resident> Residents
        {
            get { return _residents; }
            set
            {
                _residents = value;
                OnPropertyChanged("Residents");
            }
        }

        [Required(ErrorMessage = "should not be empty")]
        public string AddResidentName
        {
            get { return _addResidentName; }
            set
            {
                ValidateProperty(value, "AddResidentName");
                _addResidentName = value;
                OnPropertyChanged("AddResidentName");
            }
        }
        [Required(ErrorMessage = "should not be empty")]
        [RegularExpression(@"^[1-9][0-9]{0,2}$", ErrorMessage = "should be numeric")]
        public string AddResidentAge
        {
            get { return _addResidentAge; }
            set
            {
                ValidateProperty(value, "AddResidentAge");
                _addResidentAge = value;
                OnPropertyChanged("AddResidentAge");
            }
        }
        [Required(ErrorMessage = "should be selected")]
        public Resident.Sex? AddResidentGender
        {
            get { return _addResidentGender; }
            set
            {
                ValidateProperty(value, "AddResidentGender");
                _addResidentGender = value;
                OnPropertyChanged("AddResidentGender");
            }
        }
        [Required(ErrorMessage = "should not be empty")]
        [Phone(ErrorMessage = "should be phone number")]
        public string AddResidentPhone
        {
            get { return _addResidentPhone; }
            set
            {
                ValidateProperty(value, "AddResidentPhone");
                _addResidentPhone = value;
                OnPropertyChanged("AddResidentPhone");
            }
        }
        [Required(ErrorMessage = "should not be empty")]
        [RegularExpression(@"^[1-9][0-9]{0,2}$", ErrorMessage = "should be numeric")]
        public string AddResidentResidenceTime
        {
            get { return _addResidentResidenceTime; }
            set
            {
                ValidateProperty(value, "AddResidentResidenceTime");
                _addResidentResidenceTime = value;
                OnPropertyChanged("AddResidentResidenceTime");
            }
        }
        [Required(ErrorMessage = "should not be empty")]
        public string ResidentUID
        {
            get { return _residentUID; }
            set
            {
                ValidateProperty(value, "ResidentUID");
                _residentUID = value;
                OnPropertyChanged("ResidentUID");
            }
        }
        [Required(ErrorMessage = "should not be empty")]
        public string EditResidentName
        {
            get { return _editResidentName; }
            set
            {
                ValidateProperty(value, "EditResidentName");
                _editResidentName = value;
                OnPropertyChanged("EditResidentName");
            }
        }
        [Required(ErrorMessage = "should not be empty")]
        [RegularExpression(@"^[1-9][0-9]{0,2}$", ErrorMessage = "should be numeric")]
        public string EditResidentAge
        {
            get { return _editResidentAge; }
            set
            {
                ValidateProperty(value, "EditResidentAge");
                _editResidentAge = value;
                OnPropertyChanged("EditResidentAge");
            }
        }
        [Required(ErrorMessage = "should be selected")]
        public Resident.Sex? EditResidentGender
        {
            get { return _editResidentGender; }
            set
            {
                ValidateProperty(value, "EditResidentGender");
                _editResidentGender = value;
                OnPropertyChanged("EditResidentGender");
            }
        }
        [Required(ErrorMessage = "should not be empty")]
        [Phone(ErrorMessage = "should be phone number")]
        public string EditResidentPhone
        {
            get { return _editResidentPhone; }
            set
            {
                ValidateProperty(value, "EditResidentPhone");
                _editResidentPhone = value;
                OnPropertyChanged("EditResidentPhone");
            }
        }
        [Required(ErrorMessage = "should not be empty")]
        [RegularExpression(@"^[1-9][0-9]{0,2}$", ErrorMessage = "should be numeric")]
        public string EditResidentResidenceTime
        {
            get { return _editResidentResidenceTime; }
            set
            {
                ValidateProperty(value, "EditResidentResidenceTime");
                _editResidentResidenceTime = value;
                OnPropertyChanged("EditResidentResidenceTime");
            }
        }
        [Required(ErrorMessage = "should be selected")]
        public Resident SelectedResident
        {
            get { return _selectedResident; }
            set
            {
                ValidateProperty(value, "SelectedResident");
                _selectedResident = value;
                OnPropertyChanged("SelectedResident");
            }
        }

        public RelayCommand AddResidentCommand
        {
            get
            {
                return _addResidentCommand ??
                    (_addResidentCommand = new RelayCommand(obj =>
                    {
                        Resident resident = new Resident
                        {
                            Name = AddResidentName,
                            Age = int.Parse(AddResidentAge),
                            Gender = AddResidentGender,
                            Phone = AddResidentPhone,
                            ResidenceTime = int.Parse(AddResidentResidenceTime)
                        };
                        _repository.Residents.Add(resident);
                        Residents = new ObservableCollection<Resident>(_repository.Residents);
                    }));
            }
        }
        public RelayCommand SelectResidentCommand
        {
            get
            {
                return _selectResidentCommand ??
                    (_selectResidentCommand = new RelayCommand(obj =>
                    {
                        SelectedResident = _repository.Residents.Find(r => r.UID == ResidentUID);                   
                        EditResidentName = SelectedResident?.Name;
                        EditResidentAge = SelectedResident?.Age.ToString();
                        EditResidentGender = SelectedResident?.Gender;
                        EditResidentPhone = SelectedResident?.Phone;
                        EditResidentResidenceTime = SelectedResident?.ResidenceTime.ToString();
                    }));
            }
        }
        public RelayCommand EditResidentCommand
        {
            get
            {
                return _editResidentCommand ??
                    (_editResidentCommand = new RelayCommand(obj =>
                    {
                        SelectedResident.Name = EditResidentName;
                        SelectedResident.Age = int.Parse(EditResidentAge);
                        SelectedResident.Gender = EditResidentGender;
                        SelectedResident.Phone = EditResidentPhone;
                        SelectedResident.ResidenceTime = int.Parse(EditResidentResidenceTime);
                        Residents = new ObservableCollection<Resident>(_repository.Residents);
                        SelectedResident = SelectedResident;
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
                        _repository.Residents.Remove(SelectedResident);
                        Residents = new ObservableCollection<Resident>(_repository.Residents);
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

        public ResidentManagerViewModel()
        {
            Residents = new ObservableCollection<Resident>(_repository.Residents);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
