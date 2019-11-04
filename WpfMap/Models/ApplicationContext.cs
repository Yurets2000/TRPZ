using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMap.Models.Entities;

namespace WpfMap.Models
{
    public class ApplicationContext
    {
        public static User CurrentUser { get; set; }
    }
}
