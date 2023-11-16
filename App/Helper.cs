using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    public class Helper
    {
        static char[] specialChars = { '!', '?', '.', ',' };

        public string Ellipsis(string input, int len)
        {
            //return "He...";
            //return (len > 5)?"Hel...":"He...";
            //return "Hel"[..(len-3)]+"...";
            return input[..(len - 3)] + "...";
        }

        public string Finalize(string input)
        {
            int len = input.Length;
            return (len > 0 && !specialChars.Contains(input[len - 1])) ? input += "." : input;
        }
    }
}
