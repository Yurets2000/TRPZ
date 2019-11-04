using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMap.Models.Entities
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Area
        {
            get { return Cities.Sum(city => city.Area); }
        }
        public int Population
        {
            get { return Cities.Sum(city => city.Habitants); }
        }
        public virtual List<City> Cities { get; set; }

        public Country(){}

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
