using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WpfMap.Models.Contexts;

namespace WpfMap.Models.Repositories
{
    public abstract class GenericRepository<T> : IRepository<T>
    {
        protected MainContext _context;

        public GenericRepository(MainContext context)
        {
            _context = context;
        }

        public abstract void Add(T entity);

        public abstract void Delete(T entity);

        public abstract void Edit(T entity);

        public abstract T GetById(int id);

        public abstract List<T> List();

        public abstract List<T> List(Expression<Func<T, bool>> predicate);
    }
}
