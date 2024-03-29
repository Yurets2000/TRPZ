﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfMap.Models.Contexts;
using WpfMap.Models.Entities;
using WpfMap.Models.Repositories;

namespace WpfMap.ViewModels.Admin
{
    public class StreetManagerViewModel : INotifyPropertyChanged
    {
        private string _addStreetName;

        private Street _selectedStreet;
        private ObservableCollection<Street> _streets;
        private string _editStreetName;

        private RelayCommand _selectStreetCommand;
        private RelayCommand _addStreetCommand;
        private RelayCommand _editStreetCommand;
        private RelayCommand _deleteStreetCommand;
        private RelayCommand _openHouseManagerCommand;

        public ObservableCollection<Street> Streets
        {
            get { return _streets; }
            set
            {
                ValidateProperty(value, "Streets");
                _streets = value;
                OnPropertyChanged("Streets");
            }
        }

        [Required(ErrorMessage = "should not be empty")]
        public string AddStreetName
        {
            get { return _addStreetName; }
            set
            {
                ValidateProperty(value, "AddStreetName");
                _addStreetName = value;
                OnPropertyChanged("AddStreetName");
            }
        }
        [Required(ErrorMessage = "should not be empty")]
        public string EditStreetName
        {
            get { return _editStreetName; }
            set
            {
                ValidateProperty(value, "EditStreetName");
                _editStreetName = value;
                OnPropertyChanged("EditStreetName");
            }
        }
        [Required(ErrorMessage = "should be selected")]
        public Street SelectedStreet
        {
            get { return _selectedStreet; }
            set
            {
                ValidateProperty(value, "SelectedStreet");
                _selectedStreet = value;
                OnPropertyChanged("SelectedStreet");
            }
        }

        public RelayCommand AddStreetCommand
        {
            get
            {
                return _addStreetCommand ??
                    (_addStreetCommand = new RelayCommand(obj =>
                    {
                        using (MainContext context = new MainContext())
                        {
                            var streetRep = new StreetRepository(context);
                            var cityRep = new CityRepository(context);
                            context.Cities.Attach(City);
                            Street street = new Street
                            {
                                Name = AddStreetName,
                                City = City
                            };
                            streetRep.Add(street);
                            cityRep.Edit(City);
                            context.SaveChanges();
                            Streets = new ObservableCollection<Street>(City.Streets);
                        }
                    }));
            }
        }
        public RelayCommand SelectStreetCommand
        {
            get
            {
                return _selectStreetCommand ??
                    (_selectStreetCommand = new RelayCommand(obj =>
                    {
                        EditStreetName = SelectedStreet?.Name;
                    }));
            }
        }
        public RelayCommand EditStreetCommand
        {
            get
            {
                return _editStreetCommand ??
                    (_editStreetCommand = new RelayCommand(obj =>
                    {
                        using (MainContext context = new MainContext())
                        {
                            var streetRep = new StreetRepository(context);
                            SelectedStreet.Name = EditStreetName;
                            streetRep.Edit(SelectedStreet);
                            context.SaveChanges();
                            Streets = new ObservableCollection<Street>(City.Streets);
                            SelectedStreet = SelectedStreet;
                        }
                    }));
            }
        }
        public RelayCommand DeleteStreetCommand
        {
            get
            {
                return _deleteStreetCommand ??
                    (_deleteStreetCommand = new RelayCommand(obj =>
                    {
                        using (MainContext context = new MainContext())
                        {
                            var streetRep = new StreetRepository(context);
                            var cityRep = new CityRepository(context);
                            context.Cities.Attach(City);
                            streetRep.Delete(SelectedStreet);
                            cityRep.Edit(City);
                            context.SaveChanges();
                            Streets = new ObservableCollection<Street>(City.Streets);
                        }
                    }));
            }
        }
        public RelayCommand OpenHouseManagerCommand
        {
            get
            {
                return _openHouseManagerCommand ??
                    (_openHouseManagerCommand = new RelayCommand(obj =>
                    {
                        HouseManager houseManager = new HouseManager(SelectedStreet);
                        houseManager.ShowDialog();
                    }));
            }
        }

        public City City { get; private set; }

        private void ValidateProperty<T>(T value, string name)
        {
            Validator.ValidateProperty(value, new ValidationContext(this, null, null)
            {
                MemberName = name
            });
        }

        public StreetManagerViewModel(City city)
        {
            using (MainContext context = new MainContext())
            {
                City = city;
                context.Cities.Attach(City);
                Streets = new ObservableCollection<Street>(City.Streets);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
