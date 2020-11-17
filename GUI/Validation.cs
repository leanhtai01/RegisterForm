using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GUI
{
    public class Validation
    {
        public bool IsValid(string inputString, string pattern)
        {
            Regex regex = new Regex(pattern);

            return regex.IsMatch(inputString);
        }
    }
}
