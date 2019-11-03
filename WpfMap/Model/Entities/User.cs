using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMap.Model.Entities
{
    public class User
    {
        private static int _idGenerator = 0;
        public int Id { get; private set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool Admin { get; set; }

        public User(string login, string password, bool admin)
        {
            Id = _idGenerator++;
            Login = login;
            Password = password;
            Admin = admin;
        }

        public override bool Equals(object obj)
        {
            var user = obj as User;
            return user != null &&
                   Id == user.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }
    }
}
