using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMap.Helpers.Utils;

namespace WpfMap.Models.Entities
{
    public class Resident
    {
        public int Id { get; set; }
        public string UID { get; set; }
        public enum Sex { MALE, FEMALE }
        public string Name { get; set; }
        public int Age { get; set; }
        public Sex? Gender { get; set; }
        public string Phone { get; set; }
        public int ResidenceTime { get; set; }
        public virtual List<RoomResident> RoomResidents { get; set; }

        public Resident(){}

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
