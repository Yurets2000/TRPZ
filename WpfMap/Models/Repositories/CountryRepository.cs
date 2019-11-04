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
    public class CountryRepository : GenericRepository<Country>
    {
        
        public CountryRepository(MainContext context) : base(context)
        {
        }

        public override void Add(Country entity)
        {
            _context.Countries.Add(entity);
        }

        public override void Delete(Country entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
        }

        public override void Edit(Country entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public override Country GetById(int id)
        {
            return _context.Countries.Find(id);
        }

        public override List<Country> List()
        {
            return _context.Countries.ToList();
        }

        public override List<Country> List(Expression<Func<Country, bool>> predicate)
        {
            return _context.Countries.Where(predicate).ToList();
        }
    }
}
