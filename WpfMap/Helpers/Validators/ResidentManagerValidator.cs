using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMap.Helpers.Validators
{
    public class ResidentManagerValidator : IManagerValidator<ResidentManager>
    {
        public bool IsDataValidForAddition(ResidentManager manager)
        {
            return BasicValidator.IsStringValid(manager.addResidentName.Text) &&
                   BasicValidator.IsIntValidInRange(manager.addResidentAge.Text, 0, 120) &&
                   BasicValidator.IsStringValid(manager.addResidentGender.Text) &&
                   BasicValidator.IsIntValidInRange(manager.addResidentTime.Text, 0, int.Parse(manager.addResidentAge.Text));
        }

        public bool IsDataValidForEdition(ResidentManager manager)
        {
            return BasicValidator.IsStringValid(manager.editResidentName.Text) &&
                   BasicValidator.IsIntValidInRange(manager.editResidentAge.Text, 0, 120) &&
                   BasicValidator.IsStringValid(manager.editResidentGender.Text) &&
                   BasicValidator.IsIntValidInRange(manager.editResidentTime.Text, 0, int.Parse(manager.editResidentAge.Text));
        }
    }
}
