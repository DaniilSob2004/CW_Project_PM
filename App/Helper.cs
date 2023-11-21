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

        public string CombineUrl(params string[] parts)
        {
            StringBuilder result = new();
            string temp;
            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i] == "..") { continue; }

                temp = "/" + parts[i].TrimStart('/').TrimEnd('/');

                if ((i != parts.Length - 1) && parts[i + 1] == "..") { continue; }

                result.Append(temp);
            }
            return result.ToString();


            // ------------- третья версия -------------
            //StringBuilder result = new();
            //string temp;
            //foreach (string part in parts)
            //{
            //    // !!! СВОИ ЦИКЛЫ СДЕЛАЛ, НО ПОТОМ НАШЁЛ ГОТОВЫЕ МЕТОДЫ TrimStart() и TrimEnd() !!!
            //    // while (temp[0] == '/') { temp = temp[1..^0]; temp = "/" + temp }
            //    // while (temp[temp.Length - 1] == '/') { temp = temp[..^1]; }

            //    temp = "/" + part.TrimStart('/').TrimEnd('/');
            //    result.Append(temp);
            //}
            //return result.ToString();


            // ------------- вторая версия -------------
            //StringBuilder result = new();
            //string temp;
            //foreach (string part in parts)
            //{
            //    temp = part;
            //    if (!part.StartsWith('/'))
            //    {
            //        temp = "/" + temp;
            //    }
            //    if (part.EndsWith('/'))
            //    {
            //        temp = temp[..^1];  // от 0 до предпоследнего
            //    }
            //    result.Append(temp);
            //}
            //return result.ToString();


            // ------------- первая версия -------------
            //if (!part1.StartsWith('/'))
            //{
            //    part1 = "/" + part1;
            //}
            //if (part1.EndsWith('/'))
            //{
            //    part1 = part1[0..^1];  // от 0 до предпоследнего
            //}

            //if (!part2.StartsWith('/'))
            //{
            //    part2 = "/" + part2;
            //}
            //if (part2.EndsWith('/'))
            //{
            //    part2 = part2[..^1];
            //}

            //return $"{part1}{part2}";
        }
    }
}
