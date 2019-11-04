using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WpfMap.Models.Contexts;
using WpfMap.Models.Entities;

namespace WpfMap.Models.Repositories
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(MainContext context) : base(context)
        {
        }

        public override void Add(User entity)
        {
            _context.Users.Add(entity);
        }

        public override void Delete(User entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
        }

        public override void Edit(User entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public override User GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public override List<User> List()
        {
            return _context.Users.ToList();
        }

        public override List<User> List(Expression<Func<User, bool>> predicate)
        {
            return _context.Users.Where(predicate).ToList();
        }
    }
}
