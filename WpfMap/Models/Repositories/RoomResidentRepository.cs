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
    public class RoomResidentRepository : GenericRepository<RoomResident>
    {
        public RoomResidentRepository(MainContext context) : base(context)
        {
        }

        public override void Add(RoomResident entity)
        {
            _context.RoomResidents.Add(entity);
        }

        public override void Delete(RoomResident entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
        }

        public override void Edit(RoomResident entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public override RoomResident GetById(int id)
        {
            return _context.RoomResidents.Find(id);
        }

        public override List<RoomResident> List()
        {
            return _context.RoomResidents.ToList();
        }

        public override List<RoomResident> List(Expression<Func<RoomResident, bool>> predicate)
        {
            return _context.RoomResidents.Where(predicate).ToList();
        }
    }
}
