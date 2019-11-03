using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMap.Model.Entities
{
    public class City
    {
        private static int _idGenerator = 0;
        public int Id { get; private set; }
        public string Name { get; set; }
        public float Area { get; set; }
        public int Habitants
        {
            get { return Streets.Sum(street => street.Habitants); }
        }
        public List<Street> Streets { get; set; }

        public City()
        {
            Id = _idGenerator++;
        }

        public override bool Equals(object obj)
        {
            var city = obj as City;
            return city != null &&
                   Id == city.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }
    }
}
