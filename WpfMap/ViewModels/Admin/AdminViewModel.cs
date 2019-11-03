using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfMap.ViewModels.Admin
{
    public class AdminViewModel : INotifyPropertyChanged
    {
        private string _manager;

        private RelayCommand _selectManagerCommand;
        private RelayCommand _openUserPageCommand;
        private RelayCommand _openLoginPageCommand;

        public event EventHandler ClosingRequest;

        public string Manager
        {
            get { return _manager; }
            set
            {
                _manager = value;
                OnPropertyChanged("Manager");
            }
        }

        public RelayCommand SelectManagerCommand
        {
            get
            {
                return _selectManagerCommand ??
                    (_selectManagerCommand = new RelayCommand(obj => 
                    {
                        switch (Manager)
                        {
                            case "Country Manager":
                                CountryManager countryManager = new CountryManager();
                                countryManager.ShowDialog();
                                break;
                            case "Room-Resident Manager":
                                RoomResidentManager roomManager = new RoomResidentManager();
                                roomManager.ShowDialog();
                                break;
                            case "Resident Manager":
                                ResidentManager residentManager = new ResidentManager();
                                residentManager.ShowDialog();
                                break;
                        }
                    }));
            }
        }

        public RelayCommand OpenUserPageCommand
        {
            get
            {
                return _openUserPageCommand ??
                    (_openUserPageCommand = new RelayCommand(obj =>
                    {
                        UserPage userPage = new UserPage();
                        userPage.Show();
                        ClosingRequest?.Invoke(this, EventArgs.Empty);
                    }));
            }
        }

        public RelayCommand OpenLoginPageCommand
        {
            get
            {
                return _openLoginPageCommand ??
                    (_openLoginPageCommand = new RelayCommand(obj =>
                    {
                        Login login = new Login();
                        login.Show();
                        ClosingRequest?.Invoke(this, EventArgs.Empty);
                    }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
