using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMap.Model.DTO
{
    public class Street
    {
        private static int _idGenerator = 0;
        public int Id { get; private set; }
        public string Name { get; set; }
        public int Habitants
        {
            get { return Houses.Sum(house => house.Habitants); }
        }
        public List<House> Houses { get; set; }

        public override bool Equals(object obj)
        {
            var street = obj as Street;
            return street != null &&
                   Id == street.Id;
        }

        public Street()
        {
            Id = _idGenerator++;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }
    }
}
