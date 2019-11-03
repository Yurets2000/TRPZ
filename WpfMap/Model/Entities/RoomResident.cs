using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMap.Model.Entities
{
    public class RoomResident
    {
        private static int _idGenerator = 0;
        public int Id { get; private set; }
        public Room Room { get; set; }
        public Resident Resident { get; set; }

        public RoomResident()
        {
            Id = _idGenerator++;
        }
    }
}
