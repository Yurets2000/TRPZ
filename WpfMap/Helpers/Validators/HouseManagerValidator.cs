using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMap.Helpers.Validators
{
    public class HouseManagerValidator : IManagerValidator<HouseManager>
    {
        public bool IsDataValidForAddition(HouseManager manager)
        {
            return BasicValidator.IsStringValid(manager.addHouseAddress.Text);
        }

        public bool IsDataValidForEdition(HouseManager manager)
        {
             return BasicValidator.IsStringValid(manager.editHouseAddress.Text);
        }
    }
}
