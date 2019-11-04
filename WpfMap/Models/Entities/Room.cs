using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMap.Helpers.Utils;

namespace WpfMap.Models.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public int HouseId { get; set; }
        public House House { get; set; }
        public string UID { get; set; }
        public int Area { get; set; }
        public int No { get; set; }
        public virtual List<RoomResident> RoomResidents { get; set; }

        public Room(){}

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
