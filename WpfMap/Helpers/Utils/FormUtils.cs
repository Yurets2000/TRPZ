using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfMap.Model
{
    public class Utils
    {
        public static void RefreshComboBox(ComboBox comboBox)
        {
            int selectedIndex = comboBox.SelectedIndex;
            comboBox.SelectedIndex = -1;
            comboBox.Items.Refresh();
            comboBox.SelectedIndex = selectedIndex;
        }
    }
}
