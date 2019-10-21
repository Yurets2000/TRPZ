using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMap.Helpers.Validators
{
    public class BasicValidator
    {
        public static bool IsStringValid(string text)
        {
            return !string.IsNullOrEmpty(text?.Trim());
        }

        public static bool IsIntValid(string text)
        {
            return int.TryParse(text, out int n);
        }

        public static bool IsIntValidInRange(string text, int min, int max)
        {
            if (!IsIntValid(text))
            {
                return false;
            }
            else
            {
                int n = 0;
                int.TryParse(text, out n);
                return n >= min && n <= max;
            }
        }

        public static bool IsFloatValid(string text)
        {
            return float.TryParse(text, out float f);
        }

        public static bool IsFloatValidInRange(string text, float min, float max)
        {
            if (!IsFloatValid(text))
            {
                return false;
            }
            else
            {
                float f = 0;
                float.TryParse(text, out f);
                return f >= min && f <= max;
            }
        }
    }
}
