using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfMap.Helpers.Validators
{
    public class CountryManagerValidator : IManagerValidator<CountryManager>
    {
        public bool IsDataValidForAddition(CountryManager manager)
        {
            return BasicValidator.IsStringValid(manager.addCountryName.Text);
        }

        public bool IsDataValidForEdition(CountryManager manager)
        {
            return BasicValidator.IsStringValid(manager.editCountryName.Text);
        }
    }
}
