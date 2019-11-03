using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMap.Helpers.Utils;

namespace WpfMap.Model.Entities
{
    public class Room
    {
        private static int _idGenerator = 0;
        public int Id { get; private set; }
        public string UID { get; private set; }
        public int Area { get; set; }
        public int No { get; set; }
        public List<RoomResident> RoomResidents { get; set; }

        public Room()
        {
            Id = _idGenerator++;
            UID = Generator.RandomString(7);
        }

        public override bool Equals(object obj)
        {
            var room = obj as Room;
            return room != null &&
                   Id == room.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }
    }
}
