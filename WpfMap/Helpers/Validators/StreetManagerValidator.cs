using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMap.Helpers.Validators
{
    public class StreetManagerValidator : IManagerValidator<StreetManager>
    {
        public bool IsDataValidForAddition(StreetManager manager)
        {
            return BasicValidator.IsStringValid(manager.addStreetName.Text);
        }

        public bool IsDataValidForEdition(StreetManager manager)
        {
            return BasicValidator.IsStringValid(manager.editStreetName.Text);
        }
    }
}
