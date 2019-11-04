using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfMap.Models.Contexts;
using WpfMap.Models.Repositories;

namespace WpfMap.ViewModels.Common
{
    public class RegistrationViewModel : INotifyPropertyChanged
    {
        private string _username;
        private string _password;

        private RelayCommand _registerCommand;
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

        public RelayCommand RegisterCommand
        {
            get
            {
                return _registerCommand ??
                    (_registerCommand = new RelayCommand(obj =>
                    {
                        using (MainContext context = new MainContext())
                        {
                            var userRepository = new UserRepository(context);
                            Models.Entities.User user = userRepository.List(u => (u.Login == Username && u.Password == Password)).FirstOrDefault();
                            if (user == null)
                            {
                                Models.Entities.User newUser = new Models.Entities.User
                                {
                                    Login = Username,
                                    Password = Password,
                                    Admin = false
                                };
                                userRepository.Add(newUser);
                                context.SaveChanges();
                                ClosingRequest?.Invoke(this, EventArgs.Empty);
                            }
                            else
                            {
                                MessageBox.Show("User with such login and password already registered");
                            }
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
