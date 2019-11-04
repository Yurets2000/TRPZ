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
    public class RoomRepository : GenericRepository<Room>
    {
        public RoomRepository(MainContext context) : base(context)
        {
        }

        public override void Add(Room entity)
        {
            _context.Rooms.Add(entity);
        }

        public override void Delete(Room entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
        }

        public override void Edit(Room entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public override Room GetById(int id)
        {
            return _context.Rooms.Find(id);
        }

        public override List<Room> List()
        {
            return _context.Rooms.ToList();
        }

        public override List<Room> List(Expression<Func<Room, bool>> predicate)
        {
            return _context.Rooms.Where(predicate).ToList();
        }
    }
}
