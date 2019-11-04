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
    public class StreetRepository : GenericRepository<Street>
    {
        public StreetRepository(MainContext context) : base(context)
        {
        }

        public override void Add(Street entity)
        {
            _context.Streets.Add(entity);
        }

        public override void Delete(Street entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
        }

        public override void Edit(Street entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public override Street GetById(int id)
        {
            return _context.Streets.Find(id);
        }

        public override List<Street> List()
        {
            return _context.Streets.ToList();
        }

        public override List<Street> List(Expression<Func<Street, bool>> predicate)
        {
            return _context.Streets.Where(predicate).ToList();
        }
    }
}
