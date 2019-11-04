using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMap.Models.Entities
{
    public class RoomResident
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public int ResidentId { get; set; }
        public Resident Resident { get; set; }

        public RoomResident(){}
    }
}
