using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMap.Models.Entities
{
    public class House
    {
        public int Id { get; set; }
        public int StreetId { get; set; }
        public Street Street { get; set; }
        public string Address { get; set; }
        public int Habitants
        {
            get { return Rooms.Sum(room => room.RoomResidents.Count); }
        }
        public virtual List<Room> Rooms { get; set; }

        public House(){}

        public override bool Equals(object obj)
        {
            var house = obj as House;
            return house != null &&
                   Id == house.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }
    }
}
