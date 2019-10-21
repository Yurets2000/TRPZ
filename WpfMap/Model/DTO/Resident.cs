using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMap.Model.DTO
{
    public class Resident
    {
        private static int _idGenerator = 0;
        public int Id { get; private set; }
        public enum Sex { MALE, FEMALE }
        public string Name { get; set; }
        public int Age { get; set; }
        public Sex Gender { get; set; }
        public string Phone { get; set; }
        public int ResidenceTime { get; set; }

        public Resident()
        {
            Id = _idGenerator++;
        }

        public override bool Equals(object obj)
        {
            var resident = obj as Resident;
            return resident != null &&
                   Id == resident.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }
    }
}
