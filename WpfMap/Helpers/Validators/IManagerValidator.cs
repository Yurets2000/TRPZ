using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfMap.Helpers.Validators
{
    public interface IManagerValidator<T> where T : Window
    {
       bool IsDataValidForAddition(T manager);
       bool IsDataValidForEdition(T manager);
    }
}
