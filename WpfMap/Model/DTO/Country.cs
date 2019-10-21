using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMap.Model.DTO
{
    public class Country
    {
        private static int _idGenerator = 0;
        public int Id { get; private set; }
        public string Name { get; set; }
        public float Area
        {
            get { return Cities.Sum(city => city.Area); }
        }
        public int Population
        {
            get { return Cities.Sum(city => city.Habitants); }
        }
        public City Capital { get; set; }
        public List<City> Cities { get; set; }

        public Country()
        {
            Id = _idGenerator++;
        }

        public override bool Equals(object obj)
        {
            var country = obj as Country;
            return country != null &&
                   Id == country.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }
    }
}
