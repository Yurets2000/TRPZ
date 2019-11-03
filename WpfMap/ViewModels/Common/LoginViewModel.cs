using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfMap.Model.Repositories;
using WpfMap.Views.Common;
using WpfMap.Model.Entities;
using System.Windows;
using WpfMap.Model;
using System.Windows.Controls;

namespace WpfMap.ViewModels.Common
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _username;
        private string _password;

        private RelayCommand _openRegistrationPageCommand;
        private RelayCommand _loginCommand;
        private RelayCommand _passwordChangedCommand;

        public event EventHandler ClosingRequest;

        [Required(ErrorMessage = "should not be empty")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "length should be from 3 to 20 characters")]
        public string Username
        {
            get { return _username; }
            set
            {
                ValidateProperty(value, "Username");
                _username = value;
                OnPropertyChanged("Username");
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }

        public RelayCommand OpenRegistrationPageCommand
        {
            get
            {
                return _openRegistrationPageCommand ??
                    ( _openRegistrationPageCommand = new RelayCommand(obj =>
                    {
                        Registration registration = new Registration();
                        registration.Show();
                    }));
            }
        }
        public RelayCommand LoginCommand
        {
            get
            {
                return _loginCommand ??
                    (_loginCommand = new RelayCommand(obj =>
                    {
                        Model.Entities.User user = UserRepository.GetInstance().SearchUser(Username, Password);
                        if (user == null)
                        {
                            MessageBox.Show("There is no such user!");
                        }
                        else
                        {
                            ApplicationContext.CurrentUser = user;
                            if (user.Admin)
                            {
                                AdminPage adminPage = new AdminPage();
                                adminPage.Show();
                            }
                            else
                            {
                                UserPage userPage = new UserPage();
                                userPage.Show();
                            }
                            ClosingRequest?.Invoke(this, EventArgs.Empty);
                        }
                    }));
            }
        }
        public RelayCommand PasswordChangedCommand
        {
            get
            {
                return _passwordChangedCommand ??
                    (_passwordChangedCommand = new RelayCommand(obj =>
                    {
                        PasswordBox passwordBox = obj as PasswordBox;
                        Password = passwordBox.Password;
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
