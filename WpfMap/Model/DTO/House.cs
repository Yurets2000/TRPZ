using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMap.Model.DTO
{
    public class House
    {
        private static int _idGenerator = 0;
        public int Id { get; private set; }
        public string Address { get; set; }
        public int Habitants
        {
            get { return Rooms.Sum(room => room.RoomResidents.Count); }
        }
        public List<Room> Rooms { get; set; }

        public House()
        {
            Id = _idGenerator++;
        }

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
