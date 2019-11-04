using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMap.Models.Entities
{
    public class Street
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public string Name { get; set; }
        public int Habitants
        {
            get { return Houses.Sum(house => house.Habitants); }
        }
        public virtual List<House> Houses { get; set; }

        public Street(){}

        public override bool Equals(object obj)
        {
            var street = obj as Street;
            return street != null &&
                   Id == street.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }
    }
}
