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
    public class HouseRepository : GenericRepository<House>
    {
        public HouseRepository(MainContext context) : base(context)
        {
        }

        public override void Add(House entity)
        {
            _context.Houses.Add(entity);
        }

        public override void Delete(House entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
        }

        public override void Edit(House entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public override House GetById(int id)
        {
            return _context.Houses.Find(id);
        }

        public override List<House> List()
        {
            return _context.Houses.ToList();
        }

        public override List<House> List(Expression<Func<House, bool>> predicate)
        {
            return _context.Houses.Where(predicate).ToList();
        }
    }
}
