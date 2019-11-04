using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WpfMap.Models.Entities;

namespace WpfMap.Models.Repositories
{
    public interface IRepository<T>
    {
        T GetById(int id);
        List<T> List();
        List<T> List(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Delete(T entity);
        void Edit(T entity);
    }
}
