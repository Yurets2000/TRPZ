using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMap.Model.Entities;

namespace WpfMap.Model.Repositories
{
    public class UserRepository
    {
        public HashSet<User> Users { get; private set; }
        private static UserRepository _repository;

        public static UserRepository GetInstance()
        {
            if (_repository == null)
            {
                _repository = new UserRepository();
            }
            return _repository;
        }

        private UserRepository()
        {
            Users = new HashSet<User>
            {         
                new User("admin", "admin", true),
                new User("user", "user", false)
            };
        }

        public User SearchUser(string login, string password)
        {
            return Users.FirstOrDefault(u => u.Login == login && u.Password == password);
        }
    }
}
