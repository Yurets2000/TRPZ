using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMap.Helpers.Validators
{
    public class CityManagerValidator : IManagerValidator<CityManager>
    {
        public bool IsDataValidForAddition(CityManager manager)
        {
            return BasicValidator.IsStringValid(manager.addCityName.Text) &&
                   BasicValidator.IsFloatValidInRange(manager.addCityArea.Text, 0, 10000);
        }

        public bool IsDataValidForEdition(CityManager manager)
        {
            return BasicValidator.IsStringValid(manager.editCityName.Text) &&
                   BasicValidator.IsFloatValidInRange(manager.editCityArea.Text, 0, 10000);
        }
    }
}
