using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMap.Models.Entities
{
    public class City
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public string Name { get; set; }
        public float Area { get; set; }
        public int Habitants
        {
            get { return Streets.Sum(street => street.Habitants); }
        }
        public virtual List<Street> Streets { get; set; }

        public City(){}

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
